using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Knight
    public float smoothSpeed = 5f;
    private float offsetY;    // giữ nguyên vị trí Y ban đầu của camera

    void Start()
    {
        // Ghi nhớ vị trí Y ban đầu của camera
        offsetY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Chỉ theo dõi theo trục X (ngang)
        Vector3 targetPosition = new Vector3(target.position.x, offsetY, transform.position.z);

        // Di chuyển mượt mà theo nhân vật
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
