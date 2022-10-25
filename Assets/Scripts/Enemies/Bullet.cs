using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Vector3 velocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy") && !collision.CompareTag("DeathArea"))
        {
            if (collision.CompareTag("Player"))
                collision.GetComponent<PlayerLife>().Death();

            GetComponentInParent<EnemyShoot>().bullets.Remove(gameObject);
            Destroy(gameObject);
        }    
    }
}
