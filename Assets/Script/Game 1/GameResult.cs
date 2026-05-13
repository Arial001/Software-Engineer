using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    [Header("References")]
    public ParentObjectNameReader parentObjectNameReader;
    [Header("References TimerGameResult")]
    public TimerGameResult TimerGameResult;
    public Text congratulationText;
    public Text resultText;

    void Start()
    {
        if (parentObjectNameReader == null || congratulationText == null || resultText == null)
        {
            Debug.LogError("Mohon lengkapi referensi yang dibutuhkan di Inspector!");
            return;
        }

        ShowResults();
    }

    public void ShowResults()
    {
        // Aktifkan kedua text
        congratulationText.gameObject.SetActive(true);
        resultText.gameObject.SetActive(true);
        TimerGameResult.enabled = true;
        TimerGameResult.totalTime = 10;

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        int totalAnswers = parentObjectNameReader.GetNumberAnswers();
        congratulationText.text = "Selamat kamu telah menyelesaikan tantangan.";
        resultText.text = $"Kamu menyelesaikan {totalAnswers} pertanyaan dalam waktu 5 menit.";
    }

    // Method untuk menyembunyikan text jika diperlukan
    public void HideResults()
    {
        congratulationText.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
    }
}
