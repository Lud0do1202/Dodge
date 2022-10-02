using UnityEngine;

public class Player : MonoBehaviour
{
    // Components
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public Animator animator;

    // Scripts
    public PlayerMovement playerMovement;

    // State

    // Singleton
    public static Player instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one instance of Player");
            return;
        }
        instance = this;
    }
}
