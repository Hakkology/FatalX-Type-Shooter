using System;
using UnityEngine;

public class ProgressionController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpaceObjectSpawner spawner;

    [Header("Movement Speed")]
    [SerializeField] private float initialSpeed = 2f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float speedStep = 0.2f;
    [SerializeField] private int speedThreshold = 10;  // her object için

    [Header("Spawn Interval")]
    [SerializeField] private float initialInterval = 1f;
    [SerializeField] private float minInterval = 0.2f;
    [SerializeField] private float intervalStep = 0.004f; 

    private float _currentSpeed;
    private float _currentInterval;
    private int _destroyedCount;

    void Awake()
    {
        _currentSpeed = initialSpeed;
        _currentInterval = initialInterval;
    }

    void Start()
    {
        if (spawner == null)
            Debug.LogError("ProgressionManager: Spawner reference missing!");
        else
            spawner.ObjectDespawned += OnObjectDespawned;
    }

    void OnEnable()
    {
        if (spawner != null)
            spawner.ObjectDespawned += OnObjectDespawned;
    }

    void OnDisable()
    {
        if (spawner != null)
            spawner.ObjectDespawned -= OnObjectDespawned;
    }

    private void OnObjectDespawned(string word, GameObject go)
    {
        _destroyedCount++;
        GameStatsController.Instance.AddDestroyedObject();

        if (word.Length < 6)
            GameStatsController.Instance.AddHitAsteroid();
        else
            GameStatsController.Instance.AddHitShip();

        // 1) Spawn interval‘ı düşür
        _currentInterval = Mathf.Max(minInterval, _currentInterval - intervalStep);
        spawner.spawnRate = _currentInterval;

        // 2) Her speedThreshold objede bir speed artır
        if (_destroyedCount % speedThreshold == 0)
        {
            _currentSpeed = Mathf.Min(maxSpeed, _currentSpeed + speedStep);
            HUDManager.Instance.UpdateChaosMeter(_currentSpeed);
        }
    }

    public void ResetProgression()
    {
        _destroyedCount = 0;
        _currentSpeed = initialSpeed;
        _currentInterval = initialInterval;
        spawner.spawnRate = _currentInterval;
    }


    public float GetCurrentSpeed() => _currentSpeed;
}
