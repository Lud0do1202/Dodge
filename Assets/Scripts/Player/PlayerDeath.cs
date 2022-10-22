using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    // Components
    private Animator playerAnimator;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeathArea"))
        {
            Death();
        }
    }

    public void Death()
    {
        playerAnimator.SetTrigger("Death");
        playerMovement.StopMovement();
    }

    public void EndDeath()
    {
        if (CurrentSceneManager.instance.autoRespawn)
        {
            GameManager.instance.ReloadActiveScene();
        }
        else
        {
            Debug.Log("Game Over");

        }
    }
}
