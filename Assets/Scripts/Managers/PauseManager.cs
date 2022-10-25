using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.instance.ButtonPressed("Pause Button");
    }
}
