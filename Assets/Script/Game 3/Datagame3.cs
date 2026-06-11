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
    [Header("Data Copy Balok")]
    [Tooltip("List 1D baru untuk menyimpan hasil copy (duplikat) dari allGridBlocks.")]
    public List<GameObject> copiedAllGridBlocks = new List<GameObject>();
    

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
    public void InitializeGrid()
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
        DuplicateGridBlocks1D();
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

    public void DuplicateGridBlocks1D()
    {
        // 1. Bersihkan list baru sebelum diisi agar data tidak menumpuk/dobel 
        // jika fungsi ini dipanggil lebih dari sekali
        copiedAllGridBlocks.Clear();

        // 2. Lakukan looping sebanyak jumlah objek di list asli
        for (int i = 0; i < allGridBlocks.Count; i++)
        {
            GameObject originalObject = allGridBlocks[i];

            if (originalObject != null)
            {
                /// 3. COPY OBJECT: Duplikat objek dan paksa masuk ke parent (induk) yang sama
                GameObject duplicatedObject = Instantiate(originalObject, originalObject.transform.parent);

                // (Opsional) Rapikan nama agar mudah dibedakan di Inspector
                duplicatedObject.name = originalObject.name;

                // [TAMBAHAN PENTING] Matikan objek copy agar tidak bertumpuk/dobel di layar
                duplicatedObject.SetActive(false);

                // 4. Masukkan objek hasil copy ke list baru
                copiedAllGridBlocks.Add(duplicatedObject);
            }
            else
            {
                // 5. PENJAGA INDEX: Jika objek aslinya hilang/null, kita tetap harus 
                // memasukkan 'null' ke list baru. Ini SANGAT PENTING agar index balok 
                // selanjutnya tidak bergeser maju.
                copiedAllGridBlocks.Add(null);
                Debug.Log($"Objek asli di index {i} kosong/null, slot di list baru dibiarkan kosong.");
            }
        }

        Debug.Log($"Selesai meng-copy! Jumlah objek di list asli: {allGridBlocks.Count} | Jumlah di list copy: {copiedAllGridBlocks.Count}");
    }

    public void RestoreMissingBlocks()
    {
        // Cegah error jika list backup belum pernah dibuat
        if (copiedAllGridBlocks == null || copiedAllGridBlocks.Count == 0)
        {
            Debug.LogWarning("List backup (copiedAllGridBlocks) kosong! Lakukan duplikasi terlebih dahulu.");
            return;
        }

        int restoredCount = 0;

        // Looping untuk mengecek semua index di list utama
        for (int i = 0; i < allGridBlocks.Count; i++)
        {
            // Jika terdeteksi ada slot yang kosong atau objeknya telah hancur
            if (allGridBlocks[i] == null)
            {
                // Cek keamanan: Pastikan index di list backup tersedia dan tidak kosong
                if (i < copiedAllGridBlocks.Count && copiedAllGridBlocks[i] != null)
                {
                    // 1. PINDAHKAN OBJECT: Ambil referensi langsung dari list backup TANPA Instantiate
                    GameObject restoredBlock = copiedAllGridBlocks[i];

                    // 2. AKTIFKAN: Karena saat di-copy objek ini dimatikan (SetActive(false)),
                    // kita harus menghidupkannya kembali agar muncul di scene.
                    restoredBlock.SetActive(true);

                    // 3. MASUKKAN KE LIST UTAMA: Isi slot kosong tersebut dengan objek backup
                    allGridBlocks[i] = restoredBlock;

                    restoredCount++;
                }
                else
                {
                    Debug.LogWarning($"Index {i} di list utama kosong, tapi tidak ada data backup yang sesuai.");
                }
            }
        }
        Debug.Log($"Proses pengecekan selesai. Berhasil memindahkan {restoredCount} objek dari backup ke list utama.");
    }

    public void DestroyAllGridBlocks()
    {
        // Gunakan 'for' loop agar kita bisa mengakses spesifik nomor index-nya
        for (int i = 0; i < allGridBlocks.Count; i++)
        {
            if (allGridBlocks[i] != null)
            {
                // 1. Hancurkan objek secara fisik dari dalam game
                Destroy(allGridBlocks[i]);

                // 2. Ubah isi slot tersebut menjadi null secara eksplisit
                // Index-nya tetap ada, tapi isinya menjadi kosong.
                allGridBlocks[i] = null;
            }
        }

        Debug.Log("Semua objek fisik berhasil dihancurkan, tetapi slot index di allGridBlocks tetap dipertahankan.");
    }
}
