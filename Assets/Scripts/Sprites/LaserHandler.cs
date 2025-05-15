using UnityEngine;

public class LaserHandler : MonoBehaviour
{
    public Sprite redLaserSprite;
    public Sprite blueLaserSprite;
    public Sprite greenLaserSprite;
    public Sprite yellowLaserSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateLaserColor();
    }

    void UpdateLaserColor()
    {
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
        // Debug.Log("Laser Color Updated: " + ColorManager.Instance.CurrentColor);
    }
}
