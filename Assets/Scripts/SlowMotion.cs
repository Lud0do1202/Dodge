using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotion : MonoBehaviour
{
    public float timeSlowTime;
    public float timeBeforeNextSloxTime;
    public float timeScale;

    private bool canSlowTime = true;

    public Animator animator;
    public Image SlowMotionBar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canSlowTime)
        {
            StartCoroutine(SlowTime());
        }
    }

    IEnumerator SlowTime()
    {
        canSlowTime = false;
        SlowMotionBar.enabled = false;
        Time.timeScale = timeScale;
        yield return new WaitForSecondsRealtime(timeSlowTime);
        Time.timeScale = 1f;
        SlowMotionBar.enabled = true;
        animator.SetTrigger("StartSlowMotionBar");
        yield return new WaitForSecondsRealtime(timeBeforeNextSloxTime);
        canSlowTime = true;
    }
}
