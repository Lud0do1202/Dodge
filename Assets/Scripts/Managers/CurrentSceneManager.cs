using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    // Var
    public string nextSceneToLoad;
    public string backSceneToload;

    // State
    public bool autoRespawn;
    public bool canPause;

    // Singleton
    public static CurrentSceneManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of CurrentSceneManager");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if (Time.timeScale == 1f)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;
        }
    }
}
