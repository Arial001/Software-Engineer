using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPertanyaanGambar : MonoBehaviour
{
    [Header("Referensi PertanyaanTebakGambar")]
    public PertanyaanTebakGambar PertanyaanTebakGambar;
    [Header("Daftar Kontainer")]
    public List<GameObject> containers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (PertanyaanTebakGambar == null)
        {
            Debug.LogError("PertanyaanTebakGambar belum diatur! Harap seret GameObject dengan script PertanyaanTebakGambar ke slot di Inspector.");
            return;
        }

        // Ambil hasil randomisasi terbaru dari PertanyaanTebakGambar
        List<string> randomizedResults = PertanyaanTebakGambar.GetAllRandomizedAnswers();

        if (randomizedResults == null || randomizedResults.Count == 0)
        {
            Debug.LogWarning("Hasil randomisasi kosong! Pastikan PertanyaanTebakGambar bekerja dengan benar dan list 'card' terisi.");
            return;
        }

        Debug.Log($"DisplayPertanyaanGambar: Sudah mendapatkan hasil randomisasi: {string.Join(", ", randomizedResults)}");

        // Pastikan semua anak di semua kontainer dinonaktifkan sebelum mendistribusikan yang baru
        DeactivateAllChildrenInContainers();

        // Bagi hasil randomisasi ke masing-masing kontainer
        DistributeQuestionsToContainers(randomizedResults);
    }

    private void DeactivateAllChildrenInContainers()
    {
        foreach (GameObject container in containers)
        {
            if (container == null) continue; // Lewati jika kontainer kosong

            foreach (Transform child in container.transform)
            {
                // Hanya nonaktifkan jika memang aktif, untuk efisiensi
                if (child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    private void DistributeQuestionsToContainers(List<string> randomizedResults)
    {
        int containerCount = containers.Count;
        int resultCount = randomizedResults.Count;

        // Pastikan kontainer dan hasil randomisasi tidak kosong
        if (containerCount == 0)
        {
            Debug.LogWarning("Daftar kontainer kosong! Pastikan Anda mengisi kontainer di Inspector.");
            return;
        }

        if (resultCount == 0)
        {
            Debug.LogWarning("Hasil randomisasi kosong! Tidak ada pertanyaan yang akan didistribusikan.");
            return;
        }

        Debug.Log($"DisplayPertanyaanGambar: Jumlah kontainer: {containerCount}, Jumlah hasil randomisasi: {resultCount}");

        // Jumlah item yang akan didistribusikan (sesuai yang terkecil antara jumlah kontainer atau jumlah hasil randomisasi)
        int itemsToDistribute = Mathf.Min(containerCount, resultCount);

        for (int i = 0; i < itemsToDistribute; i++)
        {
            string questionName = randomizedResults[i];
            GameObject container = containers[i];

            if (container == null) // Pastikan kontainer tidak null di editor
            {
                Debug.LogWarning($"Kontainer di indeks {i} kosong! Lewati.");
                continue;
            }

            Debug.Log($"DisplayPertanyaanGambar: Proses distribusi untuk pertanyaan '{questionName}' ke kontainer '{container.name}'.");

            // Cari anak dengan nama yang sesuai di dalam kontainer
            Transform childToDisplay = FindChildByName(container.transform, questionName);

            if (childToDisplay != null)
            {
                Debug.Log($"DisplayPertanyaanGambar: Menampilkan objek '{questionName}' di kontainer '{container.name}'.");
                // Aktifkan objek anak yang sesuai
                childToDisplay.gameObject.SetActive(true);

                // --- MODIFIKASI HANYA UNTUK POSISI LOKAL ---
                // Atur posisi lokal anak agar sama dengan posisi parent-nya (kontainer)
                // Ini akan membuat pivot anak berada di pivot kontainer
                childToDisplay.localPosition = Vector3.zero;

                // Rotasi dan skala tidak diubah/direset
                // childToDisplay.localRotation = Quaternion.identity;
                // childToDisplay.localScale = Vector3.one;

            }
            else
            {
                Debug.LogWarning($"DisplayPertanyaanGambar: Anak dengan nama '{questionName}' tidak ditemukan di kontainer '{container.name}'. Pastikan penamaan sama persis.");
            }
        }
    }

    private Transform FindChildByName(Transform parent, string childName)
    {
        // Debug.Log($"DisplayPertanyaanGambar: Mencari anak dengan nama '{childName}' di kontainer '{parent.name}'.");
        foreach (Transform child in parent)
        {
            // Debug.Log($"DisplayPertanyaanGambar: Memeriksa anak: {child.name}");
            // Gunakan StringComparison.OrdinalIgnoreCase untuk pencarian nama yang tidak peduli huruf besar/kecil
            if (child.name.Equals(childName, System.StringComparison.OrdinalIgnoreCase))
            {
                // Debug.Log($"DisplayPertanyaanGambar: Anak ditemukan: {child.name}");
                return child; // Kembalikan transform anak yang cocok
            }
        }
        // Debug.Log($"DisplayPertanyaanGambar: Anak dengan nama '{childName}' tidak ditemukan di kontainer '{parent.name}'.");
        return null; // Tidak ditemukan
    }

    public void ResetScript()
    {
        // Panggil DeactivateAllChildrenInContainers() terlebih dahulu
        // untuk memastikan semua objek disembunyikan sebelum distribusi baru
        DeactivateAllChildrenInContainers();

        // Panggil Initialize untuk mendapatkan hasil randomisasi terbaru dan mendistribusikannya
        Initialize();

        Debug.Log("DisplayPertanyaanGambar: Script berhasil di-reset.");
    }
}