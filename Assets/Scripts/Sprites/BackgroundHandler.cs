using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpriteHandler : MonoBehaviour
{
    [Header("Sprites on background")]
    public List<Sprite> backgroundSprites;

    [Header("Slide Speed")]
    public float moveSpeed = 2f;

    private Camera cam;
    private SpriteRenderer[] bgRenderers = new SpriteRenderer[2];
    private float viewWidth;
    private float viewHeight;

    void Start()
    {
        cam = Camera.main;
        viewHeight = cam.orthographicSize * 2f;
        viewWidth  = viewHeight * cam.aspect;

        Sprite spr = backgroundSprites[Random.Range(0, backgroundSprites.Count)];
        Vector2 spriteSize = spr.bounds.size; 

        for (int i = 0; i < 2; i++)
        {
            GameObject go = new GameObject("Background_" + i);
            go.transform.SetParent(transform);
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = spr;
            sr.sortingOrder = -10;

            float scaleX = viewWidth  / spriteSize.x;
            float scaleY = viewHeight / spriteSize.y;
            go.transform.localScale = new Vector3(scaleX, scaleY, 1f);
            go.transform.position = new Vector3(i * viewWidth, 0f, 0f);

            bgRenderers[i] = sr;
        }
    }

    void Update()
    {
        // 6) Kaydırma ve sınır atlama
        for (int i = 0; i < 2; i++)
        {
            Transform t = bgRenderers[i].transform;
            t.Translate(Vector3.left * moveSpeed * Time.unscaledDeltaTime);

            // Sprite tamamen görünüm dışına çıktıysa
            if (t.position.x <= -viewWidth)
            {
                // Diğer sprite'ın sağına taşı
                t.position += Vector3.right * (viewWidth * 2f);
            }
        }
    }
}
