using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeathArea"))
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("Death");
    }
}
