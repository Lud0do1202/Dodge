using UnityEngine;

public class BackButton : MonoBehaviour
{
    public bool saveStateMainSubScene;

    public Transform spawnMainSubScene;
    public Transform playerMainSubScene;

    public GameObject mainSubScene;
    public GameObject optionsSubScene;

    private float timeAtChangeMode;

    private bool triggerStay = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && triggerStay)
        {
            //OptionsManager.instance.NotHightLighted(gameObject);
            //triggerStay = false;

            if (mainSubScene.activeInHierarchy == true)
            {
                mainSubScene.SetActive(false);
                optionsSubScene.SetActive(true);

                // Save Time when passing from main -> options for calculating the delay between shots of the enemy
                if(saveStateMainSubScene)
                    timeAtChangeMode = Time.time;
            }
            else
            {
                mainSubScene.SetActive(true);
                optionsSubScene.SetActive(false);

                // Restart the coroutine which stop due to the inactive gameObject
                if (saveStateMainSubScene)
                {
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        EnemyShoot enemyShoot = go.GetComponent<EnemyShoot>();
                        enemyShoot.StartCoroutine(enemyShoot.Shoot(enemyShoot.delayBetweenShots - (timeAtChangeMode - enemyShoot.timeAtShot)));
                    }

                    // Regive the velocity for bullets which was 0 due to the inactive gameObject
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Bullet"))
                        go.GetComponent<Rigidbody2D>().velocity = go.GetComponent<Bullet>().velocity;
                }
            }

            // Replacer le player
            if(!saveStateMainSubScene)
                playerMainSubScene.position = spawnMainSubScene.position;

            // Changer le focus sur le player
            CameraFollow.instance.ChangeFocusPlayer();
            CameraFollow.instance.SetPosOnPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OptionsManager.instance.HightLighted(gameObject);
            triggerStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OptionsManager.instance.NotHightLighted(gameObject);
            triggerStay = false;
        }
    }
}
