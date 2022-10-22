using UnityEngine;

public class Reso4096x2304Button : MonoBehaviour
{
    private bool triggerStay = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && triggerStay)
        {
            OptionsManager.instance.ChangeResolution(4096, 2304, 3);
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
