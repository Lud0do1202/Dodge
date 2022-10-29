using UnityEngine;

public class Button : MonoBehaviour
{
    private bool triggerStay = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && triggerStay)
            GameManager.instance.ButtonPressed(tag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.HightLighted(gameObject);
            triggerStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.NotHightLighted(gameObject);
            triggerStay = false;
        }
    }
}
