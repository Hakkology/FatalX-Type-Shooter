using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ImpactVFXController : MonoBehaviour
{
    private Color impactColor;
    private ParticleSystem impactParticles;
    private Light2D impactLight;

    void Awake()
    {
        impactParticles = GetComponent<ParticleSystem>();
        impactLight = GetComponentInChildren<Light2D>();

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
        if (impactLight != null)
        {
            impactLight.color = impactColor;
            impactLight.enabled = true;
            impactLight.intensity = 0;
        }
    }

    public void PlayImpactEffect(Vector3 position)
    {
        transform.position = position;

        // Işık efekti
        if (impactLight != null)
        {
            FlashImpactLight();
        }

        // Partikül efekti
        if (impactParticles != null)
        {
            impactParticles.Play();
        }

        // VFX'i geri döndür
        Invoke(nameof(ReturnToPool), 0.3f);
    }

    private void ReturnToPool()
    {
        VFXPool.Instance.ReturnImpactVFX(gameObject);
    }

    private void FlashImpactLight()
    {
        if (impactLight != null)
        {
            impactLight.DOKill(); 
            DOTween.To(() => impactLight.intensity, x => impactLight.intensity = x, 5, 0.05f).OnComplete(() =>
            {
                DOTween.To(() => impactLight.intensity, x => impactLight.intensity = x, 0, 0.2f);
            });
        }
    }

    private Color GetImpactColor()
    {
        switch (ColorManager.Instance.CurrentColor)
        {
            case ThemeColor.Red:
                return Color.red;
            case ThemeColor.Blue:
                return Color.blue;
            case ThemeColor.Green:
                return Color.green;
            case ThemeColor.Yellow:
                return Color.yellow;
            default:
                return Color.white;
        }
    }

    public Color GetColor()
    {
        return impactColor;
    }
}
