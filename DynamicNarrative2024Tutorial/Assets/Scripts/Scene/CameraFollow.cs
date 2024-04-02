using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // 要跟随的目标物体
    public float smoothSpeed = 0.125f;  // 相机移动的平滑度
    public Vector3 offset = new Vector3(0f, 0f, -1f);  // 相机相对于目标物体的偏移量

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}