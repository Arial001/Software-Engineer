using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datagame3 : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Header("Grid & Block Data")]
    [Tooltip("Drag & Drop balok 3D di sini.")]
    public List<GameObject> allGridBlocks = new List<GameObject>();
    [Tooltip("Daftar 26 MeshFilter untuk huruf A-Z secara berurutan.")]
    public List<MeshFilter> alphabetMeshes = new List<MeshFilter>();
    [Tooltip("Daftar 2 MeshRenderer yang akan digunakan untuk balok.")]
    public List<MeshRenderer> blockRenderers = new List<MeshRenderer>();
    public int B = 0;
    public GeneratorGame3 GeneratorGame3;

    [Header("Grid Dimensions")]
    [Tooltip("Jumlah baris grid.")]
    public int gridRows = 10;
    [Tooltip("Jumlah kolom grid.")]
    public int gridCols = 20;

    public List<List<GameObject>> gridBlocks { get; private set; }
    public bool[,] isOccupied;

    // --- List baru untuk menyimpan data yang diminta ---
    [Header("Data Balok Terpilih")]
    [Tooltip("Menyimpan indeks 1D dari balok yang berisi kata.")]
    public List<int> storedGridIndices = new List<int>();
    [Tooltip("Menyimpan objek balok yang sesuai dengan kata.")]
    public List<GameObject> selectedGridBlocks = new List<GameObject>();


    void Awake()
    {
        InitializeGrid();
    }
    public void EnableAllGridBlocks()
    {
        foreach (GameObject block in allGridBlocks)
        {
            if (block != null)
            {
                block.SetActive(true);
            }
        }
        Debug.Log("Semua balok pada allGridBlocks sudah dihidupkan kembali!");
    }
    public void SetAllBlocksToMaterialIndexB()
    {
        if (blockRenderers == null || blockRenderers.Count == 0)
        {
            Debug.Log("blockRenderers belum diisi!");
            return;
        }
        if (B < 0 || B >= blockRenderers.Count)
        {
            Debug.Log($"Index B ({B}) berada di luar range list blockRenderers!");
            return;
        }
        var mat = blockRenderers[B].sharedMaterial;
        foreach (GameObject block in allGridBlocks)
        {
            if (block != null)
            {
                var mr = block.GetComponent<MeshRenderer>();
                if (mr != null)
                {
                    mr.sharedMaterial = mat;
                }
            }
        }
        Debug.Log($"Semua objek di allGridBlocks sudah diganti materialnya dengan blockRenderers index {B}.");
    }
    private void InitializeGrid()
    {
        if (allGridBlocks.Count != gridRows * gridCols)
        {
            Debug.Log("Jumlah balok (" + allGridBlocks.Count + ") tidak sesuai dengan dimensi grid (" + gridRows + "x" + gridCols + "). Mohon sesuaikan jumlahnya.");
            return;
        }

        gridBlocks = new List<List<GameObject>>();
        isOccupied = new bool[gridRows, gridCols];

        for (int i = 0; i < gridRows; i++)
        {
            gridBlocks.Add(new List<GameObject>());
            for (int j = 0; j < gridCols; j++)
            {
                int index = (i * gridCols) + j;
                gridBlocks[i].Add(allGridBlocks[index]);
                isOccupied[i, j] = false;
            }
        }
    }

    /// <summary>
    /// Memperbarui list indeks dan objek balok yang terpilih.
    /// </summary>
    public void UpdateSelectedLists()
    {
        if (GeneratorGame3 == null)
        {
            Debug.Log("Referensi GeneratorGame3 tidak diatur!");
            return;
        }

        storedGridIndices.Clear();
        selectedGridBlocks.Clear();

        foreach (var placementData in GeneratorGame3.wordPlacements)
        {
            foreach (var gridIndex in placementData.gridIndices)
            {
                // Simpan index
                storedGridIndices.Add(gridIndex);

                // Dapatkan objek balok dari list 1D utama
                if (gridIndex >= 0 && gridIndex < allGridBlocks.Count)
                {
                    selectedGridBlocks.Add(allGridBlocks[gridIndex]);
                }
            }
        }
    }
    public void DisableFoundWordBlocks()
    {
        if (GeneratorGame3 == null)
        {
            Debug.Log("Referensi GeneratorGame3 tidak diatur!");
            return;
        }

        // Loop untuk semua FoundWordData dalam foundWords
        foreach (var found in GeneratorGame3.foundWords)
        {
            foreach (var gridIndex in found.gridIndicesFoundword)
            {
                if (gridIndex >= 0 && gridIndex < allGridBlocks.Count)
                {
                    GameObject block = allGridBlocks[gridIndex];
                    if (block != null)
                    {
                        block.SetActive(false);
                        GeneratorGame3.CheckIfAllWordBlocksAreDead();
                    }
                }
            }
        }
    }
}
