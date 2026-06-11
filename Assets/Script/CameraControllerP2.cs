using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CameraControllerP2 : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float minVerticalAngle = -90f;
    [SerializeField] private float maxVerticalAngle = 90f;
    public int joystickIndex = 0;

    private Gamepad joystick;
    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (Gamepad.all.Count > joystickIndex)
        {
            joystick = Gamepad.all[joystickIndex];
            PrintJoystickControls();
        }
    }

    private void Update()
    {
        if (Gamepad.all.Count > joystickIndex)
        {
            joystick = Gamepad.all[joystickIndex];
            PrintJoystickControls();
        }
        Rotated();
    }

    private void Rotated()
    {
        if (joystick == null) return;

        var rightStickX = joystick.rightStick.x as AxisControl;    // Untuk gerakan horizontal (kanan/kiri)
        var rightStickY = joystick.rightStick.y as AxisControl;     // Untuk gerakan vertikal (atas/bawah)

        if (rightStickX != null && rightStickY != null)
        {
            float mouseX = rightStickX.ReadValue() * mouseSensitivity; //* Time.deltaTime;
            float mouseY = rightStickY.ReadValue() * mouseSensitivity; //* Time.deltaTime;

            // Rotasi horizontal (player body)
            playerBody.Rotate(Vector3.down * mouseX);

            // Rotasi vertikal (kamera)
            xRotation += mouseY;
            xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);

            // Terapkan rotasi ke kamera
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 180f);
        }
    }

    private void PrintJoystickControls()
    {
        Debug.Log($"Kontrol pada Gamepad Index {joystickIndex}:");

        foreach (var control in joystick.children)
        {
            Debug.Log($"Control Name: {control.name}, Control Type: {control.GetType()}");
        }
    }
}
