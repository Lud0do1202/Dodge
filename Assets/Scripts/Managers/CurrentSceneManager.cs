using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    // Var
    public string nextSceneToLoad;

    // State
    public bool infinteRespawn;

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
}
