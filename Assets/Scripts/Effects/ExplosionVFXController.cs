using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VFXExplosionController : MonoBehaviour
{
    private Color impactColor;
    private ParticleSystem explosionParticles;
    private Light2D explosionLight;

    void Awake()
    {
        explosionParticles = GetComponent<ParticleSystem>();
        explosionLight = GetComponentInChildren<Light2D>();

        if (ColorManager.Instance == null)
        {
            Debug.LogWarning("ColorManager bulunamadı, varsayılan renk kullanılacak.");
            impactColor = Color.red;
        }
        else
        {
            impactColor = GetImpactColor();
        }

        // Işığı aktif hale getir
        if (explosionLight != null)
        {
            explosionLight.color = impactColor;
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
            explosionLight.DOKill(); 

            DOTween.To(() => explosionLight.intensity, x => explosionLight.intensity = x, 2, 0.03f).OnComplete(() =>
            {
                DOTween.To(() => explosionLight.intensity, x => explosionLight.intensity = x, 0, 0.1f);
            });
        }
    }

    private Color GetImpactColor()
    {
        switch (ColorManager.Instance.CurrentColor)
        {
            case ColorManager.ShipColor.Red:
                return Color.red;
            case ColorManager.ShipColor.Blue:
                return Color.blue;
            case ColorManager.ShipColor.Green:
                return Color.green;
            case ColorManager.ShipColor.Yellow:
                return Color.yellow;
            default:
                return Color.white;
        }
    }
}
