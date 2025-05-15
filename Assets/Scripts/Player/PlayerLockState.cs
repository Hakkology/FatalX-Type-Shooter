using UnityEngine;

public class PlayerLockState : IPlayerState
{
    private PlayerController player;

    public PlayerLockState(PlayerController playerController)
    {
        player = playerController;
    }

    public void UpdateState()
    {
        if (Input.anyKeyDown)
        {
            string key = Input.inputString.ToLower();
            if (!string.IsNullOrEmpty(key))
            {
                SpaceObject target = FindTargetByLetter(key[0]);
                if (target != null)
                {
                    player.SetLockedTarget(target);
                    Debug.Log("Target Locked: " + target._word);
                }
            }
        }
    }

    private SpaceObject FindTargetByLetter(char letter)
    {
        foreach (var word in player.spaceObjectSpawner.spawnedWords)
        {
            if (word[0] == letter)
            {
                GameObject targetObj = GameObject.Find(word);
                if (targetObj != null)
                {
                    return targetObj.GetComponent<SpaceObject>();
                }
            }
        }
        return null;
    }
}
