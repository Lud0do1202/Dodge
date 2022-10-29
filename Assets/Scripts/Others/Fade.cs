using UnityEngine;

public class Fade : MonoBehaviour
{
    public Animator animator;

    public static Fade instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one instance of Fade");
            return;
        }
        instance = this;
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
