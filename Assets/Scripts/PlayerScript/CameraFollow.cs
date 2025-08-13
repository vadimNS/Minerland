using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;     // Гравець або інший об'єкт
    [SerializeField] private float smoothSpeed = 0.125f; // Плавність
    [SerializeField] private Vector3 offset;       // Зсув камери
    [SerializeField] private float yDeadZone = 0.5f; // Мертва зона по Y (≈ 8 пікселів)

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 currentPosition = transform.position;

        // Якщо гравець майже не змінив висоту — не рухаємо камеру по Y
        float yDifference = Mathf.Abs(desiredPosition.y - currentPosition.y);
        if (yDifference < yDeadZone)
        {
            desiredPosition.y = currentPosition.y;
        }

        // Плавне переміщення
        Vector3 smoothedPosition = Vector3.Lerp(currentPosition, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
