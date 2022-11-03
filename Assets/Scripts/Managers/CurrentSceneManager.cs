using UnityEngine;
using TMPro;
using System.Collections;

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
    public bool lastLevel;
    public string nextSceneToLoad;
    [HideInInspector]
    public int nbEnemies;

    // State
    public bool canPause;
    public bool dontFollowPlayer;

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
        currentSubScene = startSubScene;
        currentPlayer = startPlayer;
        currentSpawnPlayer = startSpawnPlayer;
    }
    private void Start()
    {
        // Compter le nombre d'ennemis
        nbEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // S'il n'y a pas d'ennemi -> nbEnemies = -1 pour pas rentrer dans la condition du update
        if (nbEnemies == 0)
            nbEnemies = -1;

        // Birth of the player
        currentPlayer.gameObject.GetComponent<PlayerLife>().Birth();

        // Fade
        Fade.instance.FadeIn();
    }
    
    private void Update()
    {
        if(nbEnemies == 0)
        {
            nbEnemies--;
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        // Attendre avant de charger soit la scene de credit ou le prochain niveau
        yield return new WaitForSeconds(0.5f);

        if (!lastLevel)
            GameManager.instance.LoadNextLevel(nextSceneToLoad);
        else
            GameManager.instance.ButtonPressed("Credits Button");

    }


}
