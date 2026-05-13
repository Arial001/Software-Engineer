using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcherAktif : MonoBehaviour
{
    [SerializeField] private Camera[] playerCameras; // Array untuk menyimpan referensi kamera pemain
    private int currentCameraIndex = 0; // Indeks kamera saat ini

    private void Start()
    {
        // Menyembunyikan semua kamera kecuali kamera pertama
        for (int i = 0; i < playerCameras.Length; i++)
        {
            playerCameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
    }

    private void Update()
    {
        // Cek input untuk berpindah kamera
        if (Input.GetKeyDown(KeyCode.T)) // Ganti dengan tombol yang diinginkan
        {
            SwitchCamera();
        }
    }

    public void SwitchCamera()
    {
        // Menyembunyikan kamera saat ini
        playerCameras[currentCameraIndex].gameObject.SetActive(false);

        // Pindah ke indeks kamera berikutnya
        currentCameraIndex++;
        if (currentCameraIndex >= playerCameras.Length)
        {
            currentCameraIndex = 0; // Kembali ke kamera pertama jika sudah mencapai akhir
        }

        // Menampilkan kamera baru
        playerCameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
