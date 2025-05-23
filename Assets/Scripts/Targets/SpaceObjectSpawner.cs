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
    private string[] _englishWords = {
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

    private string[] _turkishWords = {
        "MASA","SANDALYE","KOLTUK","BILGISAYAR","TELEFON","KAPI","PENCERE","AYNA","HALI","LAMBA",
        "KITAPLIK","DEFTER","KALEM","KIRTASIYE","SILGI","CETVEL","PUSULA","MAKAS","OGRETMEN","OGRENCI",
        "MUHENDIS","DOKTOR","MUZIKCI","SANATCI","MUZIK","YAZAR","SARKICI","HEKIM","OKUL","HASTANE",
        "ECZANE","MARKET","RESTORAN","OTEL","HAVAALANI","LIMAN","KIR","KOY","KEDI","KOPEK",
        "KUZU","INEK","FIL","ASLAN","KAPLAN","AYI","KURT","YILAN","BALIK","KUS",
        "SERCE","KIRMIZI","YESIL","MAVI","SARI","TURUNCU","MOR","LACIVERT","PEMBE","BEYAZ",
        "SIYAH","ELMA","PORTAKAL","MUZ","SEFTALI","KAVUN","UZUM","KIVI","LIMON","CILEK",
        "ARMUT","KIRAZ","NAR","KAYISI","DOMATES","SALATALIK","PATATES","HAVUC","PANCAR","BROKOLI",
        "ISPANAK","LAHANA","PATLICAN","TURP","SARIMSAK","KEREVIZ","ENGINAR","KABAK","MISIR","BIBER",
        "MANTAR","BAS","KOL","BACAK","KALP","BEYIN","GOGUS","KARNI","OMUZ","SIRT",
        "BOYUN","DIRSEK","BILEK","YEMEK","CAY","ICMEK","ZEYTIN","PEYNIR","SUT","YOGURT",
        "RECEL","TEREYAGI","KAHVALTI","TATLILAR","CEREZ","PASTA","BOREK","KURABIYE","KREMA","TURTA",
        "CICEK","GUL","LALE","PAPATYA","ORKIDE","MENEKSE","LAVANTA","AYCICEGI","YASEMIN","NEKTAR",
        "GOKGURULTU","YILDIRIM","YAGMUR","KAR","DOLU","FIRTINA","TOPRAK","DENIZ","ALEV","ATES",
        "IKLIM","GUNES","YILDIZ","UYDU","METEOR","PLANET","GALAKSI","NEBULA","KOMET","GULMEK",
        "AGLAMAK","YUZMEK","KOSMAK","OTURMAK","KALKMAK","SORMAK","CEVAPLAMAK","DUSUNMEK","YAZMAK","RESIM",
        "FILM","MAKALE","HABER","YAZI","SOZ","KITAP","DERGI","GAZETE","YOL","OTOBUS",
        "TREN","METRO","ULASIM","SEYAHAT","GUZERGAH","BINMEK","INMEK","SICAKLIK","BASINC","NEM",
        "RUZGAR","BULUT","SIS","KAHVE","SAKIZ","MASAJ","RIHTIM","ISIK","SES","DANS",
        "TOP","VAKIT","ZAMAN","GECE","GUN","HAFTA","YIL","KIYI","DALGA","SAYFA"
    };

    private string[] _words;

    public GameObject objectPrefab;
    public float spawnRate = 1f;
    public int maxObjects = 10;


    void Start()
    {
        HUDManager.Instance.RaiseTargetCountChanged(spawnedWords.Count);
        _words = (GameSettings.Instance.currentLanguage == Language.English)
            ? _englishWords
            : _turkishWords;
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
            var filteredWords = _words.Where(word => !existingFirstLetters.Contains(word[0])).ToList();

            // Uygun kelime yoksa tüm kelimelerden rastgele seç
            string selectedWord = filteredWords.Count > 0
                ? filteredWords[Random.Range(0, filteredWords.Count)]
                : _words[Random.Range(0, _words.Length)];

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