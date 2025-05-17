using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LaserController : MonoBehaviour
{
    // public GameObject impactEffectPrefab;
    public Sprite redLaserSprite;
    public Sprite blueLaserSprite;
    public Sprite greenLaserSprite;
    public Sprite yellowLaserSprite;
    private float speed = 45f;

    private SpriteRenderer _spriteRenderer;
    private Light2D _light2D;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _light2D = GetComponentInChildren<Light2D>();
    }

    // Lazer rengini güncelleme
    public void UpdateLaserColor()
    {
        if (ColorManager.Instance == null) return;

        switch (ColorManager.Instance.CurrentColor)
        {
            case ColorManager.ShipColor.Red:
                _spriteRenderer.sprite = redLaserSprite;
                _light2D.color = Color.red;
                break;
            case ColorManager.ShipColor.Blue:
                _spriteRenderer.sprite = blueLaserSprite;
                _light2D.color = Color.blue;
                break;
            case ColorManager.ShipColor.Green:
                _spriteRenderer.sprite = greenLaserSprite;
                _light2D.color = Color.green;
                break;
            case ColorManager.ShipColor.Yellow:
                _spriteRenderer.sprite = yellowLaserSprite;
                _light2D.color = Color.yellow;
                break;
        }
        Debug.Log("Laser Color Updated: " + ColorManager.Instance.CurrentColor);
    }

    // Lazerin hareketi
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.y) > 20)
        {
            LaserPool.Instance.ReturnLaser(gameObject);
        }
    }

    public void ActivateLaser(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            collision.gameObject.GetComponent<SpaceObject>().InvokeLaserHit();
            // Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            LaserPool.Instance.ReturnLaser(this.gameObject);
        }
    }
}
