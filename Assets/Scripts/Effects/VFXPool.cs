using System.Collections.Generic;
using UnityEngine;

public class VFXPool : MonoBehaviour
{
    public static VFXPool Instance;

    public GameObject impactVFXPrefab;
    public GameObject explosionVFXPrefab;
    public int poolSize = 10;

    private Queue<GameObject> impactVFXPool = new Queue<GameObject>();
    private Queue<GameObject> explosionVFXPool = new Queue<GameObject>();

    void Awake()
    {
        // Singleton Yapısı
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject impactVFX = Instantiate(impactVFXPrefab);
            impactVFX.SetActive(false);
            impactVFXPool.Enqueue(impactVFX);
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject explosionVFX = Instantiate(explosionVFXPrefab);
            explosionVFX.SetActive(false);
            explosionVFXPool.Enqueue(explosionVFX);
        }
    }

    public GameObject GetImpactVFX()
    {
        if (impactVFXPool.Count > 0)
        {
            GameObject impactVFX = impactVFXPool.Dequeue();
            impactVFX.SetActive(true);
            return impactVFX;
        }
        else
        {
            // Havuz doluysa yeni oluştur
            GameObject impactVFX = Instantiate(impactVFXPrefab);
            impactVFX.SetActive(false);
            return impactVFX;
        }
    }

    public GameObject GetExplosionVFX()
    {
        if (explosionVFXPool.Count > 0)
        {
            GameObject explosionVFX = explosionVFXPool.Dequeue();
            explosionVFX.SetActive(true);
            return explosionVFX;
        }
        else
        {
            // Havuz doluysa yeni oluştur
            GameObject explosionVFX = Instantiate(explosionVFXPrefab);
            explosionVFX.SetActive(false);
            return explosionVFX;
        }
    }

    public void ReturnImpactVFX(GameObject impactVFX)
    {
        impactVFX.SetActive(false);
        impactVFXPool.Enqueue(impactVFX);
    }

    public void ReturnExplosionVFX(GameObject explosionVFX)
    {
        explosionVFX.SetActive(false);
        explosionVFXPool.Enqueue(explosionVFX);
    }
}
