using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // AudioSource
    public AudioSource audioSource;

    // Mixer
    public AudioMixer musicMixer;

    // Clips
    public List<AudioClip> musicsGame;
    private int indexMusicsGame = 0;
    public AudioClip musicMainMenu;
    public AudioClip musicPause;
    public AudioClip musicCredits;

    private bool isInGame = false;

    // Singleton
    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of AudioManager");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        // Activer le son
        SetMusicVolume(GetIndexMusicLevel());
        SetSoundsVolume(GetIndexSoundsLevel());

        // Play Main Menu Clip
        PlayMainMenuMusic();

        // Mélanger l'ordre des musiques
        List<AudioClip> temp = new(musicsGame);
        int iteration = temp.Count;
        musicsGame.Clear();
        for (int i = 0; i < iteration; i++)
        {
            // Choose a index randomly
            int index = Random.Range(0, temp.Count);
            musicsGame.Add(temp[index]);
            temp.RemoveAt(index);
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (isInGame)
            {
                indexMusicsGame = (indexMusicsGame + 1) % musicsGame.Count;
                audioSource.clip = musicsGame[indexMusicsGame];
            }

            audioSource.Play();
        }
    }

    public int GetIndexMusicLevel()
    {
        return PlayerPrefs.GetInt("indexMusicLevel", 9);
    }
    public int GetIndexSoundsLevel()
    {
        return PlayerPrefs.GetInt("indexSoundsLevel", 9);
    }

    public void SetMusicVolume(int indexMusicLevel)
    {
        PlayerPrefs.SetInt("indexMusicLevel", indexMusicLevel);

        float volume = Mathf.Pow((indexMusicLevel + 1 - 10), 3) / 12.5f;
        musicMixer.SetFloat("Music", volume);
    }
    public void SetSoundsVolume(int indexSoundsLevel)
    {
        PlayerPrefs.SetInt("indexSoundsLevel", indexSoundsLevel);

        float volume = Mathf.Pow((indexSoundsLevel + 1 - 10), 3) / 12.5f;
        musicMixer.SetFloat("Sounds", volume);
    }

    public void PlayMainMenuMusic()
    {
        isInGame = false;
        audioSource.clip = musicMainMenu;
        audioSource.Play();
    }

    public void PlayGameMusic()
    {
        isInGame = true;
        audioSource.clip = musicsGame[indexMusicsGame];
        audioSource.Play();
    }
    public void PlayPauseMusic()
    {
        isInGame = false;
        audioSource.clip = musicPause;
        audioSource.Play();
    }
    public void PlayCreditsMusic()
    {
        isInGame = false;
        audioSource.clip = musicCredits;
        audioSource.Play();
    }
}
