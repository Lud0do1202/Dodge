using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class SelectLevelManager : MonoBehaviour
{
    public List<GameObject> locks;
    public List<GameObject> levels;

    public Color colorCurrentLevelPlayed;
    public Color colorLocks;

    //[HideInInspector]
    public Vector2 posLevelButtonPressed;

    private int indexLastLevelReached, indexCurrentLevelPlayed;

    public static SelectLevelManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of SelectLevelManager");
            return;
        }
        instance = this;

        indexLastLevelReached = PlayerPrefs.GetString("LastLevelReached", "Level_00").CompareTo("Level_00");
        indexCurrentLevelPlayed = PlayerPrefs.GetString("NextLevel", "Level_00").CompareTo("Level_00");

        // Enable Levels
        for (int i = 0; i <= indexLastLevelReached; i++)
        {
            // Set active levels
            levels[i].SetActive(true);

            // Change the color of the currentLevelPlayed
            if (i == indexCurrentLevelPlayed)
            {
                levels[i].GetComponent<Tilemap>().color = colorCurrentLevelPlayed;
                levels[i].GetComponentInChildren<Light2D>().color = colorCurrentLevelPlayed;
            }
        }

        // Set active locks
        for (int i = indexLastLevelReached + 1; i < locks.Count; i++)
            locks[i].SetActive(true);
    }
}
