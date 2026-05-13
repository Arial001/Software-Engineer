using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody; // Referensi ke karakter
    [SerializeField] private float minVerticalAngle = -90f;
    [SerializeField] private float maxVerticalAngle = 90f;

    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Rotated();
    }

    private void Rotated()
    {
        // Input mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotasi horizontal (player body)
        playerBody.Rotate(Vector3.down * mouseX);

        // Rotasi vertikal (kamera)
        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);

        // Terapkan rotasi ke kamera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, -180f);
    }
}
