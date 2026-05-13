using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class FoundWordData
{
    public string word;
    public List<int> gridIndicesFoundword = new List<int>();
}

public class WordPlacementData
{
    public string word;
    public List<char> letters = new List<char>();
    public List<int> gridIndices = new List<int>();
    public List<string> materialNames = new List<string>();
}

public class GeneratorGame3 : MonoBehaviour
{
    public List<FoundWordData> foundWords = new List<FoundWordData>();
    [Header("Generator Settings")]
    [Tooltip("Drag & Drop game object Datagame3 di sini.")]
    public Datagame3 dataManager;
    [Tooltip("Daftar kata yang akan digenerate ke grid.")]
    public List<string> wordsToPlace = new List<string>();
    [Header("Referensi Skrip Lain")]
    [Tooltip("Drag & Drop game object QuestionIndexEliminationG3 di sini.")]
    public QuestionIndexEliminationG3 questionEliminator;
    [Header("Renderer Indexes")]
    [Tooltip("Index Mesh Renderer untuk kata-kata yang ditempatkan.")]
    public int wordRendererIndex = 0;
    [Tooltip("Index Mesh Renderer untuk huruf-huruf acak.")]
    public int randomRendererIndex = 0;
    [Header("Word Order")]
    [Tooltip("Jika diaktifkan, kata akan ditempatkan secara terbalik.")]
    public bool reverseWordOrder = false;
    [Header("Hasil Penempatan")]
    [Tooltip("Menyimpan data lokasi untuk setiap kata yang digenerate.")]
    public List<WordPlacementData> wordPlacements = new List<WordPlacementData>();
    [Header("Referensi Nama Renderer")]
    [Tooltip("List yang menyimpan nama material dari daftar blockRenderers di Datagame3.")]
    public List<string> availableRendererNames = new List<string>();

    // Skor untuk masing-masing player
    public int p1;
    public int p2;
    public int p3;
    public int p4;
    public int A;

    void Update()
    {
        // Hanya update visual, tidak mempengaruhi data score/verifikasi tanpa pemanggilan fungsi khusus
    }

    public void PopulateAvailableRendererNames()
    {
        if (dataManager == null)
        {
            Debug.Log("Referensi Datagame3 tidak diatur saat Start!");
            return;
        }
        availableRendererNames.Clear();
        foreach (var renderer in dataManager.blockRenderers)
        {
            if (renderer != null && renderer.sharedMaterial != null)
            {
                availableRendererNames.Add(renderer.sharedMaterial.name);
            }
            else
            {
                availableRendererNames.Add("Renderer Kosong");
            }
        }
    }

    public void clear()
    {
        foundWords.Clear();
        availableRendererNames.Clear();
        wordPlacements.Clear();
        wordsToPlace.Clear();
        p1 = 0;
        p2 = 0;
        p3 = 0;
        p4 = 0;
        A = 0;
    }

    public void GenerateWords()
    {
        A = 0;
        if (dataManager == null)
        {
            Debug.Log("Datagame3 is not assigned. Please assign it in the Inspector.");
            return;
        }
        if (questionEliminator == null)
        {
            Debug.Log("QuestionIndexEliminationG3 is not assigned. Please assign it in the Inspector.");
            return;
        }
        wordsToPlace.Clear();
        wordsToPlace.AddRange(questionEliminator.selectedAnswers);
        wordPlacements.Clear();
        ResetGrid();
        MeshRenderer selectedRenderer = dataManager.blockRenderers[wordRendererIndex];
        foreach (string word in wordsToPlace)
        {
            PlaceWordRandomly(word.ToUpper(), selectedRenderer);
        }
        FillRemainingBlocksRandomly();
    }

    private void ResetGrid()
    {
        for (int i = 0; i < dataManager.gridRows; i++)
        {
            for (int j = 0; j < dataManager.gridCols; j++)
            {
                dataManager.isOccupied[i, j] = false;
            }
        }
        FillRemainingBlocksRandomly();
    }

