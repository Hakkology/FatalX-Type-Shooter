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
        SpaceObject target = player.GetLockedTarget();

        // Eğer hedef null veya kelime bitti ise temizle
        if (target == null || target._word.Length == 0)
        {
            player.ClearLockedTarget();
            return;
        }

        if (Input.anyKeyDown)
        {
            string key = Input.inputString.ToUpper();
            if (!string.IsNullOrEmpty(key))
            {
                if (key[0] == target._word[0])
                {
                    GameStatsController.Instance.AddCorrectKeyPress();
                    SoundController.RequestSound(SoundID.CorrectType);
                    player.ShootLaserAtTarget();
                }
                else
                {
                    GameStatsController.Instance.AddWrongKeyPress();
                    SoundController.RequestSound(SoundID.FalseType);
                    // Debug.Log("Wrong letter: " + key);
                }
            }
        }
    }

}
