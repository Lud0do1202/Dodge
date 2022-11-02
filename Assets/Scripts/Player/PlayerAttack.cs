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
        collision.GetComponent<EnemyShoot>().canShoot = false;

        // Retirer le nombre d'ennemis dans la current scene
        CurrentSceneManager.instance.nbEnemies--;

        // Désactiver le canShootPlayer
        if (CurrentSceneManager.instance.nbEnemies == 0)
            foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
                bullet.GetComponent<Bullet>().canShootPlayer = false;
        
    }

    public void EndAttack()
    {
        playerMovement.StartMovement();
    }
}
