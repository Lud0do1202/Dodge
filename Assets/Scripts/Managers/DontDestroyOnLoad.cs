using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // DontDestroyOnLoad
    public GameObject[] dontDestroyOnLoadGO;

    private void Awake()
    {
        Debug.LogWarning("Use HasKey(FirstSceneLoaded) instead of GetInt");
        if (!PlayerPrefs.HasKey("FirstSceneLoaded"))
            PlayerPrefs.SetInt("FirstSceneLoaded", 1);

        // Dont Destroy on Load
        foreach (GameObject go in dontDestroyOnLoadGO)
        {
            if (PlayerPrefs.GetInt("FirstSceneLoaded") == 1)
                DontDestroyOnLoad(go);
            else
                Destroy(go);
        }
    }
}
