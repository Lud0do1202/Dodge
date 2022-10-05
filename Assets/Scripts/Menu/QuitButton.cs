using UnityEngine;
using UnityEngine.Tilemaps;

public class QuitButton : MonoBehaviour
{
    public Animator animator;
    public TilemapRenderer tilemapRenderer;

    private bool isInTrigger = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isInTrigger)
            Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("MakeBrighter");
            tilemapRenderer.enabled = true;
        }

        isInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("MakeLessBright");
            tilemapRenderer.enabled = false;
        }

        isInTrigger = false;
    }
}
