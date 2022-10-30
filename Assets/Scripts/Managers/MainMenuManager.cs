using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Ref
    public GameObject tutoWindow;
    public Image checkmark;
    public GameObject player;

    private bool tutoWindowClosed = true;

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

        // Say if we have to show the Tuto window
        Debug.LogWarning("Use HasKey(ShowTutoWindow) instead of GetInt");
        if (!PlayerPrefs.HasKey("ShowTutoWindow"))
            PlayerPrefs.SetInt("ShowTutoWindow", 1);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("ShowTutoWindow") == 1 && PlayerPrefs.GetInt("FirstSceneLoaded") == 1)
        {
            tutoWindow.SetActive(true);
            player.SetActive(false);
            tutoWindowClosed = false;
        }
    }

    private void Update()
    {
        if(!tutoWindowClosed)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                ToggleShowTutoWindow();

            else if (Input.GetKeyDown(KeyCode.Return))
                CloseTutoWindow();
        }
    }

    // Show / Close the Tuto window
    public void ToggleShowTutoWindow()
    {
        PlayerPrefs.SetInt("ShowTutoWindow", PlayerPrefs.GetInt("ShowTutoWindow")==1?0:1);
        checkmark.enabled = !checkmark.enabled;
    }
    public void CloseTutoWindow()
    {
        player.SetActive(true);
        player.GetComponent<PlayerLife>().Birth();
        tutoWindow.SetActive(false);
        tutoWindowClosed = true;
    }
}
