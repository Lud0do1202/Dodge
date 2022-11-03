using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public bool creditsScene;
    private float saveSpeed;

    private void Awake()
    {
        if (creditsScene)
            saveSpeed = GetComponent<PlayerCredits>().speed;
    }

    public void Birth()
    {
        // Animation de naissance
        GetComponent<Animator>().SetTrigger("Birth");

        if (!creditsScene)
            // Arreter le déplacement du player
            GetComponent<PlayerMovement>().StopMovement();
            
        // Positionner la camera
        CameraFollow.instance.SetFocusPlayer();
    }

    public void EndBirth()
    {
        if (creditsScene)
        {
            GetComponent<PlayerCredits>().isDead = false;
            GetComponent<PlayerCredits>().speed = saveSpeed;
            GetComponent<PlayerCredits>().animator.SetBool("IsRunning", true);

            GetComponent<BoxCollider2D>().enabled = true;
        }

        // Premier shot des ennemis
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyShoot es = go.GetComponent<EnemyShoot>();
            if (creditsScene)
               es.canShoot = true;
            es.StartCoroutine(es.Shoot(es.delayFirstShot));
        }

        if(!creditsScene)
            // Activer le déplacement du player
            GetComponent<PlayerMovement>().StartMovement();
    }

    public void Death()
    {
        // Annuler le pause mode --> reinitialiser avec le reloadScene
        CurrentSceneManager.instance.canPause = false;

        // Animation de mort
        GetComponent<Animator>().SetTrigger("Death");

        if (creditsScene)
            GetComponent<PlayerCredits>().isDead = true;

        if(!creditsScene)
            // Arreter le déplacement du player
            GetComponent<PlayerMovement>().StopMovement();

        // Desactiver le collider
        GetComponent<BoxCollider2D>().enabled = false;

        // Arreter les tirs des ennemies
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            go.GetComponent<EnemyShoot>().canShoot = false;
    }

    public void EndDeath()
    {
        if (creditsScene)
        {
            transform.position = GetComponent<PlayerCredits>().spawn.position;
            GetComponent<PlayerCredits>().indexPoints = 0;
            Birth();
        }
        else
        {
            if(CurrentSceneManager.instance.nbEnemies != 0)
                GameManager.instance.ReloadActiveScene();
        }
    }
}
