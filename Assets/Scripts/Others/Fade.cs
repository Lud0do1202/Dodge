using UnityEngine;

public class Fade : MonoBehaviour
{
    public Animator animator;

    public float timeAnimation;

    public static Fade instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one instance of Fade");
            return;
        }
        instance = this;

        Debug.LogWarning("TimeAnimation needed in LoadNextScene but it's hard-code");
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }
}
