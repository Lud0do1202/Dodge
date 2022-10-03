using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Component
    private Animator animator;
    private PlayerMovement playerMovement;

    private void Start()
    {
        animator = Player.instance.animator;
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
        animator.SetTrigger("Attack");

        Destroy(collision.gameObject);
    }

    public void EndAttack()
    {
        playerMovement.StartMovement();
    }
}
