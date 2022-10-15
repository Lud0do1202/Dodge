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
        if(!CurrentSceneManager.instance.autoRespawn && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Manual Respawn");
            ReloadActiveScene();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
            PlayerPrefs.DeleteAll();
    }

    public void ReloadActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadSceneWithName(string nameSceneToLoad)
    {
        SceneManager.LoadScene(nameSceneToLoad);
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
