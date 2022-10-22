using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    // Ref
    public GameObject helpWindow;

    // Singleton
    public static MainMenuManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of MainMenuManager");
            return;
        }
        instance = this;

        // Say if we have to show the help window
        if (PlayerPrefs.GetInt("ShowHelpWindow", 1) == 1)
            helpWindow.SetActive(true);
        else
            helpWindow.SetActive(false);
    }

    public void HightLighted(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetTrigger("Hightlighted");
    }
    public void NotHightLighted(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetTrigger("NotHightlighted");
    }

    // Show / Close the help window
    public void ShowHelpWindow(bool toggle)
    {
        if (toggle)
            PlayerPrefs.SetInt("ShowHelpWindow", 0);
        else
            PlayerPrefs.SetInt("ShowHelpWindow", 1);
    }
    public void CloseHelpWindow()
    {
        helpWindow.SetActive(false);
    }
}
