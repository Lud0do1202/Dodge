using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of GameManager");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.LogWarning("Manual Respawn");
            ReloadActiveScene();
        }
    }

    public void ReloadActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }    

    public void LoadScene(string nameSceneToLoad)
    {
        SceneManager.LoadScene(nameSceneToLoad);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("NextLevel", "Level_00"));
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
