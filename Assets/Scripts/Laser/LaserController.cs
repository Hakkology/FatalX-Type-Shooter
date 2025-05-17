using UnityEngine;

public class LaserController : MonoBehaviour
{
    public Sprite redLaserSprite;
    public Sprite blueLaserSprite;
    public Sprite greenLaserSprite;
    public Sprite yellowLaserSprite;
    private float speed = 45f;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Lazer rengini güncelleme
    public void UpdateLaserColor()
    {
        if (ColorManager.Instance == null) return;

        switch (ColorManager.Instance.CurrentColor)
        {
            case ColorManager.ShipColor.Red:
                spriteRenderer.sprite = redLaserSprite;
                break;
            case ColorManager.ShipColor.Blue:
                spriteRenderer.sprite = blueLaserSprite;
                break;
            case ColorManager.ShipColor.Green:
                spriteRenderer.sprite = greenLaserSprite;
                break;
            case ColorManager.ShipColor.Yellow:
                spriteRenderer.sprite = yellowLaserSprite;
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
            LaserPool.Instance.ReturnLaser(this.gameObject);
        }
    }
}