    private void PlaceWordRandomly(string word, MeshRenderer renderer)
    {
        int maxTries = 100;
        bool wordPlaced = false;
        for (int i = 0; i < maxTries; i++)
        {
            int startRow = Random.Range(0, dataManager.gridRows);
            int startCol = Random.Range(0, dataManager.gridCols);
            bool isHorizontal = Random.Range(0, 2) == 0;
            if (CanPlaceWord(word, startRow, startCol, isHorizontal))
            {
                PlaceWord(word, startRow, startCol, isHorizontal, renderer);
                wordPlaced = true;
                break;
            }
        }
        if (!wordPlaced)
        {
            Debug.Log("Tidak dapat menempatkan kata: " + word + " setelah " + maxTries + " percobaan.");
        }
    }

    private bool CanPlaceWord(string word, int startRow, int startCol, bool isHorizontal)
    {
        int wordLength = word.Length;
        if (isHorizontal)
        {
            if (startCol + wordLength > dataManager.gridCols)
                return false;
            for (int i = 0; i < wordLength; i++)
            {
                if (dataManager.isOccupied[startRow, startCol + i])
                    return false;
            }
        }
        else // Vertikal
        {
            if (startRow + wordLength > dataManager.gridRows)
                return false;
            for (int i = 0; i < wordLength; i++)
            {
                if (dataManager.isOccupied[startRow + i, startCol])
                    return false;
            }
        }
        return true;
    }

    private void PlaceWord(string word, int startRow, int startCol, bool isHorizontal, MeshRenderer renderer)
    {
        WordPlacementData placementData = new WordPlacementData();
        placementData.word = word;
        for (int i = 0; i < word.Length; i++)
        {
            int charIndex = reverseWordOrder ? word.Length - 1 - i : i;
            int currentRow = isHorizontal ? startRow : startRow + i;
            int currentCol = isHorizontal ? startCol + i : startCol;
            int gridIndex = (currentRow * dataManager.gridCols) + currentCol;
            GameObject block = dataManager.gridBlocks[currentRow][currentCol];
            MeshRenderer blockRenderer = block.GetComponent<MeshRenderer>();
            placementData.letters.Add(word[charIndex]);
            placementData.gridIndices.Add(gridIndex);
            if (blockRenderer != null && blockRenderer.sharedMaterial != null)
            {
                placementData.materialNames.Add(blockRenderer.sharedMaterial.name);
            }
            else
            {
                placementData.materialNames.Add("Renderer Kosong");
            }
            char letter = word[charIndex];
            int letterIndex = letter - 'A';
            if (letterIndex >= 0 && letterIndex < dataManager.alphabetMeshes.Count)
            {
                MeshFilter mf = block.GetComponent<MeshFilter>();
                if (mf != null)
                {
                    mf.sharedMesh = dataManager.alphabetMeshes[letterIndex].sharedMesh;
                }
                if (blockRenderer != null)
                {
                    blockRenderer.sharedMaterial = renderer.sharedMaterial;
                }
            }
            dataManager.isOccupied[currentRow, currentCol] = true;
        }
        wordPlacements.Add(placementData);
    }

    private void FillRemainingBlocksRandomly()
    {
        MeshRenderer defaultRenderer = dataManager.blockRenderers[randomRendererIndex];
        for (int i = 0; i < dataManager.gridRows; i++)
        {
            for (int j = 0; j < dataManager.gridCols; j++)
            {
                if (!dataManager.isOccupied[i, j])
                {
                    GameObject block = dataManager.gridBlocks[i][j];
                    int randomIndex = Random.Range(0, dataManager.alphabetMeshes.Count);
                    MeshFilter mf = block.GetComponent<MeshFilter>();
                    if (mf != null)
                    {
                        mf.sharedMesh = dataManager.alphabetMeshes[randomIndex].sharedMesh;
                    }
                    MeshRenderer mr = block.GetComponent<MeshRenderer>();
                    if (mr != null)
                    {
                        mr.sharedMaterial = defaultRenderer.sharedMaterial;
                    }
                }
            }
        }
    }

