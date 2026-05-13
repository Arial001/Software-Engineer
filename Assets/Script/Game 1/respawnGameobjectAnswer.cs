using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnGameobjectAnswer : MonoBehaviour
{
    [Header("Referensi QuestionIndexElimination")]
    public QuestionIndexElimination questionIndexElimination; // Referensi ke script QuestionIndexElimination

    [Header("Daftar Kontainer")]
    public List<GameObject> containers = new List<GameObject>(); // Daftar GameObject kosong yang berfungsi sebagai kontainer
    

    private void Start()
    {
        Initialize();
        questionIndexElimination.OnStartTwice += ResetScript;
        
    }
    private void Initialize()
    {
        if (questionIndexElimination == null)
        {

            Debug.LogError("QuestionIndexElimination belum diatur!");
            return;
        }

        // Ambil hasil randomisasi dari QuestionIndexElimination
        List<string> randomizedResults = questionIndexElimination.GetAllRandomizedAnswers();

        if (randomizedResults == null || randomizedResults.Count == 0)
        {
            Debug.LogWarning("Hasil randomisasi kosong! Pastikan QuestionIndexElimination bekerja dengan benar.");
            return;
        }

        //Debug.Log($"Sudah mendapatkan hasil randomisasi: {string.Join(", ", randomizedResults)}");

        // Bagi hasil randomisasi ke masing-masing kontainer
        DistributeQuestionsToContainers(randomizedResults);
    }

    // Fungsi untuk mendistribusikan pertanyaan ke kontainer
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

        //Debug.Log($"Jumlah kontainer: {containerCount}, Jumlah hasil randomisasi: {resultCount}");

        // Jumlah item yang akan didistribusikan
        int itemsToDistribute = Mathf.Min(containerCount, resultCount);

        for (int i = 0; i < itemsToDistribute; i++)
        {
            string questionName = randomizedResults[i];
            GameObject container = containers[i];

            //Debug.Log($"Proses distribusi untuk pertanyaan '{questionName}' ke kontainer '{container.name}'.");

            // Cari anak dengan nama yang sesuai di dalam kontainer
            Transform childToDisplay = FindChildByName(container.transform, questionName);

            if (childToDisplay != null)
            {
                //Debug.Log($"Menampilkan objek '{questionName}' di kontainer '{container.name}'.");
                // Aktifkan objek anak yang sesuai
                childToDisplay.gameObject.SetActive(true);
                // Lepaskan dari parent agar berada di posisi root
                //childToDisplay.SetParent(null);
            }
            else
            {
                //Debug.LogWarning($"Anak dengan nama '{questionName}' tidak ditemukan di kontainer '{container.name}'.");
            }
        }
    }

    // Fungsi untuk mencari anak berdasarkan nama di dalam kontainer
    private Transform FindChildByName(Transform parent, string childName)
    {
       // Debug.Log($"Mencari anak dengan nama '{childName}' di kontainer '{parent.name}'.");
        foreach (Transform child in parent)
        {
          //  Debug.Log($"Memeriksa anak: {child.name}");
            if (child.name.Equals(childName, System.StringComparison.OrdinalIgnoreCase))
            {
               // Debug.Log($"Anak ditemukan: {child.name}");
                return child; // Kembalikan transform anak yang cocok
            }
        }
       // Debug.Log($"Anak dengan nama '{childName}' tidak ditemukan di kontainer '{parent.name}'.");
        return null; // Tidak ditemukan
    }
    public void ResetScript()
    {
        foreach (GameObject container in containers)
        {
            foreach (Transform child in container.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(false); // Nonaktifkan anak
                }
            }
        }
        Initialize();

        Debug.Log("Script DisplaySelectedQuestion berhasil di-reset.");
    }
    private void OnDestroy()
    {
        questionIndexElimination.OnStartTwice -= ResetScript; // Unsubscribe to prevent memory leaks
    }
}
