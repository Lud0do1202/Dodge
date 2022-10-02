using UnityEngine;

// *****************************

public class PlayerMovement : MonoBehaviour
{
    // Var
    public float speed;

    // Var for movement
    private float horMovement, vertMovement;
    private Vector3 velocity = Vector3.zero;

    // Components player
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        // Init component of the player
        spriteRenderer = Player.instance.spriteRenderer;
        animator = Player.instance.animator;
        rb = Player.instance.rb;
    }

    private void Update()
    {
        // Déplacement horizontal
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {
            horMovement = -speed * Time.fixedDeltaTime;
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            horMovement = speed * Time.fixedDeltaTime;
            spriteRenderer.flipX = false;
        }
        else
            horMovement = 0f;

        // Déplacement vertical
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
            vertMovement = speed * Time.fixedDeltaTime;
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            vertMovement = -speed * Time.fixedDeltaTime;
        else
            vertMovement = 0f;

        // Animation de RUN
        if (horMovement == 0f && vertMovement == 0f)
            animator.SetBool("IsRunning", false);
        else
            animator.SetBool("IsRunning", true);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.SmoothDamp(rb.velocity, new Vector3(horMovement, vertMovement), ref velocity, .05f);
    }
}
