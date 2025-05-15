using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpaceObjectSpawner spaceObjectSpawner;
    private SpaceObject lockedTarget;

    // State Referansları
    private IPlayerState lockState;
    private IPlayerState shootState;
    private IPlayerState currentState;

    void Start()
    {
        lockState = new PlayerLockState(this);
        shootState = new PlayerShootState(this);

        // İlk durumu ayarla
        SetState(lockState);
    }

    void Update()
    {
        currentState?.UpdateState();
        if (lockedTarget == null)
        {
            SetState(lockState);
        }
    }

    public void SetState(IPlayerState newState)
    {
        currentState = newState;
        Debug.Log("State Changed: " + newState.GetType().Name);
    }

    // Hedef ayarlama
    public void SetLockedTarget(SpaceObject target)
    {
        lockedTarget = target;
        SetState(shootState); 
    }

    public SpaceObject GetLockedTarget()
    {
        return lockedTarget;
    }

    public void ClearLockedTarget()
    {
        lockedTarget = null;
        SetState(lockState); // Hedef kaybolunca kilitlenme moduna geç
    }

    public void ShootLaserAtTarget()
    {
        if (lockedTarget != null)
        {
            Debug.Log("Shooting Laser at: " + lockedTarget._word);
            bool destroyed = lockedTarget.TakeDamage(); 

            if (destroyed)
            {
                ClearLockedTarget();
            }
        }
    }
}
