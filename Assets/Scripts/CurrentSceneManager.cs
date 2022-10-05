using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    // Var
    public string nextSceneToLoad;

    // State
    public bool infinteRespawn;
    public bool activeSlowMotion;

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

    private void Start()
    {
        // Set active the slow motion mode
        if (activeSlowMotion)
            GameManager.instance.GetComponent<SlowMotion>().enabled = true;
        else
            GameManager.instance.GetComponent<SlowMotion>().enabled = false;
    }
}
