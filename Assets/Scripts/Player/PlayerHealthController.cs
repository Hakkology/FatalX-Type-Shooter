// PlayerHealth.cs (non-MonoBehaviour)
using UnityEngine;

public class PlayerHealthController
{
    public int MaxHP { get; }
    public int CurrentHP { get; private set; }

    public PlayerHealthController(int maxHP)
    {
        MaxHP = maxHP;
        CurrentHP = maxHP;
        HUDManager.Instance.RaiseHealthChanged(CurrentHP);
    }

    public void TakeDamage(int amount)
    {
        CurrentHP = Mathf.Max(0, CurrentHP - amount);
        HUDManager.Instance.RaiseHealthChanged(CurrentHP);
    }
}
