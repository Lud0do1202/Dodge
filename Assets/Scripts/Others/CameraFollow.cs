using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float timeOffset;

    private Vector3 velocity = Vector3.zero;

    public static CameraFollow instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one instance of CameraFollow");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        SetFocusPlayer();
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(
                CurrentSceneManager.instance.currentPlayer.position.x,
                CurrentSceneManager.instance.currentPlayer.position.y, 
                transform.position.z
            ), ref velocity, timeOffset);
    }

    public void SetFocusPlayer()
    {
        // Positionner la camera sur le joueur
        transform.position = new Vector3(
                CurrentSceneManager.instance.currentPlayer.position.x,
                CurrentSceneManager.instance.currentPlayer.position.y,
                transform.position.z
            );
    }
}
