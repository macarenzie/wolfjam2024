using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton

    public AudioSource musicSource; // Main music source
    public AudioSource sfxSource;   // Sound effects source

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public List<Sound> musicTracks; //this probably will not need to be a list so you can just get a single reference
    public List<Sound> soundEffects;

    private Dictionary<string, AudioClip> musicDict;
    private Dictionary<string, AudioClip> sfxDict;

    void Awake()
    {
        // Singleton pattern
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);

        // Initialize dictionaries for quick access
        musicDict = new Dictionary<string, AudioClip>();
        sfxDict = new Dictionary<string, AudioClip>();

        foreach (Sound music in musicTracks) musicDict[music.name] = music.clip;
        foreach (Sound sfx in soundEffects) sfxDict[sfx.name] = sfx.clip;
    }

    public void PlayMusic(string trackName, bool loop = true)
    {
        if (musicDict.TryGetValue(trackName, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music track '{trackName}' not found!");
        }
    }

    public void PlaySFX(string sfxName)
    {
        if (sfxDict.TryGetValue(sfxName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound effect '{sfxName}' not found!");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
