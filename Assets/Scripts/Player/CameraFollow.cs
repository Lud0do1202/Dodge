using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform primaryPlayer;
    public Transform secondaryPlayer;
    private bool isPrimaryPlayer = false;
    private Transform playerTransform;

    public float timeOffset;

    private Vector3 velocity = Vector3.zero;

    public static CameraFollow instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("2 instances of CameraFollow");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        ChangeFocusPlayer();
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z), ref velocity, timeOffset);
    }

    public void SetPosOnPlayer()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
    }

    public void ChangeFocusPlayer()
    {
        if (isPrimaryPlayer)
            playerTransform = secondaryPlayer;
        else
            playerTransform = primaryPlayer;

        isPrimaryPlayer = !isPrimaryPlayer;
    }
}
