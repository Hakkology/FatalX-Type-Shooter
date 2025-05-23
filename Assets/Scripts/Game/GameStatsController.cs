using UnityEngine;

public class GameStatsController : MonoBehaviour
{
    public static GameStatsController Instance { get; private set; }

    // Counters
    public int DestroyedObjectCount { get; private set; }
    public int WrongKeyPressCount { get; private set; }
    public int CorrectKeyPressCount { get; private set; }
    public int HitAsteroidCount { get; private set; }
    public int HitShipCount { get; private set; }
    public float Accuracy
    {
        get
        {
            int total = CorrectKeyPressCount + WrongKeyPressCount;
            return total > 0 ? (CorrectKeyPressCount / (float)total) * 100f : 0f;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        ResetStats();
    }

    public void ResetStats()
    {
        DestroyedObjectCount = 0;
        WrongKeyPressCount = 0;
        CorrectKeyPressCount = 0;
        HitAsteroidCount = 0;
        HitShipCount = 0;
    }

    public void AddDestroyedObject() => DestroyedObjectCount++;
    public void AddWrongKeyPress() => WrongKeyPressCount++;
    public void AddCorrectKeyPress() => CorrectKeyPressCount++;
    public void AddHitAsteroid() => HitAsteroidCount++;
    public void AddHitShip() => HitShipCount++;
}
