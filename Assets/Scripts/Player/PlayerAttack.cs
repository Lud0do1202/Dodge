using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Component
    private Animator playerAnimator;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            Attack(collision);
    }

    public void Attack(Collider2D collision)
    {
        // Positionner le player sur l'ennemi
        transform.position = collision.transform.position;

        // Arreter tout movement et animation
        playerMovement.StopMovement();
        playerAnimator.SetTrigger("Attack");

        // Animaion de mort de l'ennemi
        collision.GetComponent<Animator>().SetTrigger("Death");

        // Désactiver son collider et les tirs
        collision.GetComponent<BoxCollider2D>().enabled = false;
        collision.GetComponent<EnemyShoot>().canShoot = false;

        // Retirer le nombre d'ennemis dans la current scene
        CurrentSceneManager.instance.nbEnemies--;

        // Desactiver le trigger si c'était le dernier ennemy afin de stopper une bullet sans mourir
        if(CurrentSceneManager.instance.nbEnemies == 0)
            GetComponent<BoxCollider2D>().enabled = false;
    }

    public void EndAttack()
    {
        playerMovement.StartMovement();
    }
}
