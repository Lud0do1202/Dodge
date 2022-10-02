using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    // Components
    private Animator animator;
    private PlayerMovement playerMovement;

    private void Start()
    {
        animator = Player.instance.animator;
        playerMovement = Player.instance.playerMovement;
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
        animator.SetTrigger("Death");
        playerMovement.StopMovement();
    }
}
