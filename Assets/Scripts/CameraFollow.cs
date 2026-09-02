using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float cameraOffsetStrength;
    [SerializeField] private float smoothTime;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform target;

    void Update()
    {
        Vector3 targetPosition = target.position + new Vector3(0, 0, cameraOffsetStrength);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
