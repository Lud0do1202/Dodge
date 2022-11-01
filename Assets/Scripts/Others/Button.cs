using UnityEngine;

public class Button : MonoBehaviour
{
    private bool triggerStay = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && triggerStay)
        {
            // If it's a level button -> save position of its parent for knowing which level button we pressed
            if (tag == "Level Button")
                SelectLevelManager.instance.posLevelButtonPressed = transform.parent.transform.position;
            GameManager.instance.ButtonPressed(tag);
        }
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
