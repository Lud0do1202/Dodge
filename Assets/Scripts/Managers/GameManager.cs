using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // DontDestroyOnLoad
    public GameObject[] dontDestroyOnLoadGO;

    // Singleton
    public static GameManager instance;
    private CurrentSceneManager csm;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of GameManager");
            return;
        }
        instance = this;

        // Dont Destroy on Load
        foreach (GameObject go in dontDestroyOnLoadGO)
            DontDestroyOnLoad(go);
    }

    private void Start()
    {
        // Singleton
        csm = CurrentSceneManager.instance;

        // Birth of the player
        csm.currentPlayer.gameObject.GetComponent<PlayerLife>().Birth();
    }

    private void Update()
    {
        // Charger la subScene pause
        if (Input.GetKeyDown(KeyCode.Escape) && csm.canPause)
        {
            csm.canPause = false;
            LoadSubScene(csm.pauseSubScene, csm.pausePlayer, csm.pauseSpawnPlayer, true, true);
        }
    }

    private void OnApplicationQuit()
    {
        // Réinitialiser le fait que la fenetre de tuto n'est plus fermée
        PlayerPrefs.SetInt("tutoWindowClosed", 0);
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
        SceneManager.LoadScene(PlayerPrefs.GetString("NextLevel", "Level_01"));
    }

    public void LoadSubScene(GameObject subSceneToLoad, Transform playerTransform, Transform spawnPlayerTransform, bool saveTimeAtLoadSubScene, bool birthAnimation)
    {
        CurrentSceneManager csm = CurrentSceneManager.instance;

        // Charger la subscene
        csm.currentSubScene.SetActive(false);
        subSceneToLoad.SetActive(true);

        // Save le time au moment de la pause pour pouvoir relancer les coroutines correctement
        if(saveTimeAtLoadSubScene)
            csm.timeAtLoadPauseSubScene = Time.time;

        // replacer le joueur
        if(spawnPlayerTransform != null)
            playerTransform.position = spawnPlayerTransform.position;

        // Changer previous/current 
        csm.previousSubScene = csm.currentSubScene;
        csm.previousPlayer = csm.currentPlayer;
        csm.previousSpawnPlayer = csm.currentSpawnPlayer;

        csm.currentSubScene = subSceneToLoad;
        csm.currentPlayer = playerTransform;
        csm.currentSpawnPlayer = spawnPlayerTransform;

        // Repositionner la camera
        CameraFollow.instance.SetFocusPlayer();

        // Naissance du joueur
        if (birthAnimation)
            playerTransform.gameObject.GetComponent<PlayerLife>().Birth();

        // Fade In
        Fade.instance.FadeIn();
    }

    public void LoadSubScenePauseToLevel()
    {
        StartCoroutine(_LoadSubScenePauseToLevel());
    }
    private IEnumerator _LoadSubScenePauseToLevel()
    {
        // Charger la subScene 
        LoadSubScene(csm.levelSubScene, csm.levelPlayer, null, false, false);
        
        // Charger le panel du countdown
        csm.panelCountdown.SetActive(true);

        // Mettre le jeu en pause
        Time.timeScale = 0f;

        // Faire le countdown
        for (int i = 3; i >= 1; i--)
        {
            csm.textCountdown.text = i.ToString();
            csm.countownAnimator.SetTrigger("Increasing");
            yield return new WaitForSecondsRealtime(1f);
        }
        csm.panelCountdown.SetActive(false);

        // remettre le timeScale normal
        Time.timeScale = 1f;

        // Relancer la coroutine pour les tirs des ennemis
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyShoot enemyShoot = go.GetComponent<EnemyShoot>();
            enemyShoot.StartCoroutine(enemyShoot.Shoot(enemyShoot.delayBetweenShots - (csm.timeAtLoadPauseSubScene - enemyShoot.timeAtShot)));
        }

        // Redonner la vélocité aux balles
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Bullet"))
            go.GetComponent<Rigidbody2D>().velocity = go.GetComponent<Bullet>().velocity;

        // Dire qu'il peut denouveau faire pause
        csm.canPause = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    // BUTTONS
    public void HightLighted(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetTrigger("Hightlighted");
    }
    public void NotHightLighted(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetTrigger("NotHightlighted");
    }

    public void ButtonPressed(string tagButton)
    {
        switch (tagButton)
        {
            // Play
            case "Play Button":
                LoadNextLevel();
                break;

            // Quit
            case "Quit Button":
                Application.Quit();
                break;

            // Options
            case "Options Button":
                LoadSubScene(csm.optionsSubScene, csm.optionsPlayer, csm.optionsSpawnPlayer, false, true);
                break;

            // Select Level
            case "Select Level Button":
                LoadScene("Select Level");
                break;

            // Reduce Music
            case "Reduce Music Button":
                OptionsManager.instance.ReduceMusicLevel();
                break;

            // Increase Music
            case "Increase Music Button":
                OptionsManager.instance.IncreaseMusicLevel();
                break;

            // Reduce Sounds
            case "Reduce Sounds Button":
                OptionsManager.instance.ReduceSoundsLevel();
                break;

            // Increase Sounds
            case "Increase Sounds Button":
                OptionsManager.instance.IncreaseSoundsLevel();
                break;

            // Fullscreen
            case "Fullscreen Button":
                OptionsManager.instance.ChangeFullscreen();
                break;

            // 720x480
            case "720x480 Button":
                OptionsManager.instance.ChangeResolution(720, 480, 0);
                break;

            // 1080x720
            case "1080x720 Button":
                OptionsManager.instance.ChangeResolution(1080, 720, 1);
                break;

            // 1920x1080
            case "1920x1080 Button":
                OptionsManager.instance.ChangeResolution(1920, 1080, 2);
                break;

            // 4096x2304
            case "4096x2304 Button":
                OptionsManager.instance.ChangeResolution(4096, 2304, 3);
                break;

            // Credits
            case "Credits Button":
                LoadScene("Credits");
                break;

            // Back
            case "Back Button":
                LoadSubScene(csm.previousSubScene, csm.previousPlayer, csm.previousSpawnPlayer, false, true);
                break;

            // Resume
            case "Resume Button":
                LoadSubScenePauseToLevel();
                break;

            // Main Menu
            case "Main Menu Button":
                LoadScene("Main Menu");
                break;

            // Not found
            default:
                Debug.LogWarning("Button with tag (" + tagButton + ") not found");
                break;
        }
    }
}
