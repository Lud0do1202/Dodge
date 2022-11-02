using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Vector3 velocity;

    [HideInInspector]
    public bool canShootPlayer = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy") && !collision.CompareTag("Death Area"))
        {
            if (collision.CompareTag("Player") && canShootPlayer)
                collision.GetComponent<PlayerLife>().Death();

            GetComponentInParent<EnemyShoot>().bullets.Remove(gameObject);
            Destroy(gameObject);
        }    
    }
}
