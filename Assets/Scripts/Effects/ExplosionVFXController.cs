using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VFXExplosionController : MonoBehaviour
{
    private ParticleSystem explosionParticles;
    private Light2D explosionLight;

    void Awake()
    {
        explosionParticles = GetComponent<ParticleSystem>();
        explosionLight = GetComponentInChildren<Light2D>();

        // Işığı aktif hale getir
        if (explosionLight != null)
        {
            explosionLight.enabled = true;
            explosionLight.intensity = 0;
        }
    }

    public void PlayExplosionEffect(Vector3 position)
    {
        transform.position = position;

        // Işık efekti
        if (explosionLight != null)
        {
            FlashExplosionLight();
        }

        // Partikül efekti
        if (explosionParticles != null)
        {
            explosionParticles.Play();
        }

        // VFX'i geri döndür
        Invoke(nameof(ReturnToPool), 1.0f);
    }

    private void ReturnToPool()
    {
        VFXPool.Instance.ReturnExplosionVFX(gameObject);
    }

    private void FlashExplosionLight()
    {
        if (explosionLight != null)
        {
            explosionLight.DOKill(); // Önceki animasyonu iptal et

            // Işığı hızlıca aç ve yavaşça kapat
            DOTween.To(() => explosionLight.intensity, x => explosionLight.intensity = x, 8, 0.1f).OnComplete(() =>
            {
                DOTween.To(() => explosionLight.intensity, x => explosionLight.intensity = x, 0, 0.5f);
            });
        }
    }
}
