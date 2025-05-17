using System.Collections;
using UnityEngine;

public class ShipHandler : MonoBehaviour
{
    public Sprite[] shipSprites;  
    private SpriteRenderer spriteRenderer;
    private Sprite sprite1;
    private Sprite sprite2;
    public float cycleSpeed = 0.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateShipColor();
        StartCoroutine(CycleSprites());
    }

    void UpdateShipColor()
    {
        if (ColorManager.Instance == null)
        {
            Debug.LogError("ColorManager instance not found!");
            return;
        }

        int colorIndex = (int)ColorManager.Instance.CurrentColor;

        int spriteIndex1 = colorIndex * 2;
        int spriteIndex2 = colorIndex * 2 + 1;

        if (shipSprites.Length >= spriteIndex2 + 1)
        {
            Sprite sprite1 = shipSprites[spriteIndex1];
            Sprite sprite2 = shipSprites[spriteIndex2];
            SetSprites(sprite1, sprite2);
            Debug.Log("Ship Sprites Updated: " + ColorManager.Instance.CurrentColor);
        }
        else
        {
            Debug.LogError("Insufficient sprites in the array. Expected 8 sprites.");
        }
    }

    public void SetSprites(Sprite s1, Sprite s2)
    {
        sprite1 = s1;
        sprite2 = s2;
        spriteRenderer.sprite = sprite1;
    }

    IEnumerator CycleSprites()
    {
        while (true)
        {
            spriteRenderer.sprite = sprite1;
            yield return new WaitForSeconds(cycleSpeed);
            spriteRenderer.sprite = sprite2;
            yield return new WaitForSeconds(cycleSpeed);
        }
    }

}
