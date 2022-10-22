using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Var
    public float speed;

    // Var for movement
    private float horMovement, vertMovement;
    private Vector3 velocity = Vector3.zero;

    // Components player
    private SpriteRenderer playerSpriteRenderer;
    private Animator playerAnimator;
    private Rigidbody2D playerRb;

    private void Start()
    {
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerRb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Déplacement horizontal
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {
            horMovement = -speed * Time.fixedDeltaTime;
            playerSpriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            horMovement = speed * Time.fixedDeltaTime;
            playerSpriteRenderer.flipX = false;
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
            playerAnimator.SetBool("IsRunning", false);
        else
            playerAnimator.SetBool("IsRunning", true);
    }

    private void FixedUpdate()
    {
        playerRb.velocity = Vector3.SmoothDamp(playerRb.velocity, new Vector3(horMovement, vertMovement), ref velocity, .05f);
    }

    public void StopMovement()
    {
        playerRb.velocity = Vector2.zero;
        this.enabled = false;
    }

    public void StartMovement()
    {
        this.enabled = true;
    }
}
