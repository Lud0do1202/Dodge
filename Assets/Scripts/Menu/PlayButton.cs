using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayButton : MonoBehaviour
{
    public Animator animator;
    public TilemapRenderer tilemapRenderer;

    private bool isInTrigger = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isInTrigger)
            GameManager.instance.LoadSceneWithName(CurrentSceneManager.instance.nextSceneToLoad);
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
