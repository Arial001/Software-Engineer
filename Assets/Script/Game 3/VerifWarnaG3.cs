using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifWarnaG3 : MonoBehaviour
{
    [Header("Referensi GeneratorGame3")]
    [Tooltip("Drag & Drop game object dengan script GeneratorGame3 di sini.")]
    public GeneratorGame3 generator;

    [Header("Data Hasil Baca")]
    [Tooltip("List kata-kata yang akan dibaca dari GeneratorGame3.")]
    public List<string> harvestedWords = new List<string>();
    [Tooltip("List nama material dari kata-kata yang dibaca.")]
    public List<string> harvestedMaterials = new List<string>();

    [Header("Material Manual")]
    [Tooltip("Masukkan Mesh Renderer yang ingin Anda gunakan secara manual.")]
    public List<MeshRenderer> manualMaterials = new List<MeshRenderer>();
    [Tooltip("Nama-nama material dari list manual.")]
    public List<string> MeshrendererName = new List<string>(); // Sekarang publik

    // Metode ini bisa dipanggil dari skrip lain atau saat start
    private void Start()
    {
        RetrieveData();
    }
    public void RetrieveData()
    {
        if (generator == null)
        {
            Debug.LogError("Referensi GeneratorGame3 tidak diatur!");
            return;
        }

        // --- Mengambil data wordsToPlace ---
        harvestedWords.Clear();
        harvestedWords.AddRange(generator.wordsToPlace);

        // --- Mengambil data materialNames dari wordPlacements ---
        harvestedMaterials.Clear();
        foreach (var placementData in generator.wordPlacements)
        {
            // Tambahkan nama kata sebagai penanda
            harvestedMaterials.Add($"--- Kata: {placementData.word} ---");
            harvestedMaterials.AddRange(placementData.materialNames);
        }

        // --- Memanggil metode baru untuk mengisi list manual ---
        PopulateManualMaterialNames();

        Debug.Log("Data berhasil diambil dari GeneratorGame3.");
    }

    /// <summary>
    /// Mengambil nama material dari list MeshRenderer manual.
    /// </summary>
    private void PopulateManualMaterialNames()
    {
        MeshrendererName.Clear();

        if (manualMaterials == null)
        {
            return;
        }

        foreach (var mr in manualMaterials)
        {
            if (mr != null && mr.sharedMaterial != null)
            {
                MeshrendererName.Add(mr.sharedMaterial.name);
            }
            else
            {
                MeshrendererName.Add("Renderer Kosong");
            }
        }
    }
}