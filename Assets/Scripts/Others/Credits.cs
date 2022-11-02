using UnityEngine;

public class Credits : MonoBehaviour
{
    public void EndCredits()
    {
        GameManager.instance.LoadScene("Main Menu");
    }
}
