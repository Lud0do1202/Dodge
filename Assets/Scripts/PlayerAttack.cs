using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Component
    private Animator playerAnimator;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerAnimator = Player.instance.animator;
        playerMovement = Player.instance.playerMovement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            Attack(collision);
    }

    public void Attack(Collider2D collision)
    {
        transform.position = collision.transform.position;
        playerMovement.StopMovement();
        playerAnimator.SetTrigger("Attack");

        collision.GetComponent<BoxCollider2D>().enabled = false;
        collision.GetComponent<Animator>().SetTrigger("Death");
    }

    public void EndAttack()
    {
        playerMovement.StartMovement();
    }
}
