using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject level;
    public GameObject options;

    private float timeAtChangeMode;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(level.activeInHierarchy == true)
            {
                level.SetActive(false);
                options.SetActive(true);

                // Save Time when passing from main -> options for calculating the delay between shots of the enemy
                timeAtChangeMode = Time.time;
            }
            else
            {
                options.SetActive(false);
                level.SetActive(true);

                // Restart the coroutine which stop due to the inactive gameObject
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    EnemyShoot enemyShoot = go.GetComponent<EnemyShoot>();
                    enemyShoot.StartCoroutine(enemyShoot.Shoot(enemyShoot.delayBetweenShots - (timeAtChangeMode - enemyShoot.timeAtShot)));
                }

                // Regive the velocity for bullets which was 0 due to the inactive gameObject
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Bullet"))
                    go.GetComponent<Rigidbody2D>().velocity = go.GetComponent<Bullet>().velocity;
            }

            // Changer le focus sur le player
            CameraFollow.instance.ChangeFocusPlayer();
            CameraFollow.instance.SetPosOnPlayer();
        }
    }
}
