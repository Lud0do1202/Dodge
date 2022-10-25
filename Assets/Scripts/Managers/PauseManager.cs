using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Resume();

        else if (Input.GetKeyDown(KeyCode.O))
            GameManager.instance.LoadSubScene(CurrentSceneManager.instance.optionsSubScene, CurrentSceneManager.instance.optionsPlayer, CurrentSceneManager.instance.optionsSpawnPlayer, false);
    }

    public void Resume()
    {
        GameManager.instance.LoadSubScenePauseToLevel();
    }
}
