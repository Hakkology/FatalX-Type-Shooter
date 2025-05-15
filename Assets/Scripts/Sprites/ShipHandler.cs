using UnityEngine;

public class ShipHandler : MonoBehaviour
{
    public Sprite[] shipSprites;  // 8 adet sprite (2'şer adet renk başına)

    private ShipAnimationHandler animationHandler;

    void Start()
    {
        animationHandler = GetComponent<ShipAnimationHandler>();
        UpdateShipColor();
    }

    void UpdateShipColor()
    {
        // Mevcut rengi al
        int colorIndex = (int)ColorManager.Instance.CurrentColor;

        // Renk indexine göre sprite'ları seç
        int spriteIndex1 = colorIndex * 2;      // İlk sprite
        int spriteIndex2 = colorIndex * 2 + 1;  // İkinci sprite

        if (shipSprites.Length >= spriteIndex2 + 1)
        {
            Sprite sprite1 = shipSprites[spriteIndex1];
            Sprite sprite2 = shipSprites[spriteIndex2];
            animationHandler.SetSprites(sprite1, sprite2);
            Debug.Log("Ship Sprites Updated: " + ColorManager.Instance.CurrentColor);
        }
        else
        {
            Debug.LogError("Insufficient sprites in the array. Expected 8 sprites.");
        }
    }
}
