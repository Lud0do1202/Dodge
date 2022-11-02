using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public void Birth()
    {
        // Animation de naissance
        GetComponent<Animator>().SetTrigger("Birth");

        // Arreter le déplacement du player
        GetComponent<PlayerMovement>().StopMovement();

        // Positionner la camera
        CameraFollow.instance.SetFocusPlayer();
    }

    public void EndBirth()
    {
        // Activer le déplacement du player
        GetComponent<PlayerMovement>().StartMovement();

        // Premier shot des ennemis
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyShoot es = go.GetComponent<EnemyShoot>();
            es.StartCoroutine(es.Shoot(es.delayFirstShot));
        }
    }

    public void Death()
    {
        // Annuler le pause mode --> reinitialiser avec le reloadScene
        CurrentSceneManager.instance.canPause = false;

        // Animation de mort
        GetComponent<Animator>().SetTrigger("Death");

        // Arreter le déplacement du player
        GetComponent<PlayerMovement>().StopMovement();

        // Desactiver le collider + attack du player 
        GetComponent<BoxCollider2D>().enabled = false;

        // Arreter les tirs des ennemies
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            go.GetComponent<EnemyShoot>().canShoot = false;
    }

    public void EndDeath()
    {
        GameManager.instance.ReloadActiveScene();
    }
}
