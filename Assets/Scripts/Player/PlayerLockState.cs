using System.Linq;
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
                // Hedefi bul ve kilitle
                SpaceObject target = FindTargetByLetter(key[0]);
                if (target != null && !target.Equals(null))
                {
                    player.SetLockedTarget(target);
                    target.InvokeLockedAsTarget();
                    Debug.Log("Target Locked: " + target._word);
                }
                else
                {
                    Debug.Log("No valid target found for letter: " + key[0]);
                }
            }
        }
    }

    // Harfe göre hedef bulma işlemi
    private SpaceObject FindTargetByLetter(char letter)
    {
        // Uzay nesneleri arasında geçerli olanları bul
        foreach (var spaceObject in player.spaceObjectSpawner.spawnedObjects.ToList())
        {
            if (spaceObject == null || spaceObject.Equals(null))
            {
                continue; // Geçersiz objeyi atla
            }

            SpaceObject spaceObj = spaceObject.GetComponent<SpaceObject>();
            if (spaceObj != null && spaceObj._word.Length > 0 && spaceObj._word[0] == letter)
            {
                return spaceObj;
            }
        }
        return null;
    }
}
