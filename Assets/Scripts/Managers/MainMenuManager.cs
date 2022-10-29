using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Ref
    public GameObject tutoWindow;
    public Image checkmark;
    public PlayerMovement playerMovement;

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
        if (PlayerPrefs.GetInt("ShowTutoWindow", 1) == 1 && PlayerPrefs.GetInt("tutoWindowClosed", 0) == 0)
            tutoWindow.SetActive(true);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("ShowTutoWindow", 1) == 1)
           playerMovement.enabled = false;
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("tutoWindowClosed") == 0)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                ShowTutoWindow();

            else if (Input.GetKeyDown(KeyCode.Return))
                CloseTutoWindow();
        }
    }

    // Show / Close the Tuto window
    public void ShowTutoWindow()
    {
        PlayerPrefs.SetInt("ShowTutoWindow", PlayerPrefs.GetInt("ShowTutoWindow")==1?0:1);
        checkmark.enabled = !checkmark.enabled;
    }
    public void CloseTutoWindow()
    {
        playerMovement.enabled = true;

        tutoWindow.SetActive(false);
        PlayerPrefs.SetInt("tutoWindowClosed", 1);
    }
}
