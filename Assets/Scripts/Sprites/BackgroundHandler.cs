using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpriteHandler : MonoBehaviour
{
    public List<Sprite> backgroundSprites;  
    public float moveSpeed = 2f; 

    private SpriteRenderer[] renderers; 
    private float spriteWidth;

    void Start()
    {
        Sprite selectedSprite = backgroundSprites[Random.Range(0, backgroundSprites.Count)];

        renderers = new SpriteRenderer[2];
        for (int i = 0; i < 2; i++)
        {
            GameObject bgObject = new GameObject("Background_" + i);
            bgObject.transform.parent = transform;
            renderers[i] = bgObject.AddComponent<SpriteRenderer>();
            renderers[i].sprite = selectedSprite;
            renderers[i].sortingOrder = -10;  // Arka planda kalsın
        }

        spriteWidth = renderers[0].bounds.size.x;

        renderers[0].transform.position = new Vector3(0, 0, 0);
        renderers[1].transform.position = new Vector3(spriteWidth, 0, 0);
    }

    void Update()
    {

        for (int i = 0; i < 2; i++)
        {
            renderers[i].transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            if (renderers[i].transform.position.x <= -spriteWidth)
            {
                Vector3 newPos = renderers[i].transform.position;
                newPos.x += 2 * spriteWidth;
                renderers[i].transform.position = newPos;
            }
        }
    }
}
