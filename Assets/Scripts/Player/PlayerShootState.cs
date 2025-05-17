using UnityEngine;

public class PlayerShootState : IPlayerState
{
    private PlayerController player;

    public PlayerShootState(PlayerController playerController)
    {
        player = playerController;
    }

    public void UpdateState()
    {
        if (player.GetLockedTarget() == null)
        {
            player.ClearLockedTarget();
            return;
        }

        if (Input.anyKeyDown)
        {
            string key = Input.inputString.ToLower();
            if (!string.IsNullOrEmpty(key))
            {
                SpaceObject target = player.GetLockedTarget();

                if (target._word.Length > 0 && key[0] == target._word[0])
                {
                    player.ShootLaserAtTarget();
                }
                else
                {
                    Debug.Log("Wrong letter: " + key);
                }
            }
        }
    }
}
