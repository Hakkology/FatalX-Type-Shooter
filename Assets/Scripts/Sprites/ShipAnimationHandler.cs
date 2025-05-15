using System.Collections;
using UnityEngine;

public class ShipAnimationHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Sprite sprite1;
    private Sprite sprite2;
    public float cycleSpeed = 0.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(CycleSprites());
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