    public void UpdateWordPlacementData()
    {
        if (dataManager == null)
        {
            Debug.Log("Referensi Datagame3 tidak diatur.");
            return;
        }
        foreach (var placementData in wordPlacements)
        {
            placementData.materialNames.Clear();
        }
        foreach (var placementData in wordPlacements)
        {
            foreach (var gridIndex in placementData.gridIndices)
            {
                int row = gridIndex / dataManager.gridCols;
                int col = gridIndex % dataManager.gridCols;
                if (row >= 0 && row < dataManager.gridRows && col >= 0 && col < dataManager.gridCols)
                {
                    GameObject block = dataManager.gridBlocks[row][col];
                    MeshRenderer blockRenderer = block.GetComponent<MeshRenderer>();
                    if (blockRenderer != null && blockRenderer.sharedMaterial != null)
                    {
                        placementData.materialNames.Add(blockRenderer.sharedMaterial.name);
                    }
                    else
                    {
                        placementData.materialNames.Add("Renderer Kosong");
                    }
                }
                else
                {
                    Debug.Log($"Index grid tidak valid: {gridIndex}. Periksa apakah dimensi grid di Datagame3 berubah setelah generate.");
                    placementData.materialNames.Add("Index tidak valid");
                }
            }
        }
    }

    /// <summary>
    /// Verifikasi kata yang ditemukan untuk player tertentu berdasarkan index renderer yang berbeda.
    /// Memudahkan scoring individual player.
    /// </summary>
    public int VerifMeshRendererPerPlayer(int indexRendererVerif)
    {
        if (availableRendererNames.Count == 0)
        {
            Debug.Log("List availableRendererNames kosong.");
            return 0;
        }
        if (indexRendererVerif < 0 || indexRendererVerif >= availableRendererNames.Count)
        {
            Debug.Log("Index renderer verifikasi tidak valid: " + indexRendererVerif);
            return 0;
        }
        string targetMaterialName = availableRendererNames[indexRendererVerif];
        int countMatches = 0;

        // Bersihkan list foundWords agar data sesuai verifikasi terbaru
        foundWords.Clear();

        foreach (var placementData in wordPlacements)
        {
            bool allLettersMatch = true;
            bool allBlocksActive = true;
            foreach (var gridIndex in placementData.gridIndices)
            {
                if (dataManager != null && gridIndex >= 0 && gridIndex < dataManager.allGridBlocks.Count)
                {
                    GameObject block = dataManager.allGridBlocks[gridIndex];
                    if (block == null || !block.activeSelf)
                    {
                        allBlocksActive = false;
                        break;
                    }
                }
            }
            if (!allBlocksActive)
                continue;
            foreach (var materialName in placementData.materialNames)
            {
                if (string.Compare(materialName, targetMaterialName) != 0)
                {
                    allLettersMatch = false;
                    break;
                }
            }
            if (allLettersMatch)
            {
                countMatches++;

                // Simpan kata dan indeks grid-nya dalam foundWords
                FoundWordData found = new FoundWordData();
                found.word = placementData.word;
                found.gridIndicesFoundword.AddRange(placementData.gridIndices);
                foundWords.Add(found);

                Debug.Log($"[Player Verif] Kata '{placementData.word}' cocok untuk material index {indexRendererVerif} ({targetMaterialName})");
            }
        }
        Debug.Log($"Total kata cocok untuk player (material index {indexRendererVerif}): {countMatches}");
        return countMatches;
    }


    public void CheckIfAllWordBlocksAreDead()
    {
        bool allDead = true;
        foreach (var placementData in wordPlacements)
        {
            foreach (var gridIndex in placementData.gridIndices)
            {
                if (dataManager != null && gridIndex >= 0 && gridIndex < dataManager.allGridBlocks.Count)
                {
                    GameObject block = dataManager.allGridBlocks[gridIndex];
                    if (block != null && block.activeSelf)
                    {
                        allDead = false;
                        break;
                    }
                }
            }
            if (!allDead) break;
        }
        if (allDead)
        {
            A = 1;
            Debug.Log("SEMUA game object balok kata wordsToPlace sudah MATI. A di-set ke 1.");
        }
        else
        {
            A = 0;
            Debug.Log("MASIH ADA game object balok kata wordsToPlace yang HIDUP. A di-set ke 0.");
        }
    }
}