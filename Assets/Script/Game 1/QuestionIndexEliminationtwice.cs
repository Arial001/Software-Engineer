using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestionIndexEliminationtwice : MonoBehaviour
{
    [Header("Referensi QuestionIndexElimination")]
    public QuestionIndexElimination randomQuestionAnswer;
    [Header("Referensi FinalQuestionIndexElimination")]
    public FinalQuestionIndexElimination FinalQuestionIndexElimination;
    public event Action OnResetFinalQuestionIndexElimination;

    private int selectedIndex = -1;
    private int Reset;
    public int b;
    private List<string> combinedList = new List<string>();
    private List<string> combinedQuestionsAndAnswers = new List<string>();

    private void Start()
    {
        randomQuestionAnswer.OnStartTwice += ResetScript;
        Reset = randomQuestionAnswer.GetReset();
        Initialize();
    }

    /*private void Update()
    {
        Reset = randomQuestionAnswer.GetReset();
        Debug.Log($"nilai reset = {Reset}");
        if (Reset == 1)
        {
            Debug.Log("Reset terdeteksi. Memulai ulang script QuestionIndexEliminationtwice.");
            ResetScript();
            
        }
    }*/

    private void Initialize()
    {
        Reset = randomQuestionAnswer.GetReset();
        if (randomQuestionAnswer == null)
        {
            Debug.LogError("RandomQuestionAnswer belum diatur!");
            return;
        }
        FinalQuestionIndexElimination.enabled = false;

        // Ambil data soal dan jawaban dari RandomQuestionAnswer
        combinedList = GetCombinedAnswersAndQuestions();

        // Pilih satu index jawaban secara acak
        selectedIndex = RandomizeSingleIndex();

        // Cetak hasil randomisasi
        PrintSelectedIndexAndQuestions();
        OnResetFinalQuestionIndexElimination?.Invoke();
    }

    public List<string> GetCombinedAnswersAndQuestions()
    {
        return randomQuestionAnswer.GetCombinedAnswersAndQuestions();
    }
    public List<string> GetCombinedAnswersQuestions()
    {
        return combinedQuestionsAndAnswers;
    }

    private int RandomizeSingleIndex()
    {
        List<string> answers = randomQuestionAnswer.GetAllRandomizedAnswers();
        if (answers.Count > 0)
        {
            int randomIndex = Random.Range(0, answers.Count);
            string selectedAnswer = answers[randomIndex];

            // Cari index dari jawaban yang terpilih dalam combinedList
            for (int i = 0; i < combinedList.Count; i++)
            {
                if (combinedList[i] == selectedAnswer)
                {
                    //Debug.Log($"Jawaban terpilih: {selectedAnswer}, Index: {i}");
                    return i;
                }
            }
        }

        Debug.LogWarning("Tidak ada jawaban yang tersedia untuk dipilih.");
        return -1;
    }

    private void PrintSelectedIndexAndQuestions()
    {
        combinedQuestionsAndAnswers.Clear();
        if (selectedIndex >= 0 && selectedIndex < combinedList.Count)
        {
            string jawaban = combinedList[selectedIndex];
            combinedQuestionsAndAnswers.Add(combinedList[selectedIndex]);
            //Debug.Log($"Jawaban Terpilih: {jawaban}");
            //Debug.Log("Pertanyaan untuk Jawaban tersebut:");
            

            // Tampilkan pertanyaan-pertanyaan setelah jawaban terpilih
            for (int i = selectedIndex + 1; i < combinedList.Count; i++)
            {
                if (randomQuestionAnswer.GetAllRandomizedAnswers().Contains(combinedList[i]))
                {
                    break; // Berhenti jika menemukan jawaban berikutnya
                }
                combinedQuestionsAndAnswers.Add(combinedList[i]);
                //Debug.Log($"- {combinedList[i]}"); // Debug untuk pertanyaan
            }

            FinalQuestionIndexElimination.enabled = true;
        }
        else
        {
            Debug.LogWarning($"Tidak ada data untuk indeks {selectedIndex}.");
        }
    }

    public void ResetScript()
    {
        //b = 1;
        FinalQuestionIndexElimination.enabled = false;
        Initialize();
        //Reset = 0;
        combinedList.Clear();
        selectedIndex = -1;
        
        //StartCoroutine(ResetB());

        Debug.Log("Script QuestionIndexEliminationtwice berhasil di-reset.");
    }
    private void OnDestroy()
    {
        randomQuestionAnswer.OnStartTwice -= ResetScript; // Unsubscribe to prevent memory leaks
    }
    IEnumerator ResetB()
    {
        yield return null; // Tunggu satu frame agar script lain bisa membaca nilai
        b = 0;
    }

    public int ReadSelectedIndex()
    {
        return selectedIndex;
    }
    public int GetReset()
    {
        return b;
    }
}
