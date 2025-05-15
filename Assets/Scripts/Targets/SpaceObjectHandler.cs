using System.Collections.Generic;
using UnityEngine;

public class SpaceObjectHandler : MonoBehaviour
{
    public static SpaceObjectHandler Instance { get; private set; }

    // Sprite listeleri
    public List<Sprite> smallAsteroids = new List<Sprite>();
    public List<Sprite> mediumAsteroids = new List<Sprite>();
    public List<Sprite> largeAsteroids = new List<Sprite>();
    public List<Sprite> giantAsteroids = new List<Sprite>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Atlas içindeki sprite'ları yükle
    private void LoadSpriteAtlas(string path, List<Sprite> spriteList)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        
        if (sprites.Length == 0)
        {
            Debug.LogError($"No sprites found in path: {path}");
            return;
        }

        foreach (var sprite in sprites)
        {
            if (sprite != null)
            {
                spriteList.Add(sprite);
            }
        }

        Debug.Log($"Loaded {sprites.Length} sprites from {path}");
    }

    // Rastgele sprite döndür
    public Sprite GetRandomSprite(int wordLength)
    {
        List<Sprite> spriteList;

        if (wordLength < 4)
            spriteList = smallAsteroids;
        else if (wordLength < 7)
            spriteList = mediumAsteroids;
        else if (wordLength < 10)
            spriteList = largeAsteroids;
        else
            spriteList = giantAsteroids;

        if (spriteList == null || spriteList.Count == 0)
        {
            Debug.LogError("No sprites found for the given size!");
            return null;
        }

        int randomIndex = Random.Range(0, spriteList.Count);
        return spriteList[randomIndex];
    }
}
