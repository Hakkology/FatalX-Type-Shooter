using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public SpaceObjectSpawner spaceObjectSpawner;
    public Transform laserSpawnPoint;
    public int startingHP = 3;
    public PlayerHealthController HealthController;
    public PlayerScoreController ScoreController;

    private SpaceObject lockedTarget;
    private Light2D shootLight;
    // State Referansları
    private IPlayerState lockState;
    private IPlayerState shootState;
    private IPlayerState currentState;




    void Start()
    {
        HealthController = new PlayerHealthController(startingHP);
        ScoreController = new PlayerScoreController();

        lockState = new PlayerLockState(this);
        shootState = new PlayerShootState(this);

        // İlk durumu ayarla
        SetState(lockState);

        shootLight = laserSpawnPoint.GetComponent<Light2D>();
        shootLight.color = GetLaserColor();
        if (shootLight != null)
        {
            shootLight.enabled = true;  
            shootLight.intensity = 0;   
        }
    }

    void Update()
    {
        currentState?.UpdateState();

        if (lockedTarget == null || lockedTarget.Equals(null))
        {
            ClearLockedTarget();
            SetState(lockState);
        }
    }

    public void SetState(IPlayerState newState)
    {
        currentState = newState;
        // Debug.Log("State Changed: " + newState.GetType().Name);
    }

    // Hedef ayarlama
    public void SetLockedTarget(SpaceObject target)
    {
        lockedTarget = target;
        target.DestroyedEvent += ClearLockedTarget;
        Shoot();
        SetState(shootState);
    }

    public SpaceObject GetLockedTarget()
    {
        return lockedTarget;
    }

    public void ClearLockedTarget()
    {
        if (lockedTarget != null)
        {
            lockedTarget.DestroyedEvent -= ClearLockedTarget;
            lockedTarget = null;
            SetState(lockState);
        }
    }

    public void ShootLaserAtTarget()
    {
        if (lockedTarget != null && !lockedTarget.Equals(null))
        {
            // Debug.Log("Shooting Laser at: " + lockedTarget._word);
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 direction = lockedTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        lockedTarget.UpdateWord();

        GameObject laser = LaserPool.Instance.GetLaser();
        var laserController = laser.GetComponent<LaserController>();
        laserController.ActivateLaser(laserSpawnPoint.position, transform.rotation);

        FlashShootLight();
        // _score.AddScore(1);
    }
    
    private void FlashShootLight()
    {
        if (shootLight != null)
        {
            shootLight.DOKill(); 

            DOTween.To(() => shootLight.intensity, x => shootLight.intensity = x, 3, 0.05f).OnComplete(() =>
            {
                DOTween.To(() => shootLight.intensity, x => shootLight.intensity = x, 0, 0.2f);
            });
        }
    }

    private Color GetLaserColor()
    {
        // Lazer rengi ColorManager'dan alınıyor
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
