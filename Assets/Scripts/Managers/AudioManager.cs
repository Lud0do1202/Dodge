using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer;

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
}
