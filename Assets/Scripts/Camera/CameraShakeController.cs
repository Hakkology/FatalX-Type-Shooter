using UnityEngine;
using DG.Tweening;
public enum ShakeType
{
    Tiny,   // Küçük asteroid
    Small,  // Orta boy düşman
    Medium, // Büyük gemi
    Huge    // Devasa gemi veya boss
}
public class CameraShakeController : MonoBehaviour
{
    private Camera _camera;
    private Vector3 originalPosition;

    void Awake()
    {
        _camera = Camera.main;
        if (_camera != null)
            originalPosition = _camera.transform.position;
    }

    public void Shake(ShakeType type)
    {
        if (_camera == null) return;

        float duration;
        float strength;
        int vibrato;
        float randomness;

        // Shake türüne göre parametreler
        switch (type)
        {
            case ShakeType.Tiny:
                duration = 0.05f;
                strength = 0.2f;
                vibrato = 10;
                randomness = 30f;
                break;

            case ShakeType.Small:
                duration = 0.1f;
                strength = 0.5f;
                vibrato = 15;
                randomness = 45f;
                break;

            case ShakeType.Medium:
                duration = 0.2f;
                strength = 1.0f;
                vibrato = 20;
                randomness = 60f;
                break;

            case ShakeType.Huge:
                duration = 0.4f;
                strength = 2.0f;
                vibrato = 30;
                randomness = 90f;
                break;

            default:
                duration = 0.1f;
                strength = 0.5f;
                vibrato = 20;
                randomness = 50f;
                break;
        }

        // Patlama sonrası kamerayı sıfırlama
        _camera.transform.DOShakePosition(duration, strength, vibrato, randomness, false, true)
            .OnComplete(() => ResetPosition());
    }

    private void ResetPosition()
    {
        Vector3 resetPosition = originalPosition;
        resetPosition.z = -5; // Arka planın görünmesi için
        _camera.transform.position = resetPosition;
    }
}
