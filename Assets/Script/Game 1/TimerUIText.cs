using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIText : MonoBehaviour
{
    [Header("Referensi StartGame")]
    public StartGame StartGame;
    [Header("Referensi QuestionAnswerArray")]
    public QuestionAnswerArray questionAnswerArray;
    [Header("Referensi QuestionIndexElimination")]
    public QuestionIndexElimination QuestionIndexElimination;
    [Header("Referensi QuestionIndexEliminationtwice")]
    public QuestionIndexEliminationtwice QuestionIndexEliminationtwice;
    [Header("Referensi respawnGameobjectAnswer")]
    public GameObject respawnGameobjectAnswer;
    [Header("Referensi ParentObjectNameReader")]
    public ParentObjectNameReader ParentObjectNameReader;
    [Header("Referensi FinalQuestionIndexElimination")]
    public FinalQuestionIndexElimination finalQuestionIndexElimination;
    [Header("Referensi DisplaySelectedQuestion")]
    public DisplaySelectedQuestion DisplaySelectedQuestion;
    [Header("Referensi GameResult")]
    public GameResult GameResult;
    public Text DisplaySelectedQuestionText;
    public float totalTime = 300; // Total waktu dalam detik (5 menit)
    public Text timerText; // Referensi ke UI Text untuk menampilkan timer

    
    public void Update()
    {
        if (totalTime > 0)
        {
            
            // Kurangi total waktu dengan waktu yang berlalu setiap frame
            totalTime -= Time.deltaTime;

            // Hitung menit dan detik
            int minutes = Mathf.FloorToInt(totalTime / 60);
            int seconds = Mathf.FloorToInt(totalTime % 60);
            StartGame.enabled = false;
            // Perbarui teks timer
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            questionAnswerArray.enabled = false;
            QuestionIndexElimination.enabled = false;
            QuestionIndexEliminationtwice.enabled = false;
            finalQuestionIndexElimination.enabled = false;
            DisplaySelectedQuestion.enabled = false;
            //respawnGameobjectAnswer.gameObject.SetActive(false);
            ParentObjectNameReader.enabled = false;
            DisplaySelectedQuestionText.gameObject.SetActive(false);
            // Jika waktu habis
            timerText.text = "Time's up!";
            GameResult.enabled = true;
            GameResult.ShowResults();
            ParentObjectNameReader.DestroyAll();
            totalTime = 0;
            enabled = false;// Pastikan totalTime tidak menjadi negatif
        }
    }
}
