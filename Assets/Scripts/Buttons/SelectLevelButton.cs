using UnityEngine;

public class SelectLevelButton : MonoBehaviour
{
    private bool triggerStay = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && triggerStay)
        {
            GameManager.instance.LoadScene("Select Level");
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
