using UnityEngine;
using TMPro;

public class CurrentSceneManager : MonoBehaviour
{
    // Current
    [HideInInspector]
    public GameObject currentSubScene;
    [HideInInspector]
    public Transform currentPlayer;
    [HideInInspector]
    public Transform currentSpawnPlayer;

    // Previous
    [HideInInspector]
    public GameObject previousSubScene;
    [HideInInspector]
    public Transform previousPlayer;
    [HideInInspector]
    public Transform previousSpawnPlayer;

    // Start
    [Header("Start"), Space(2)]
    public GameObject startSubScene;
    public Transform startPlayer;
    public Transform startSpawnPlayer;

    // Main menu
    [Header("Main Menu"), Space(2)]
    public GameObject mainMenuSubScene;
    public Transform mainMenuPlayer;
    public Transform mainMenuSpawnPlayer;

    // Level
    [Header("Level"), Space(2)]
    public GameObject levelSubScene;
    public Transform levelPlayer;
    public Transform levelSpawnPlayer;

    // Options
    [Header("Options"), Space(2)]
    public GameObject optionsSubScene;
    public Transform optionsPlayer;
    public Transform optionsSpawnPlayer;

    // Pause
    [Header("Pause"), Space(2)]
    public GameObject pauseSubScene;
    public Transform pausePlayer;
    public Transform pauseSpawnPlayer;

    // Time for restart coroutine
    [HideInInspector]
    public float timeAtLoadPauseSubScene;

    // Countdown
    [Header("Countdown"), Space(2)]
    public GameObject panelCountdown;
    public TextMeshProUGUI textCountdown;
    public Animator countownAnimator;

    // Var
    [Space(10)]
    public string nextSceneToLoad;

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

        // Charger les elements du start sub scene
        CurrentSceneManager.instance.currentSubScene = startSubScene;
        CurrentSceneManager.instance.currentPlayer = startPlayer;
        CurrentSceneManager.instance.currentSpawnPlayer = startSpawnPlayer;
    }
}
