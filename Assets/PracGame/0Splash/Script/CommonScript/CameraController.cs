using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 100.0f; // Mouse sensitivity
    public float clampAngle = 80.0f; // To clamp the vertical rotation

    [SerializeField] private float verticalRotation = 0.0f; // Rotation around the X axis
    [SerializeField] private float horizontalRotation = 0.0f; // Rotation around the Y axis

    void Start()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        horizontalRotation = rotation.y;
        verticalRotation = rotation.x;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // 0 for left click
        {
            float mouseX = -Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            horizontalRotation += mouseX * sensitivity * Time.deltaTime;
            verticalRotation += mouseY * sensitivity * Time.deltaTime;

            verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0.0f);
            transform.rotation = localRotation;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Quaternion localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0.0f);
            transform.rotation = localRotation;
        }
    }

    public void InitRotation()
    {
        verticalRotation = 0.0f;
        horizontalRotation = 0.0f;
    }
}
