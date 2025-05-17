using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    public string _word;
    private TextMeshProUGUI _wordText;
    private Canvas _canvas;
    private SpriteRenderer _spriteRenderer;
    private SpaceObjectSpawner _spawner;
    private BoxCollider2D _collider2D;

    private float speed = 2;
    private int _hp;

    public event Action LockedAsTargetEvent;
    public event Action LaserHitEvent;
    public event Action DestroyedEvent;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _canvas = GetComponentInChildren<Canvas>();
        _wordText = GetComponentInChildren<TextMeshProUGUI>();
        _collider2D = GetComponent<BoxCollider2D>();

        _canvas.renderMode = RenderMode.WorldSpace;
        _canvas.worldCamera = Camera.main;
        _collider2D.enabled = false;

        // Eventlere metod bağlama
        LockedAsTargetEvent += OnLockedAsTarget;
        LaserHitEvent += OnLaserHit;
        DestroyedEvent += OnDestroyed;
    }

    public void Initialize(SpaceObjectSpawner spawner, string word)
    {
        _spawner = spawner;
        _word = word;
        _hp = word.Length;
        UpdateWordText();
        SpaceObjectSpriteLoader();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
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
        Debug.Log("Target Locked: " + _word);

        // TakeDamage();
    }

    private void OnLaserHit()
    {
        bool isDead = TakeDamage();
        if (isDead)
            DestroyedEvent?.Invoke();
    }

    private void OnDestroyed()
    {
        if (_spawner != null)
            _spawner.DeSpawnObject(this._word);

        Destroy(gameObject);
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
        Debug.Log("Events Unsubscribed");
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
