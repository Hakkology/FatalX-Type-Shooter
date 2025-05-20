using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField] private SoundLibrary soundLibrary;
    [SerializeField] private int poolSize = 3;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup mixerGroup; 

    private AudioSource[] _audioSources;
    private int _currentSourceIndex = 0;

    public static event Action<SoundID> OnSoundRequested;

    void Awake()
    {
        var others = FindObjectsOfType<SoundController>();
        if (others.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        _audioSources = new AudioSource[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = mixerGroup;
            _audioSources[i] = source;
        }

        soundLibrary.Initialize();
        OnSoundRequested += HandleSoundRequest;
    }

    private void HandleSoundRequest(SoundID id)
    {
        var clip = soundLibrary.GetClip(id);
        if (clip == null)
        {
            Debug.LogWarning($"SoundManager: Clip not found for {id}");
            return;
        }

        var source = GetNextAvailableAudioSource();
        source.clip = clip;
        source.volume = 1f; 
        source.Play();
    }


    private float DbToLinear(float db) => Mathf.Pow(10f, db / 20f);
    void OnDestroy() => OnSoundRequested -= HandleSoundRequest;
    public static void RequestSound(SoundID id) => OnSoundRequested?.Invoke(id);

    private AudioSource GetNextAvailableAudioSource()
    {
        var source = _audioSources[_currentSourceIndex];
        _currentSourceIndex = (_currentSourceIndex + 1) % _audioSources.Length;
        return source;
    }
}
