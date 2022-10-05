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
        if(!CurrentSceneManager.instance.infinteRespawn && Input.GetKeyDown(KeyCode.R))
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
}
