using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // CurrentSceneManager
    private CurrentSceneManager csm;

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

    private void Start()
    {
        // Birth of the player
        csm = CurrentSceneManager.instance;
        csm.currentPlayer.gameObject.GetComponent<PlayerLife>().Birth();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.LogWarning("Manual Respawn");
            ReloadActiveScene();
        }

        // Charger la subScene pause
        if (Input.GetKeyDown(KeyCode.Escape) && csm.canPause)
        {
            csm.canPause = false;
            LoadSubScene(csm.pauseSubScene, csm.pausePlayer, csm.pauseSpawnPlayer, true, true);
        }
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
        SceneManager.LoadScene(PlayerPrefs.GetString("NextLevel", "Level_00"));
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
        LoadSubScene(csm.levelSubScene, csm.levelPlayer, csm.levelSpawnPlayer, false, false);
        
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
            // Resume
            case "Resume Button":
                LoadSubScenePauseToLevel();
                break;

            // Play
            case "PlayButton":
                LoadNextLevel();
                break;

            // Options
            case "OptionsButton":
                LoadSubScene(csm.optionsSubScene, csm.optionsPlayer, csm.optionsSpawnPlayer, false, true);
                break;

            // Not found
            default:
                Debug.LogWarning("Button with tag (" + tagButton + ") not found");
                break;
        }
    }
}
