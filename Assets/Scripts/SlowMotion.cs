using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotion : MonoBehaviour
{
    public float timeSlowMotion;
    public float timeBeforeNextSlowMotion;
    public float timeScale;

    private bool canSlowMotion = true;

    public Animator animator;
    public Image SlowMotionBar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canSlowMotion)
        {
            StartCoroutine(SlowTime());
        }
    }

    IEnumerator SlowTime()
    {
        canSlowMotion = false;
        SlowMotionBar.enabled = false;
        Time.timeScale = timeScale;
        yield return new WaitForSecondsRealtime(timeSlowMotion);
        Time.timeScale = 1f;
        SlowMotionBar.enabled = true;
        animator.SetTrigger("StartSlowMotionBar");
        yield return new WaitForSecondsRealtime(timeBeforeNextSlowMotion);
        canSlowMotion = true;
    }
}
