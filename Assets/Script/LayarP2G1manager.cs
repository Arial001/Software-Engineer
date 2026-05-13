using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayarP2G1manager : MonoBehaviour
{
    public GameObject layarP2;
    [Header("Referensi QuestionAnswerArray")]
    public QuestionAnswerArray questionAnswerArray;
    [Header("Referensi QuestionIndexElimination")]
    public QuestionIndexElimination QuestionIndexElimination;
    [Header("Referensi QuestionIndexEliminationtwice")]
    public QuestionIndexEliminationtwice QuestionIndexEliminationtwice;
    [Header("Referensi FinalQuestionIndexElimination")]
    public FinalQuestionIndexElimination finalQuestionIndexElimination;
    [Header("Referensi DisplaySelectedQuestion")]
    public DisplaySelectedQuestion DisplaySelectedQuestion;
    [Header("Referensi respawnGameobjectAnswer")]
    public respawnGameobjectAnswer respawnGameobjectAnswer;
    [Header("Referensi ParentObjectNameReader")]
    public ParentObjectNameReader ParentObjectNameReader;
    [Header("Referensi GameResult")]
    public GameResult GameResult;
    [Header("References TimerGameResult")]
    public TimerGameResult TimerGameResult;

    [Header("Referensi StartGame")]
    public StartGame StartGame;
    [Header("References TimerUIText")]
    public TimerUIText TimerUIText;
    [Header("Referensi ChildObjectChecker")]
    public ChildObjectChecker ChildObjectChecker;


    void Start()
    {

    }
    public void ALLOFF()
    {
        questionAnswerArrayOFF();
        QuestionIndexEliminationOFF();
        QuestionIndexEliminationtwiceOFF();
        finalQuestionIndexEliminationOFF();
        DisplaySelectedQuestionOFF();
        respawnGameobjectAnswerOFF();
        ParentObjectNameReaderOFF();
        GameResultOFF();
        TimerGameResultOFF();
        StartGameOFF();
        TimerGameResult.totalTime = 0;
        TimerUIText.totalTime = 0;
        GameResult.HideResults();
        //TimerGameResult.timerText.gameObject.SetActive(false);
        //TimerUIText.timerText.gameObject.SetActive(false);
    }

    public void CheckandFixChildrenON()
    {
        ChildObjectChecker.CheckAndFixChildren();
    }
    public void Layar1OFF()
    {
        layarP2.gameObject.SetActive(false);
    }
    public void Layar1ON()
    {
        layarP2.gameObject.SetActive(true);
    }
    public void questionAnswerArrayOFF()
    {
        questionAnswerArray.enabled = false;
    }
    public void QuestionIndexEliminationOFF()
    {
        QuestionIndexElimination.enabled = false;
    }
    public void QuestionIndexEliminationtwiceOFF()
    {
        QuestionIndexEliminationtwice.enabled = false;
    }
    public void finalQuestionIndexEliminationOFF()
    {
        finalQuestionIndexElimination.enabled = false;
    }
    public void DisplaySelectedQuestionOFF()
    {
        DisplaySelectedQuestion.enabled = false;
    }
    public void respawnGameobjectAnswerOFF()
    {
        respawnGameobjectAnswer.enabled = false;
    }
    public void ParentObjectNameReaderOFF()
    {
        ParentObjectNameReader.enabled = false;
    }
    public void GameResultOFF()
    {
        GameResult.enabled = false;
    }
    public void TimerGameResultOFF()
    {
        TimerGameResult.enabled = false;
    }
    public void StartGameOFF()
    {
        StartGame.enabled = false;
    }
    public void StartGameON()
    {
        StartGame.enabled = true;
        StartGame.SetWelcomeMessage();
    }
}
