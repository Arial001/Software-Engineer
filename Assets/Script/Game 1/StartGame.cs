using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [Header("UI References")]
    public Text welcomeText;
    [Header("Referensi QuestionAnswerArray")]
    public QuestionAnswerArray questionAnswerArray;
    [Header("Referensi QuestionIndexElimination")]
    public QuestionIndexElimination QuestionIndexElimination;
    [Header("Referensi TimerUIText")]
    public TimerUIText TimerUIText;
    [Header("Referensi ParentObjectNameReader")]
    public ParentObjectNameReader ParentObjectNameReader;


    private void Start()
    {
        
        if (welcomeText == null)
        {
            Debug.LogError("Text component belum diatur di Inspector!");
            return;
        }

        SetWelcomeMessage();
    }

    public void SetWelcomeMessage()
    {
        //Verif.text = string.Empty;
        questionAnswerArray.enabled = false;
        QuestionIndexElimination.enabled = false;
        ParentObjectNameReader.enabled = false;
        TimerUIText.enabled = false;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = "SELAMAT DATANG\nTekan R / O untuk Memulai";
        welcomeText.alignment = TextAnchor.MiddleCenter;
        welcomeText.fontSize = 200;
    }
    public void Begining()
    {
        TimerUIText.enabled = true;
        TimerUIText.timerText.text = string.Empty;
        //Verif.text = string.Empty;
        questionAnswerArray.enabled = true;
        QuestionIndexElimination.enabled = true;
        QuestionIndexElimination.ResetScript();
        TimerUIText.totalTime = 300;
        Debug.Log("Parent menambah totaltime");
        //Debug.Log("menyalakan TimerUIText dari startGame");
        ParentObjectNameReader.enabled = true;
    }
}
