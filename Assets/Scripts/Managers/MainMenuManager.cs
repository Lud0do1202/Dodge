using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Ref
    public GameObject tutoWindow;
    public Image checkmark;
    public GameObject player;

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
        Debug.LogWarning("Must use PlayerPrefs.HasKey + SetInt instead of getInt with a default value");
        if (!PlayerPrefs.HasKey("ShowTutoWindow"))
            PlayerPrefs.SetInt("ShowTutoWindow", 1);
        if (!PlayerPrefs.HasKey("TutoWindowClosed"))
            PlayerPrefs.SetInt("TutoWindowClosed", 0);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("ShowTutoWindow") == 1 && PlayerPrefs.GetInt("TutoWindowClosed") == 0)
        {
            tutoWindow.SetActive(true);
            player.SetActive(false);
        }
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("TutoWindowClosed") == 0)
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
        PlayerPrefs.SetInt("TutoWindowClosed", 1);
    }
}
