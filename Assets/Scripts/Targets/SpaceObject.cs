using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpaceObject : MonoBehaviour
{
    public string _word;
    private TextMeshProUGUI _wordText;
    private Canvas _canvas;
    private SpriteRenderer _spriteRenderer;
    private SpaceObjectSpawner _spawner;
    private BoxCollider2D _collider2D;
    private CameraShakeController _cameraShake;
    private PlayerController _player;

    private float _speed = 2;
    private float _rotationMultiplier = 10f;
    private int _hp;
    private string _initialWord;
    private bool _missedObject = false;
    private float margin = -5f;
    private float offscreenX;

    public event Action LockedAsTargetEvent;
    public event Action LaserHitEvent;
    public event Action DestroyedEvent;

    void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _canvas = GetComponentInChildren<Canvas>();
        _wordText = GetComponentInChildren<TextMeshProUGUI>();
        _collider2D = GetComponent<BoxCollider2D>();
        _cameraShake = Camera.main.GetComponent<CameraShakeController>();

        _canvas.renderMode = RenderMode.WorldSpace;
        _canvas.worldCamera = Camera.main;
        _collider2D.enabled = false;

        float leftWorldX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        offscreenX = leftWorldX - margin;

        LockedAsTargetEvent += OnLockedAsTarget;
        LaserHitEvent += OnLaserHit;
        DestroyedEvent += OnDestroyed;
    }

    public void Initialize(SpaceObjectSpawner spawner, string word, PlayerController player, float speed)
    {
        _spawner = spawner;
        _player = player;
        _word = word;
        _speed = speed;
        _hp = word.Length;
        _initialWord = _word;
        UpdateWordText();
        SpaceObjectSpriteLoader();
        AdjustColliderSize();
    }

    private void AdjustColliderSize()
    {
        switch (_initialWord.Length)
        {
            case<7:
                _collider2D.size = new Vector2(2,2);
                break;
            default:
                _collider2D.size = new Vector2(3,3);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);

        float randomFactor = Random.Range(0.75f, 1.25f);
        float zAngle = _speed * _rotationMultiplier * randomFactor * Time.deltaTime;
        _spriteRenderer.gameObject.transform.Rotate(0f, 0f, zAngle);
        _wordText.transform.rotation = Quaternion.identity;

        if (transform.position.x < offscreenX && !_missedObject)
            OnMissed();
    }

    void SpaceObjectSpriteLoader()
    {
        Sprite selectedSprite = SpaceObjectHandler.Instance.GetRandomSprite(_word.Length);
        if (selectedSprite != null)
        {
            _spriteRenderer.sprite = selectedSprite;
        }
    }

    public bool TakeDamage()
    {
        _hp--;
        if (_hp <= 0)
        {
            // Destroy(gameObject);
            return true;
        }
        return false;
    }

    void UpdateWordText()
    {
        _wordText.text = _word;
    }

    public void UpdateWord()
    {
        _word = _word.Substring(1);
        UpdateWordText();
    }

    private void OnLockedAsTarget()
    {
        _collider2D.enabled = true;
        _wordText.color = Color.red;
    }

    private void OnLaserHit()
    {
        SoundController.RequestSound(_initialWord.Length < 7 ? SoundID.ImpactAstro : SoundID.ImpactShip);
        bool isDead = TakeDamage();
        if (isDead)
            DestroyedEvent?.Invoke();
    }

    private void OnDestroyed()
    {
        if (_spawner != null)
            _spawner.DeSpawnObject(this._initialWord, gameObject);

        _player.ScoreController.AddScore(_initialWord.Length);
        SoundController.RequestSound(_initialWord.Length < 7 ? SoundID.ExplosionAstro : SoundID.ExplosionShip);
        GameObject explosionVFX = VFXPool.Instance.GetExplosionVFX();
        if (explosionVFX != null)
        {
            explosionVFX.GetComponent<VFXExplosionController>().PlayExplosionEffect(transform.position);
        }

        ShakeType shakeType;

        if (_initialWord.Length < 4)
            shakeType = ShakeType.Tiny;
        else if (_initialWord.Length < 6)
            shakeType = ShakeType.Small;
        else if (_initialWord.Length < 8)
            shakeType = ShakeType.Medium;
        else
            shakeType = ShakeType.Huge;

        _cameraShake?.Shake(shakeType);

        _spriteRenderer.DOFade(0, 0.2f).OnComplete(() => Destroy(gameObject)); 
        transform.DOScale(Vector3.zero, 0.2f);
    }

    private void OnMissed()
    {
        _missedObject = true;
        
        if (_spawner != null)
            _spawner.DeSpawnObject(this._initialWord, gameObject);

        _player.HealthController.TakeDamage(1);
        _spriteRenderer.DOFade(0, 0.2f).OnComplete(() => Destroy(gameObject)); 
        transform.DOScale(Vector3.zero, 0.2f);
    }

    public void InvokeLockedAsTarget()
    {
        LockedAsTargetEvent?.Invoke();
    }

    public void InvokeLaserHit()
    {
        LaserHitEvent?.Invoke();
    }
    
    private void UnsubscribeEvents()
    {
        LockedAsTargetEvent -= OnLockedAsTarget;
        LaserHitEvent -= OnLaserHit;
        DestroyedEvent -= OnDestroyed;
        // Debug.Log("Events Unsubscribed");
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
