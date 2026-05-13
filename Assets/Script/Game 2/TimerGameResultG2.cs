using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerGameResultG2 : MonoBehaviour
{
    //[Header("Referensi GameResult")]
    //public GameResult GameResult;
    //[Header("Referensi StartGameManager")]
    //public StartGameManager StartGameManager;
    [Header("Referensi StartGame3")]
    public StartGame2 StartGame2;
    public float totalTime = 10; // Total waktu dalam detik (5 menit)
    public Text timerText; // Referensi ke UI Text untuk menampilkan timer


    void Update()
    {
        if (totalTime > 0)
        {
            timerText.gameObject.SetActive(true);
            // Kurangi total waktu dengan waktu yang berlalu setiap frame
            totalTime -= Time.deltaTime;

            // Hitung menit dan detik
            int minutes = Mathf.FloorToInt(totalTime / 60);
            int seconds = Mathf.FloorToInt(totalTime % 60);

            // Perbarui teks timer
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            StartGame2.enabled = true;
            //GameResult.enabled = false;
            //GameResult.HideResults();
            //StartGameManager.enabled = true;
            //StartGameManager.ResetScript();
            //StartGameManager.StartWelcomeGames();
            Debug.Log("menyalakan StartGame.SetWelcomeMessage();");

            // Jika waktu habis
            timerText.text = "Time's up!";
            timerText.text = string.Empty;
            timerText.gameObject.SetActive(false);
            enabled = false;
            totalTime = 0; // Pastikan totalTime tidak menjadi negatif
        }
    }
}
