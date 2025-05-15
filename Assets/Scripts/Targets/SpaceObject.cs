using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    public string _word;
    private TextMeshProUGUI _wordText;
    private SpriteRenderer _spriteRenderer;
    private SpaceObjectSpawner _spawner;

    private float speed = 2;
    private int _hp;
    private Sprite[] spaceObjectSprites;

    void Awake()
    {
        _wordText = GetComponent<TextMeshProUGUI>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        switch (_word.Length)
        {
            case < 4:
                _spriteRenderer.sprite = spaceObjectSprites[0];
                break;
            case < 7:
                _spriteRenderer.sprite = spaceObjectSprites[1];
                break;
            case < 10:
                _spriteRenderer.sprite = spaceObjectSprites[2];
                break;
            default:
                _spriteRenderer.sprite = spaceObjectSprites[3];
                break;
        }
    }

    public bool TakeDamage()
    {
        if (_word.Length > 0)
        {
            _word = _word.Substring(1);
            _hp--;
            UpdateWordText();

            if (_hp <= 0)
            {
                Destroy(gameObject);
                return true; 
            }
        }
        return false;
    }

    void UpdateWordText()
    {
        _wordText.text = _word;
    }
    
    private void OnDestroy()
    {
        if (_spawner != null)
        {
            _spawner.DeSpawnObject(this._word);
        }
    }
}
