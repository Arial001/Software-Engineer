using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<Camera> cameras;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CameraALLOff()
    {
        CameraP1OFF();
        CameraP2OFF();
        CameraP3OFF();
        CameraP4OFF();
    }
    public void CameraP1ON()
    {
        cameras[0].gameObject.SetActive(true);
    }
    public void CameraP2ON()
    {
        cameras[1].gameObject.SetActive(true);
    }
    public void CameraP3ON()
    {
        cameras[2].gameObject.SetActive(true);
    }
    public void CameraP4ON()
    {
        cameras[3].gameObject.SetActive(true);
    }
    public void MainMenuCameraON()
    {
        cameras[4].gameObject.SetActive(true);
    }
    public void ChooseCameraON()
    {
        cameras[5].gameObject.SetActive(true);
    }
    public void CameraP1OFF()
    {
        cameras[0].gameObject.SetActive(false);
    }
    public void CameraP2OFF()
    {
        cameras[1].gameObject.SetActive(false);
    }
    public void CameraP3OFF()
    {
        cameras[2].gameObject.SetActive(false);
    }
    public void CameraP4OFF()
    {
        cameras[3].gameObject.SetActive(false);
    }
    public void CameraP1G2ON()
    {
        cameras[6].gameObject.SetActive(true);
    }
    public void CameraP2G2ON()
    {
        cameras[7].gameObject.SetActive(true);
    }
    public void CameraP3G2ON()
    {
        cameras[8].gameObject.SetActive(true);
    }
    public void CameraP4G2ON()
    {
        cameras[9].gameObject.SetActive(true);
    }
    public void CameraP1G2OFF()
    {
        cameras[6].gameObject.SetActive(false);
    }
    public void CameraP2G2OFF()
    {
        cameras[7].gameObject.SetActive(false);
    }
    public void CameraP3G2OFF()
    {
        cameras[8].gameObject.SetActive(false);
    }
    public void CameraP4G2OFF()
    {
        cameras[9].gameObject.SetActive(false);
    }
    public void CameraP1G3ON()
    {
        cameras[10].gameObject.SetActive(true);
    }
    public void CameraP2G3ON()
    {
        cameras[11].gameObject.SetActive(true);
    }
    public void CameraP3G3ON()
    {
        cameras[12].gameObject.SetActive(true);
    }
    public void CameraP4G3ON()
    {
        cameras[13].gameObject.SetActive(true);
    }
    public void CameraP1G3OFF()
    {
        cameras[10].gameObject.SetActive(false);
    }
    public void CameraP2G3OFF()
    {
        cameras[11].gameObject.SetActive(false);
    }
    public void CameraP3G3OFF()
    {
        cameras[12].gameObject.SetActive(false);
    }
    public void CameraP4G3OFF()
    {
        cameras[13].gameObject.SetActive(false);
    }
    public void MainMenuCameraOFF()
    {
        cameras[4].gameObject.SetActive(false);
    }
    public void ChooseCameraOFF()
    {
        cameras[5].gameObject.SetActive(false);
    }
    /*public void SetCameraP1SingleViewport()
    {
        if (cameras.Count > 0 && cameras[0] != null)
        {
            cameras[0].rect = new Rect(0f, 0f, 1f, 1f);
            Debug.Log("Viewport camera[1] diubah menjadi x = 0, y = 0, w = 0.5, h = 0.5");
        }
        else
        {
            Debug.LogError("Camera[1] tidak ditemukan atau list kamera tidak memiliki cukup elemen!");
        }
    }*/
    public void UpdateCameraViewports()
    {
        List<Camera> activeCameras = new List<Camera>();

        // Menyaring dan mengurutkan kamera yang aktif berdasarkan indeks terkecil
        for (int i = 0; i < cameras.Count; i++)
        {
            //Debug.Log($"Jumlah kamera aktif: 1");
            if (cameras[i].gameObject.activeSelf)
            {
                Debug.Log($"Kamera dengan indeks asli {i} aktif: {cameras[i].gameObject.name}");
                activeCameras.Add(cameras[i]);
            }
        }

        int activeCount = activeCameras.Count;
        Debug.Log($"Jumlah kamera aktif: {activeCount}");

        if (activeCount == 2)
        {
            Debug.Log($"Jumlah kamera aktif: {activeCount}");
            activeCameras[0].rect = new Rect(0f, 0f, 0.5f, 1f); // Kamera pertama di kiri (50% layar)
            activeCameras[1].rect = new Rect(0.5f, 0f, 0.5f, 1f); // Kamera kedua di kanan (50% layar)
        }
        else if (activeCount == 3)
        {
            Debug.Log($"Jumlah kamera aktif: {activeCount}");
            activeCameras[0].rect = new Rect(0f, 0f, 0.5f, 1f); // Kamera pertama di kiri (50% layar)
            activeCameras[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f); // Kamera kedua di kanan atas
            activeCameras[2].rect = new Rect(0.5f, 0f, 0.5f, 0.5f); // Kamera ketiga di kanan bawah
        }
        else if (activeCount >= 4)
        {
            Debug.Log($"Jumlah kamera aktif: {activeCount}");
            activeCameras[0].rect = new Rect(0f, 0.5f, 0.5f, 0.5f); // Kamera pertama di kiri atas
            activeCameras[1].rect = new Rect(0f, 0f, 0.5f, 0.5f); // Kamera kedua di kiri bawah
            activeCameras[2].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f); // Kamera ketiga di kanan atas
            activeCameras[3].rect = new Rect(0.5f, 0f, 0.5f, 0.5f); // Kamera keempat di kanan bawah
        }
        else if (activeCount == 1)
        {
            Debug.Log($"Jumlah kamera aktif: {activeCount}");
            activeCameras[0].rect = new Rect(0f, 0f, 1f, 1f); // Kamera fullscreen jika hanya satu yang aktif
        }
    }
    public void DisableAllCameras()
    {
        // Periksa apakah list kamera ada dan memiliki elemen
        if (cameras == null || cameras.Count == 0)
        {
            Debug.LogWarning("Daftar kamera kosong atau belum diatur!");
            return;
        }

        // Ulangi setiap kamera dalam daftar dan matikan
        foreach (Camera cam in cameras)
        {
            if (cam != null)
            {
                cam.gameObject.SetActive(false);
            }
        }
        Debug.Log("Semua kamera dalam daftar telah dinonaktifkan.");
    }
}
