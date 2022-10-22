using System.Collections;
using UnityEditor;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
            PlayerPrefs.DeleteAll();
    }

    public void LoadSecondaryScene(string nameSecondaryScene)
    {
        StartCoroutine(_LoadSecondaryScene(nameSecondaryScene));
    }

    private IEnumerator _LoadSecondaryScene(string nameSecondaryScene)
    {
        string pathPrimaryScene = SceneManager.GetActiveScene().path;
        Debug.Log(pathPrimaryScene);

        // Charger la scène secondaire
        AsyncOperation EmptyScene = SceneManager.LoadSceneAsync("Empty Scene", LoadSceneMode.Additive);
        while (!EmptyScene.isDone)
            yield return null;
        Debug.Log(pathPrimaryScene);
        Debug.Log(SceneManager.GetSceneByName("Empty Scene").path);

        FileUtil.ReplaceFile(pathPrimaryScene, SceneManager.GetSceneByName("Empty Scene").path);

        // Charger la scène secondaire
        AsyncOperation secondaryScene = SceneManager.LoadSceneAsync(nameSecondaryScene);
        while (!secondaryScene.isDone)
            yield return null;
    }

    public void LoadPrimaryScene()
    {
        StartCoroutine(_LoadPrimaryScene());
    }

    private IEnumerator _LoadPrimaryScene()
    {
        // Charger la scène vide
        AsyncOperation sceneLoaded = SceneManager.LoadSceneAsync("Empty Scene");
        while (!sceneLoaded.isDone)
            yield return null;
    }

    public void LoadSceneSingle(string nameSceneToLoad)
    {
        SceneManager.LoadSceneAsync(nameSceneToLoad, LoadSceneMode.Single);
    }
    public void LoadSceneAdditive(string nameSceneToLoad)
    {
        SceneManager.LoadSceneAsync(nameSceneToLoad, LoadSceneMode.Additive);
    }

    public void ReloadActiveScene()
    {
        LoadSceneSingle(SceneManager.GetActiveScene().name);
    }    

    public void LoadNextLevel()
    {
        LoadSceneSingle(PlayerPrefs.GetString("NextLevel", "Level_00"));
    }

    public void SetActiveScene(string nameSceneToActive)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nameSceneToActive));
    }

    public void UnloadScene(string nameSceneToUnload)
    {
        SceneManager.UnloadSceneAsync(nameSceneToUnload);
    }

    public string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
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
