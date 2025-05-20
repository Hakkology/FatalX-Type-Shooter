using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundLibrary soundLibrary;
    [SerializeField] private int poolSize = 3;

    private AudioSource[] _audioSources;
    private int _currentSourceIndex = 0;

    public static event Action<SoundID> OnSoundRequested;

    void Awake()
    {
        // Eğer sahnede başka bir aktif SoundManager varsa kendini yok et
        var others = FindObjectsOfType<SoundManager>();
        if (others.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // AudioSource havuzu
        _audioSources = new AudioSource[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            _audioSources[i] = gameObject.AddComponent<AudioSource>();
        }

        soundLibrary.Initialize();
        OnSoundRequested += HandleSoundRequest;
    }

    void OnDestroy()
    {
        OnSoundRequested -= HandleSoundRequest;
    }

    private void HandleSoundRequest(SoundID id)
    {
        var clip = soundLibrary.GetClip(id);
        if (clip != null)
        {
            var source = GetNextAvailableAudioSource();
            source.clip = clip;
            source.Play();
        }
        else
        {
            Debug.LogWarning($"SoundManager: Clip not found for {id}");
        }
    }

    private AudioSource GetNextAvailableAudioSource()
    {
        var source = _audioSources[_currentSourceIndex];
        _currentSourceIndex = (_currentSourceIndex + 1) % _audioSources.Length;
        return source;
    }

    public static void RequestSound(SoundID id)
    {
        OnSoundRequested?.Invoke(id);
    }
}
