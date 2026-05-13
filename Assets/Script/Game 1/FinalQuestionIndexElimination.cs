using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class FinalQuestionIndexElimination : MonoBehaviour
{
    [Header("Referensi QuestionIndexEliminationtwice")]
    public QuestionIndexEliminationtwice questionIndexEliminationTwice;
    public DisplaySelectedQuestion DisplaySelectedQuestion;
    [Header("Referensi ParentObjectNameReader")]
    public ParentObjectNameReader ParentObjectNameReader;

    private List<string> combinedList;
    public string randomizedQuestion;
    public string randomizedAnswer;
    private List<string> selectedQuestions = new List<string>();
    public event Action OnResetDisplay;

    private void Start()
    {
        questionIndexEliminationTwice.OnResetFinalQuestionIndexElimination += ResetScript;
        Initialize();
    }

    private void Initialize()
    {
        ParentObjectNameReader.enabled = false;
        DisplaySelectedQuestion.enabled = false;

        if (questionIndexEliminationTwice == null)
        {
            Debug.LogError("QuestionIndexEliminationtwice belum diatur!");
            return;
        }
        
        // Ambil data dari QuestionIndexEliminationtwice
        combinedList = questionIndexEliminationTwice.GetCombinedAnswersQuestions();
        //Debug.Log($"Jumlah pertanyaan yang didapat: {combinedList.Count}");

        // Debug untuk membaca setiap isi indeks dari combinedList
        /*for (int i = 0; i < combinedList.Count; i++)
        {
            //Debug.Log($"Isi index {i}: {combinedList[i]}");
        }*/
        
        // Pastikan ada cukup elemen dalam combinedList
        if (combinedList.Count > 0)
        {
            // Ambil jawaban dari indeks 0
            randomizedAnswer = combinedList[0];
            //Debug.Log($"Jawaban yang diambil dari indeks 0: {randomizedAnswer}");

            // Ambil sisa pertanyaan setelah indeks 0
            selectedQuestions.Clear();
            for (int i = 1; i < combinedList.Count; i++)
            {
                selectedQuestions.Add(combinedList[i]);
            }
            
            // Lakukan randomisasi untuk memilih satu pertanyaan
            string randomizedQuestion = RandomizeQuestion(selectedQuestions);
            //Debug.Log($"Pertanyaan yang dipilih secara acak: {randomizedQuestion}");

            // Print pertanyaan dan jawaban terpilih
            PrintSelectedQuestionAndAnswer(randomizedQuestion, randomizedAnswer);
            OnResetDisplay?.Invoke();
        }
        else
        {
            Debug.LogWarning("Tidak ada data yang tersedia di combinedList.");
        }
    }

    private void ExtractQuestionsForSelectedAnswer()
    {
        selectedQuestions.Clear();
        bool isCollectingQuestions = false;

        for (int i = 0; i < combinedList.Count; i++)
        {
            if (isCollectingQuestions)
            {
                if (questionIndexEliminationTwice.randomQuestionAnswer.GetAllRandomizedAnswers().Contains(combinedList[i]))
                {
                    break; // Berhenti jika menemukan jawaban berikutnya
                }
                selectedQuestions.Add(combinedList[i]);
            }
            else if (combinedList[i] == randomizedAnswer)
            {
                isCollectingQuestions = true;
            }
        }
    }

    public string RandomizeQuestion(List<string> questions)
    {
        if (questions.Count == 0)
        {
            Debug.LogWarning("Tidak ada pertanyaan untuk dipilih.");
            return "Tidak ada pertanyaan";
        }

        int randomIndex = Random.Range(0, questions.Count);
        randomizedQuestion = questions[randomIndex];
        return randomizedQuestion;
    }

    public void PrintSelectedQuestionAndAnswer(string question, string answer)
    {
        //Debug.Log($"Pertanyaan Terpilih: {question}");
        //Debug.Log($"Jawaban Terkait: {answer}");
        randomizedAnswer = answer;
        DisplaySelectedQuestion.enabled = true;
        ParentObjectNameReader.enabled = true;
    }

    public void ResetScript()
    {
        ParentObjectNameReader.enabled = false;
        DisplaySelectedQuestion.enabled = false;
        //combinedList.Clear();
        randomizedAnswer = null;
        randomizedQuestion = null;

        Initialize();

        Debug.Log("Script FinalQuestionIndexElimination berhasil di-reset.");
    }

    private void OnDestroy()
    {
        questionIndexEliminationTwice.OnResetFinalQuestionIndexElimination -= ResetScript; // Unsubscribe to prevent memory leaks
    }

    public string GetRandomizedQuestion()
    {
        return randomizedQuestion;
    }

    public string GetRandomizedAnswer()
    {
        return randomizedAnswer;
    }
}
