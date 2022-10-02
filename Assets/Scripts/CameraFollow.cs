using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float timeOffset;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Transform transformPlayer = Player.instance.transform;
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transformPlayer.position.x, transformPlayer.position.y, transform.position.z), ref velocity, timeOffset);
    }
}
