using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Lista de sonidos")]
    public Sound[] sounds;

    private void Awake()
    {
        transform.SetParent(null);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Crea un AudioSource por cada Sound
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    /// <summary>Reproduce un sonido por nombre.</summary>
    public void Play(string soundName)
    {
        var s = System.Array.Find(sounds, item => item.name == soundName);
        if (s == null)
        {
            Debug.LogWarning($"AudioManager: sonido '{soundName}' no encontrado.");
            return;
        }
        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
    }

    /// <summary>Detiene un sonido por nombre.</summary>
    public void Stop(string soundName)
    {
        var s = System.Array.Find(sounds, item => item.name == soundName);
        if (s == null) return;

        s.source.Stop();
    }

    /// <summary>Ajusta el volumen global (0 a 1).</summary>
    public void SetVolume(float newVolume)
    {
        foreach (var s in sounds)
            s.source.volume = Mathf.Clamp01(newVolume);
    }

    internal void StopAll()
    {
        foreach (var s in sounds)
            s.source.Stop();
    }
}
