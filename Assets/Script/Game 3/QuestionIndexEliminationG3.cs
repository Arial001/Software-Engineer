using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Collections;

public class QuestionIndexEliminationG3 : MonoBehaviour
{
    [Header("Referensi QuestionAnswerArray")]
    public QuestionAnswerArrayG2 questionAnswerArray;

    [Header("Data Terpilih")]
    [Tooltip("Variabel untuk menyimpan pertanyaan yang dipilih.")]
    public string selectedQuestion;
    [Tooltip("List untuk menyimpan jawaban yang dipilih.")]
    public List<string> selectedAnswers = new List<string>();
    public GeneratorGame3 GeneratorGame3;

    private void Start()
    {
        //SelectRandomQuestion();
        //StartCoroutine(Reload());
    }

    /// <summary>
    /// Memilih satu set pertanyaan dan jawaban secara acak.
    /// </summary>
    public void SelectRandomQuestion()
    {
        // Pastikan referensi ke skrip lain tidak kosong
        if (questionAnswerArray == null)
        {
            Debug.LogError("QuestionAnswerArray belum diatur!");
            return;
        }

        // Ambil jumlah total set pertanyaan
        int totalSets = questionAnswerArray.readQuestionSets.Count;

        // Periksa apakah ada cukup set untuk dipilih
        if (totalSets == 0)
        {
            Debug.LogWarning("Tidak ada set pertanyaan yang tersedia di QuestionAnswerArrayG2.");
            return;
        }

        // Pilih satu index secara acak
        int randomIndex = Random.Range(0, totalSets);

        // Bersihkan data sebelumnya
        selectedQuestion = "";
        selectedAnswers.Clear();

        // Ambil set pertanyaan yang dipilih
        var selectedSet = questionAnswerArray.readQuestionSets[randomIndex];

        // --- Simpan pertanyaan dan jawaban ke list terpisah ---
        selectedQuestion = selectedSet.readQuestionName;

        foreach (var answer in selectedSet.readAnswerNames)
        {
            selectedAnswers.Add(answer);
        }

        // Log informasi ke konsol
        Debug.Log($"Memilih index ke-{randomIndex} secara acak.");
        Debug.Log($"Pertanyaan terpilih: {selectedQuestion}");
        Debug.Log($"Jumlah Jawaban: {selectedAnswers.Count}");
    }
    public void clear()
    {
        selectedAnswers.Clear();
        selectedQuestion = null;
    }
    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.0f);
        GeneratorGame3.enabled = true;
    }
}