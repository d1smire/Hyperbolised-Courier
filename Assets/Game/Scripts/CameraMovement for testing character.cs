using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovementfortestingcharacter : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [Header("Distance")]
    [SerializeField] private float distance = 5f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private float zoomSpeed = 10f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 120f;
    [SerializeField] private float pitch = 20f;
    [SerializeField] private float yaw = 0f;

    private void Start()
    {
        if (_target == null)
            Debug.LogWarning("Camera target not assigned.");
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        // Zoom using new Input System (mouse scroll)
        float scroll = 0f;
        if (Mouse.current != null)
            scroll = Mouse.current.scroll.ReadValue().y;

        if (Mathf.Abs(scroll) > 0.0001f)
        {
            // scroll value is usually in pixels; scale it for a smooth feel
            distance -= scroll * zoomSpeed * 0.01f;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }

        // Rotate when right mouse button is held (new Input System)
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();
            // scale delta to be framerate independent and comfortable
            yaw += delta.x * rotationSpeed * Time.deltaTime * 0.02f;
            pitch -= delta.y * rotationSpeed * Time.deltaTime * 0.02f;
            pitch = Mathf.Clamp(pitch, -89f, 89f);
        }

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 offset = rot * new Vector3(0f, 0f, -distance);
        transform.position = _target.position + offset;
        transform.rotation = rot;
    }
}
