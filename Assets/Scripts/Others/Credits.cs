using UnityEngine;

public class Credits : MonoBehaviour
{
    public void EndCredits()
    {
        GameManager.instance.ButtonPressed("Main Menu Button");
    }
}
