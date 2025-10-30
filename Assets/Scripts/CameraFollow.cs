using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // Knight
    public float smoothSpeed = 5f;
    
    // Xóa offsetY đi, chúng ta sẽ cần offset cho cả X và Y
    private Vector3 offset;
    
    [Header("Camera Boundaries")]
    public float minXBoundary; 
    public float maxXBoundary;
    
    // Thêm giới hạn cho trục Y (nếu bạn muốn)
    public float minYBoundary; 
    public float maxYBoundary;


    void Start()
    {
        // Tính toán khoảng cách ban đầu từ camera đến nhân vật
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target == null) return; 

        // 1. Tính toán vị trí mục tiêu MỚI (theo cả X và Y)
        // Chúng ta muốn camera ở vị trí của nhân vật + khoảng cách ban đầu
        Vector3 targetPosition = target.position + offset;
        
        // 2. "KẸP" vị trí X và Y trong giới hạn
        targetPosition.x = Mathf.Clamp(targetPosition.x, minXBoundary, maxXBoundary);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minYBoundary, maxYBoundary);

        // Giữ nguyên trục Z của camera
        targetPosition.z = transform.position.z; 

        // 3. Di chuyển mượt mà
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}