using UnityEngine;

public class FullscreenButton : MonoBehaviour
{
    private bool triggerStay = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && triggerStay)
        {
            OptionsManager.instance.ChangeFullscreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OptionsManager.instance.HightLighted(gameObject);
            triggerStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OptionsManager.instance.NotHightLighted(gameObject);
            triggerStay = false;
        }
    }
}
