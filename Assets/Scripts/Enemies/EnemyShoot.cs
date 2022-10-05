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
    public bool canShoot = true;

    [HideInInspector]
    public List<GameObject> bullets = new List<GameObject>();
    private Transform playerTransform;

    public GameObject prefabBullet;

    private void Start()
    {
        StartCoroutine(FirstShot());
        playerTransform = Player.instance.transform;
    }

    public IEnumerator Shoot()
    {
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
        Vector2 force = direction.normalized * speedBullet * Time.fixedDeltaTime * 1000;
        bullets[bullets.Count - 1].GetComponent<Rigidbody2D>().AddForce(force);

        // Attendre x secondes entre 2 bullets
        yield return new WaitForSeconds(Random.Range(minDelayBetweenShots, maxDelayBetweenShots));

        // Tirer une nouvelle bullet
        if(canShoot)
            StartCoroutine(Shoot());
    }

    public IEnumerator FirstShot()
    {
        yield return new WaitForSeconds(Random.Range(minDelayFirstShot, maxDelayFirstShot));
        if(canShoot)
            StartCoroutine(Shoot());
    }
}
