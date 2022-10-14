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

    public void SetMusicVolume(int indexMusicLevel)
    {
        float volume = Mathf.Pow((indexMusicLevel + 1 - 10), 3) / 12.5f;
        musicMixer.SetFloat("Music", volume);
    }
    public void SetSoundsVolume(int indexSoundsLevel)
    {
        float volume = Mathf.Pow((indexSoundsLevel + 1 - 10), 3) / 12.5f;
        musicMixer.SetFloat("Sounds", volume);
    }
}
