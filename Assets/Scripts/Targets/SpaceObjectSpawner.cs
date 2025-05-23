using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpaceObjectSpawner : MonoBehaviour
{
    public PlayerController playerController;
    public event Action<string, GameObject> ObjectDespawned;
    [SerializeField] private ProgressionController progressionManager;
    public List<string> spawnedWords = new List<string>();
    public List<GameObject> spawnedObjects = new List<GameObject>();    
    private string[] words = {
        "APPLE", "ORANGE", "BANANA", "CHERRY", "PEACH", "MELON", "GRAPE", "KIWI", "MANGO", "LEMON",
        "STRAWBERRY", "BLUEBERRY", "RASPBERRY", "WATERMELON", "PINEAPPLE", "COCONUT", "PAPAYA", "PLUM", "PEAR", "TOMATO",
        "CUCUMBER", "CARROT", "ONION", "GARLIC", "PEPPER", "POTATO", "BROCCOLI", "SPINACH", "LETTUCE", "CABBAGE",
        "ZUCCHINI", "PUMPKIN", "EGGPLANT", "RADISH", "CELERY", "ASPARAGUS", "MUSHROOM", "BEETROOT", "CORN", "RICE",
        "PASTA", "BREAD", "BUTTER", "CHEESE", "MILK", "YOGURT", "CREAM", "CEREAL", "OATS", "CHICKEN",
        "BEEF", "PORK", "LAMB", "FISH", "SHRIMP", "CRAB", "LOBSTER", "MUSSEL", "OYSTER", "SALMON",
        "TUNA", "TROUT", "COD", "MACKEREL", "SARDINE", "ANCHOVY", "SWORDFISH", "SQUID", "OCTOPUS", "COFFEE",
        "TEA", "JUICE", "SODA", "WATER", "LEMONADE", "COLA", "BEER", "WINE", "WHISKY", "GIN",
        "VODKA", "RUM", "TEQUILA", "CIDER", "BRANDY", "BOURBON", "SAKE", "ABSINTHE", "VERMOUTH", "CAKE",
        "PIE", "COOKIE", "BROWNIE", "MUFFIN", "CROISSANT", "DOUGHNUT", "BISCUIT", "WAFFLE", "PANCAKE", "SOUP",
        "SALAD", "SANDWICH", "BURGER", "PIZZA", "TACO", "BURRITO", "KEBAB", "SUSHI", "RAMEN", "NOODLE",
        "DUMPLING", "CURRY", "STEW", "CHILI", "BARBECUE", "ROAST", "GRILL", "FRY", "BAKE", "CHOCOLATE",
        "CANDY", "GUM", "TOFFEE", "CARAMEL", "FUDGE", "JELLY", "MARSHMALLOW", "LICORICE", "LOLLIPOP", "ROSE",
        "LILY", "TULIP", "DAISY", "ORCHID", "SUNFLOWER", "VIOLET", "JASMINE", "DAFFODIL", "LAVENDER", "OCEAN",
        "RIVER", "LAKE", "STREAM", "WATERFALL", "POND", "SEA", "BAY", "LAGOON", "CREEK", "MOUNTAIN",
        "HILL", "VALLEY", "CANYON", "PLATEAU", "PLAIN", "FOREST", "JUNGLE", "DESERT", "SAVANNA", "CAR",
        "BIKE", "BUS", "TRAIN", "TRAM", "PLANE", "BOAT", "SHIP", "FERRY", "YACHT", "PENCIL",
        "PEN", "ERASER", "RULER", "COMPASS", "BRUSH", "PAINT", "PAPER", "NOTEBOOK", "FOLDER", "COMPUTER",
        "LAPTOP", "TABLET", "PHONE", "MONITOR", "KEYBOARD", "MOUSE", "PRINTER", "SCANNER", "CAMERA", "ROCKET",
        "SATELLITE", "PLANET", "STAR", "COMET", "METEOR", "ASTEROID", "GALAXY", "NEBULA", "ORBIT", "DOG",
        "CAT", "BIRD", "FISH", "HORSE", "COW", "PIG", "SHEEP", "GOAT", "RABBIT", "LION",
        "TIGER", "BEAR", "WOLF", "ELEPHANT", "GIRAFFE", "ZEBRA", "KANGAROO", "PANDA", "FOX", "BOOK",
        "MAGAZINE", "NEWSPAPER", "NOVEL", "COMIC", "DIARY", "GUIDE", "ATLAS", "MAP", "DICTIONARY", "ROCK",
        "PAPER", "SCISSORS", "HAMMER", "SAW", "DRILL", "WRENCH", "SCREWDRIVER", "PLIERS", "TAPE"
    };

    public GameObject objectPrefab;
    public float spawnRate = 1f;
    public int maxObjects = 10;


    void Start()
    {
        HUDManager.Instance.RaiseTargetCountChanged(spawnedWords.Count);
        StartCoroutine(nameof(SpawnObject));
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            if (spawnedWords.Count >= maxObjects)
            {
                yield return new WaitForSecondsRealtime(0.2f);
                continue;
            }

            // İlk harfleri topla ve filtrele
            var existingFirstLetters = new HashSet<char>(spawnedWords.Select(word => word[0]));
            var filteredWords = words.Where(word => !existingFirstLetters.Contains(word[0])).ToList();

            // Uygun kelime yoksa tüm kelimelerden rastgele seç
            string selectedWord = filteredWords.Count > 0
                ? filteredWords[Random.Range(0, filteredWords.Count)]
                : words[Random.Range(0, words.Length)];

            // Yeni uzay nesnesi oluştur
            Vector3 spawnPosition = GetSpawnPosition();
            GameObject spaceObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            SpaceObject spaceObjectScript = spaceObject.GetComponent<SpaceObject>();

            float speed = progressionManager.GetCurrentSpeed();

            spaceObjectScript.Initialize(this, selectedWord, playerController, speed);
            spawnedWords.Add(selectedWord);
            spawnedObjects.Add(spaceObject);

            HUDManager.Instance.RaiseTargetCountChanged(spawnedWords.Count);

            yield return new WaitForSecondsRealtime(spawnRate);
        }
    }

    public void DeSpawnObject(string word, GameObject spaceObject)
    {
        Debug.Log("This word is despawned: " + word);
        ObjectDespawned?.Invoke(word, spaceObject);
        spawnedWords.Remove(word);
        spawnedObjects.Remove(spaceObject);
        HUDManager.Instance.RaiseTargetCountChanged(spawnedWords.Count);
    }

    private Vector3 GetSpawnPosition()
    {
        var cam = Camera.main;
        float offset = 2f;
        float camDistance = Mathf.Abs(cam.transform.position.z);

        // Sağ kenar
        float xPos = cam.ViewportToWorldPoint(new Vector3(1, 0, camDistance)).x + offset;

        // Dünya birimi cinsinden marj
        float worldMargin = 2f; // oyun alanınızda 1 birim boşluk
        var botLeft  = cam.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        float yMin = botLeft.y + worldMargin;
        float yMax = topRight.y - worldMargin;
        float yPos = Random.Range(yMin, yMax);

        return new Vector3(xPos, yPos, 0f);
    }
}