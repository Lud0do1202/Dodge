using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy") && !collision.CompareTag("DeathArea"))
        {
            if (collision.CompareTag("Player"))
                Player.instance.playerDeath.Death();

            GetComponentInParent<EnemyShoot>().bullets.Remove(gameObject);
            Destroy(gameObject);
        }
        
    }
}
