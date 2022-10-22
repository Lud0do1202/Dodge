using UnityEngine;

public class QuitButton : MonoBehaviour
{
    private bool triggerStay = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && triggerStay)
        {
            GameManager.instance.Quit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MainMenuManager.instance.HightLighted(gameObject);
            triggerStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MainMenuManager.instance.NotHightLighted(gameObject);
            triggerStay = false;
        }
    }
}
