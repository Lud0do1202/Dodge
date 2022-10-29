using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float speedBullet;

    public float minDelayFirstShot;
    public float maxDelayFirstShot;

    public float minDelayBetweenShots;
    public float maxDelayBetweenShots;

    [HideInInspector]
    public float delayFirstShot;
    [HideInInspector]
    public float timeAtShot;
    [HideInInspector]
    public float delayBetweenShots;

    [HideInInspector]
    public bool canShoot = true;

    [HideInInspector]
    public List<GameObject> bullets = new List<GameObject>();
    public Transform playerTransform;

    public GameObject prefabBullet;

    private void Awake()
    {
        if (playerTransform == null)
            Debug.LogError("Player not refered for this enemy");
        delayFirstShot = Random.Range(minDelayFirstShot, maxDelayFirstShot);
    }
    private void Start()
    {
        StartCoroutine(Shoot(delayFirstShot));
    }

    public IEnumerator Shoot(float delay)
    {
        delayBetweenShots = delay;
        timeAtShot = Time.time;

        yield return new WaitForSeconds(delay);

        // Vérifier si il peut tirer 
        if (canShoot)
        {
            delayBetweenShots = Random.Range(minDelayBetweenShots, maxDelayBetweenShots);
            timeAtShot = Time.time;

            // Instantier une bullet
            bullets.Add(Instantiate(prefabBullet, gameObject.transform));

            // Calculer le vecteur de reférence pour la rotation
            Vector3 vectorRef;
            if (playerTransform.position.y >= transform.position.y)
                vectorRef = Vector3.right;
            else
                vectorRef = Vector3.left;

            // Rotationner la bullet
            float angleRotation = Vector3.Angle(playerTransform.position - transform.position, vectorRef);
            bullets[bullets.Count - 1].transform.Rotate(0f, 0f, angleRotation, Space.Self);

            // Calculer la direction de la bullet
            Vector3 direction = playerTransform.position - transform.position;

            // Ajouter la force
            Vector2 force = direction.normalized * speedBullet * Time.fixedDeltaTime * 10;
            bullets[bullets.Count - 1].GetComponent<Bullet>().velocity = force;
            bullets[bullets.Count - 1].GetComponent<Rigidbody2D>().velocity = force;

            // Tirer une nouvelle bullet
            StartCoroutine(Shoot(delayBetweenShots));
        }
        
    }
}
