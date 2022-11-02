using System.Collections;
using UnityEngine;

public class PlayerCredits : MonoBehaviour
{
    public float speed;
    [HideInInspector]
    public bool isDead;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    public Animator animator;

    public Transform spawn;
    public Transform[] points;
    [HideInInspector]
    public int indexPoints = 0;

    private void Start()
    {
        isDead = true;
    }
    private void Update()
    {
        if (isDead)
            speed = 0f;

        if ((speed != 0f) &&
            ((indexPoints == 0 && transform.position.y > points[0].position.y) ||
            (indexPoints == 1 && transform.position.y < points[1].position.y)))
            StartCoroutine(SwitchPoints());
    }

    private IEnumerator SwitchPoints()
    {
        float saveSpeed = speed;
        speed = 0f;
        animator.SetBool("IsRunning", false);
        yield return new WaitForSeconds(0.25f);
        speed = -saveSpeed;
        indexPoints = (indexPoints + 1) % 2;
        animator.SetBool("IsRunning", true);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.SmoothDamp(rb.velocity, new Vector3(0, speed * Time.fixedDeltaTime), ref velocity, .05f);
    }
}
