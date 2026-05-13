using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisCamera : MonoBehaviour
{
    [SerializeField] private float sensitivitasMouse = 100f;
    [SerializeField] private float sudutVertikalMinimum = -90f;
    [SerializeField] private float sudutVertikalMaksimum = 90f;

    public Transform target; // Objek yang akan diikuti oleh kamera

    private float xRotasi;
    private float yRotasi;

    void Start()
    {
        // Mengunci kursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mendapatkan input dari mouse
        float mouseX = Input.GetAxis("Mouse X") * sensitivitasMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivitasMouse * Time.deltaTime;

        // Mengubah rotasi horizontal
        yRotasi += mouseX;

        // Mengubah rotasi vertikal
        xRotasi -= mouseY;
        xRotasi = Mathf.Clamp(xRotasi, sudutVertikalMinimum, sudutVertikalMaksimum);

        // Menerapkan rotasi ke kamera
        transform.localRotation = Quaternion.Euler(xRotasi, yRotasi, 0);

        // Membuat kamera selalu menghadap ke target
        transform.LookAt(target);
    }
}