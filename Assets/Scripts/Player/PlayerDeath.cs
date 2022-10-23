using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public void Death()
    {
        // Animation de mort
        GetComponent<Animator>().SetTrigger("Death");

        // Arreter le déplacement du player
        GetComponent<PlayerMovement>().StopMovement();

        // Desactiver le collider du player
        GetComponent<BoxCollider2D>().enabled = false;

        // Arreter les tirs des ennemies
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            go.GetComponent<EnemyShoot>().canShoot = false;
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
