using UnityEngine;

public enum SoundID
{
    ButtonClick,
    Explosion,
    Jump,
    PowerUp,
    // Eklenebilir...
}

[System.Serializable]
public class SoundEntry
{
    public SoundID soundID;
    public AudioClip clip;
}