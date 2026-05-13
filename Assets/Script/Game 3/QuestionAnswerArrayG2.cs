using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Kelas ini untuk input manual Anda
[System.Serializable]
public class TTS_QuestionSet
{
    [Header("Pertanyaan (GameObject UI Text)")]
    public GameObject question;

    [Header("Daftar Jawaban (GameObject)")]
    public List<GameObject> answers = new List<GameObject>();
}

// Kelas ini untuk menampung data yang dibaca otomatis
[System.Serializable]
public class TTS_ReadQuestionSet
{
    public string readQuestionName;
    public List<string> readAnswerNames = new List<string>();
}

// Nama kelas utama diubah kembali menjadi QuestionAnswerArrayG2
public class QuestionAnswerArrayG2 : MonoBehaviour
{
    [Header("Input Data Manual")]
    public List<TTS_QuestionSet> questionSets = new List<TTS_QuestionSet>();

    [Header("Data Hasil Baca Otomatis")]
    // [HideInInspector] // Anda bisa hapus komentar ini jika tidak ingin melihatnya di Inspector
    public List<TTS_ReadQuestionSet> readQuestionSets = new List<TTS_ReadQuestionSet>();

    private void Start()
    {
        ReadDataAndPopulateLists();
        PrintAllData();
    }

    /// <summary>
    /// Membaca nama objek dari list input dan mengisi list hasil baca.
    /// </summary>
    public void ReadDataAndPopulateLists()
    {
        readQuestionSets.Clear();

        foreach (var set in questionSets)
        {
            TTS_ReadQuestionSet newReadSet = new TTS_ReadQuestionSet();

            // --- Perubahan di sini untuk membaca isi teks UI Text ---
            if (set.question != null)
            {
                Text uiTextComponent = set.question.GetComponent<Text>();
                if (uiTextComponent != null)
                {
                    newReadSet.readQuestionName = uiTextComponent.text;
                }
                else
                {
                    Debug.LogError("Objek pertanyaan tidak memiliki komponen UI Text: " + set.question.name);
                    newReadSet.readQuestionName = "Komponen Text Kosong";
                }
            }
            else
            {
                newReadSet.readQuestionName = "Pertanyaan Kosong";
            }

            // Membaca nama-nama GameObject jawaban
            foreach (var answerObject in set.answers)
            {
                if (answerObject != null)
                {
                    newReadSet.readAnswerNames.Add(answerObject.name);
                }
                else
                {
                    newReadSet.readAnswerNames.Add("Jawaban Kosong");
                }
            }

            readQuestionSets.Add(newReadSet);
        }
    }
    public void clear()
    {
        readQuestionSets.Clear();
    }

    // Fungsi Debug untuk menampilkan semua data
    public void PrintAllData()
    {
        Debug.Log("--- Menampilkan Data dari List Manual ---");
        for (int i = 0; i < questionSets.Count; i++)
        {
            Debug.Log($"Set {i + 1}:");
            // Bagian ini masih menampilkan nama objek, yang bagus untuk debugging
            Debug.Log($"   Pertanyaan: {questionSets[i].question?.name ?? "Kosong"}");

            for (int j = 0; j < questionSets[i].answers.Count; j++)
            {
                Debug.Log($"   Jawaban {j + 1}: {questionSets[i].answers[j]?.name ?? "Kosong"}");
            }
        }

        Debug.Log("\n--- Menampilkan Data dari List Hasil Baca Otomatis ---");
        for (int i = 0; i < readQuestionSets.Count; i++)
        {
            Debug.Log($"Set Hasil Baca {i + 1}:");
            Debug.Log($"   Nama Pertanyaan: {readQuestionSets[i].readQuestionName}");

            for (int j = 0; j < readQuestionSets[i].readAnswerNames.Count; j++)
            {
                Debug.Log($"   Nama Jawaban {j + 1}: {readQuestionSets[i].readAnswerNames[j]}");
            }
        }
    }
}