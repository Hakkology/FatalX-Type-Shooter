using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    public static LaserPool Instance;

    public GameObject laserPrefab;
    public int poolSize = 10;

    private Queue<GameObject> laserPool = new Queue<GameObject>();

    void Awake()
    {
        // Singleton yapı
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Havuzu oluştur
        for (int i = 0; i < poolSize; i++)
        {
            GameObject laser = Instantiate(laserPrefab);
            var laserController = laser.GetComponent<LaserController>();
            laserController.UpdateLaserColor();
            laser.SetActive(false);
            laserPool.Enqueue(laser);
        }
    }

    public GameObject GetLaser()
    {
        if (laserPool.Count > 0)
        {
            GameObject laser = laserPool.Dequeue();
            laser.SetActive(true);
            return laser;
        }
        else
        {
            // Havuzda lazer kalmadıysa yeni oluştur
            GameObject laser = Instantiate(laserPrefab);
            laser.SetActive(false);
            return laser;
        }
    }

    public void ReturnLaser(GameObject laser)
    {
        laser.SetActive(false);
        laserPool.Enqueue(laser);
    }
}
