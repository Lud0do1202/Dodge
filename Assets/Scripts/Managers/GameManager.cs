using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Ref
    public GameObject helpWindow; 

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

        if (PlayerPrefs.GetInt("ShowHelpWindow", 1) == 1)
            helpWindow.SetActive(true);
        else
            helpWindow.SetActive(false);
    }

    private void Update()
    {
        if(!CurrentSceneManager.instance.autoRespawn && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Manual Respawn");
            ReloadActiveScene();
        }
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

    public void ShowHelpWindow(bool toggle)
    {
        if (toggle)
            PlayerPrefs.SetInt("ShowHelpWindow", 0);
        else
            PlayerPrefs.SetInt("ShowHelpWindow", 1);
    }

    public void CloseHelpWindow()
    {
        helpWindow.SetActive(false);
    }
}
