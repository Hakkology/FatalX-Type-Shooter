using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceObjectSpawner : MonoBehaviour
{
    public List<string> spawnedWords = new List<string>();
    public List<GameObject> spawnedObjects = new List<GameObject>();    
    private string[] words = {
        "apple", "orange", "banana", "cherry", "peach", "melon", "grape", "kiwi", "mango", "lemon",
        "strawberry", "blueberry", "raspberry", "watermelon", "pineapple", "coconut", "papaya", "plum", "pear", "tomato",
        "cucumber", "carrot", "onion", "garlic", "pepper", "potato", "broccoli", "spinach", "lettuce", "cabbage",
        "zucchini", "pumpkin", "eggplant", "radish", "celery", "asparagus", "mushroom", "beetroot", "corn", "rice",
        "pasta", "bread", "butter", "cheese", "milk", "yogurt", "cream", "cereal", "oats", "chicken",
        "beef", "pork", "lamb", "fish", "shrimp", "crab", "lobster", "mussel", "oyster", "salmon",
        "tuna", "trout", "cod", "mackerel", "sardine", "anchovy", "swordfish", "squid", "octopus", "coffee",
        "tea", "juice", "soda", "water", "lemonade", "cola", "beer", "wine", "whisky", "gin",
        "vodka", "rum", "tequila", "cider", "brandy", "bourbon", "sake", "absinthe", "vermouth", "cake",
        "pie", "cookie", "brownie", "muffin", "croissant", "doughnut", "biscuit", "waffle", "pancake", "soup",
        "salad", "sandwich", "burger", "pizza", "taco", "burrito", "kebab", "sushi", "ramen", "noodle",
        "dumpling", "curry", "stew", "chili", "barbecue", "roast", "grill", "fry", "bake", "chocolate",
        "candy", "gum", "toffee", "caramel", "fudge", "jelly", "marshmallow", "licorice", "lollipop", "rose",
        "lily", "tulip", "daisy", "orchid", "sunflower", "violet", "jasmine", "daffodil", "lavender", "ocean",
        "river", "lake", "stream", "waterfall", "pond", "sea", "bay", "lagoon", "creek", "mountain",
        "hill", "valley", "canyon", "plateau", "plain", "forest", "jungle", "desert", "savanna", "car",
        "bike", "bus", "train", "tram", "plane", "boat", "ship", "ferry", "yacht", "pencil",
        "pen", "eraser", "ruler", "compass", "brush", "paint", "paper", "notebook", "folder", "computer",
        "laptop", "tablet", "phone", "monitor", "keyboard", "mouse", "printer", "scanner", "camera", "rocket",
        "satellite", "planet", "star", "comet", "meteor", "asteroid", "galaxy", "nebula", "orbit", "dog",
        "cat", "bird", "fish", "horse", "cow", "pig", "sheep", "goat", "rabbit", "lion",
        "tiger", "bear", "wolf", "elephant", "giraffe", "zebra", "kangaroo", "panda", "fox", "book",
        "magazine", "newspaper", "novel", "comic", "diary", "guide", "atlas", "map", "dictionary", "rock",
        "paper", "scissors", "hammer", "saw", "drill", "wrench", "screwdriver", "pliers", "tape"
    };

    public GameObject objectPrefab;
    public float spawnRate = 1f;
    public int maxObjects = 10;

    void Start()
    {
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
            spaceObjectScript.Initialize(this, selectedWord);
            spawnedWords.Add(selectedWord);
            spawnedObjects.Add(spaceObject);

            yield return new WaitForSecondsRealtime(spawnRate);
        }
    }

    public void DeSpawnObject(string word, GameObject spaceObject)
    {
        Debug.Log("This word is despawned: " + word);
        spawnedWords.Remove(word);
        spawnedObjects.Remove(spaceObject);
    }

    private Vector3 GetSpawnPosition()
    {
        float offset = 2f; 
        float xPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.nearClipPlane)).x + offset;

        float yMin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;
        float yMax = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.nearClipPlane)).y;
        float yPosition = Random.Range(yMin, yMax);

        return new Vector3(xPosition, yPosition, 0);
    }

}