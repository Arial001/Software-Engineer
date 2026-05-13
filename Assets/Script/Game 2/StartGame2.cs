using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame2 : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("UI References")]
    public Text welcomeText;
    [Header("Referensi Script InteractStartG3")]
    public GrabObjectG2 GrabObjectG2;
    [Header("Referensi Script GrabObjectP2G2")]
    public List<GrabObjectP2G2> GrabObjectP2G2 = new List<GrabObjectP2G2>();
    [Header("Referensi Scripts Verifjawaban")]
    public GameObject Verifjawaban;
    [Header("Referensi Scripts ObjectPlayG2")]
    public ObjectPlayG2 ObjectPlayG2;
    [Header("Referensi Scripts JamakRotationObject")]
    public JamakRotationObject JamakRotationObject;
    [Header("Referensi Scripts PertanyaanTebakGambar")]
    public PertanyaanTebakGambar PertanyaanTebakGambar;
    [Header("Referensi Scripts InteractStartP2G2")]
    public List<InteractStartP2G2> InteractStartP2Game2 = new List<InteractStartP2G2>();
    [Header("Referensi Scripts InteractStartG2")]
    public InteractStartG2 InteractStartGame2;
    [Header("Referensi Scripts TimerGameResultG3")]
    public TimerGameResultG2 TimerGameResultG2;
    [Header("Referensi Scripts ResultGame2")]
    public ResultGame2 ResultGame2;
    private int a;
    private int b;
    private int c;
    private int d;


    //[Header("Referensi QuestionAnswerArray")]
    //public QuestionAnswerArray questionAnswerArray;
    //[Header("Referensi QuestionIndexElimination")]
    //public QuestionIndexElimination QuestionIndexElimination;
    private void Start()
    {

        if (welcomeText == null)
        {
            Debug.LogError("Text component belum diatur di Inspector!");
            return;
        }

        SetWelcomeMessage();
    }
    public void ActivateAllGrabObjectP2G2()
    {
        foreach (GrabObjectP2G2 GrabObject in GrabObjectP2G2)
        {
            if (GrabObject != null)
            {
                GrabObject.enabled = true;
                Debug.Log($"VerifControlG2: Script '{GrabObject.name}' pada GameObject '{GrabObject.gameObject.name}' diaktifkan.", this);
                // Panggil juga Initialize jika script ParentObjectNameReaderG2 perlu inisialisasi ulang
                // readerScript.InitializeVerification();
                // readerScript.ProcessChildren();
            }
        }
    }
    public void DeactivateAllGrabObjectP2G2()
    {
        foreach (GrabObjectP2G2 GrabObject in GrabObjectP2G2)
        {
            if (GrabObject != null)
            {
                GrabObject.enabled = false;
                Debug.Log($"VerifControlG2: Script '{GrabObject.name}' pada GameObject '{GrabObject.gameObject.name}' dinonaktifkan.", this);
            }
        }
    }

    public void SetWelcomeMessage()
    {
        //Verif.text = string.Empty;
        //questionAnswerArray.enabled = false;
        //QuestionIndexElimination.enabled = false;
        //ParentObjectNameReader.enabled = false;
        //TimerUIText.enabled = false;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = "SELAMAT DATANG\nTekan R / O untuk Memulai";
        welcomeText.alignment = TextAnchor.MiddleCenter;
        welcomeText.fontSize = 200;

    }
    public void StartGame()
    {
        StartCoroutine(Reload());
    }
    private IEnumerator Reload()
    {
        //Verif.text = string.Empty;
        //questionAnswerArray.enabled = false;
        //QuestionIndexElimination.enabled = false;
        //ParentObjectNameReader.enabled = false;
        //TimerUIText.enabled = false;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = "SELAMAT DATANG\nTekan R / O untuk Memulai";
        welcomeText.alignment = TextAnchor.MiddleCenter;
        welcomeText.fontSize = 200;
        InteractStartP2Game2[0].enabled = false;
        InteractStartP2Game2[1].enabled = false;
        InteractStartP2Game2[2].enabled = false;
        InteractStartGame2.enabled = false;
        DeactivateAllGrabObjectP2G2();
        GrabObjectG2.enabled = false;
        yield return new WaitForSeconds(1.0f);
        ObjectPlayG2.DummyFloorPokerON();
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = "KALIAN HARUS MENARUH DAN MENYAMAKAN KARTU DI ATAS MEJA DENGAN YANG ADA DI LANTAI";
        welcomeText.alignment = TextAnchor.UpperLeft;
        welcomeText.fontSize = 190;
        yield return new WaitForSeconds(5.0f);
        Verifjawaban.SetActive(true);
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = "KALIAN AKAN DIKASIH WAKTU 5 MENIT";
        welcomeText.alignment = TextAnchor.MiddleCenter;
        welcomeText.fontSize = 190;
        yield return new WaitForSeconds(3.0f);
        ObjectPlayG2.FloorPokerON();
        yield return new WaitForSeconds(1.1f);
        PertanyaanTebakGambar.Tdown();
        yield return new WaitForSeconds(3.1f);
        ObjectPlayG2.DummyFloorPokerOFF();
        yield return new WaitForSeconds(0.1f);
        JamakRotationObject.PutarKedepan();
        yield return new WaitForSeconds(2.1f);
        ObjectPlayG2.BeforeStart();
        yield return new WaitForSeconds(1.1f);
        ObjectPlayG2.DummyDeskPokerOFF();
        yield return new WaitForSeconds(1.1f);
        ObjectPlayG2.DeskPokerON();
        //InteractStartGame2[1].enabled = true;
        yield return new WaitForSeconds(1.1f);
        JamakRotationObject.PutarKeatas();
        yield return new WaitForSeconds(1.2f);
        ObjectPlayG2.AfterStart();
        yield return new WaitForSeconds(0.1f);
        ObjectPlayG2.CameraTOPG2ON();
        a = 0;
        b = 0;
        c = 0;
        d = 0;
        yield return new WaitForSeconds(10.1f);
        ObjectPlayG2.CameraTOPG2OFF();
        yield return new WaitForSeconds(0.5f);
        ObjectPlayG2.PlayGame2();
        yield return new WaitForSeconds(0.5f);
        ObjectPlayG2.CekposisiKamera();
        yield return new WaitForSeconds(1.5f);
        JamakRotationObject.PutarKebawah();
        yield return new WaitForSeconds(1.5f);
        GrabObjectG2.enabled = true;
        ActivateAllGrabObjectP2G2();
        yield return new WaitForSeconds(1.5f);
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        TimerGameResultG2.enabled = true;
        TimerGameResultG2.totalTime = 300;
        yield return new WaitForSeconds(300.0f);
        Verifjawaban.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        JamakRotationObject.ResetToOriginal();
        yield return new WaitForSeconds(1.0f);
        JamakRotationObject.PutarKedepan();
        yield return new WaitForSeconds(1.0f);
        JamakRotationObject.PutarKebawah();
        yield return new WaitForSeconds(1.0f);
        ObjectPlayG2.FloorPokerOFF();
        yield return new WaitForSeconds(1.1f);
        ObjectPlayG2.DummyDeskPokerON();
        yield return new WaitForSeconds(1.1f);
        ObjectPlayG2.DeskPokerOFF();
        yield return new WaitForSeconds(1.1f);
        ResultGame2.HitungNilai();
        a = ResultGame2.Player1;
        b = ResultGame2.Player2;
        c = ResultGame2.Player3;
        d = ResultGame2.Player4;
        yield return new WaitForSeconds(0.5f);
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = $"player 1 menebak = {a} kartu\n" +
                       $"player 2 menebak = {b} kartu\n" +
                       $"player 3 menebak = {c} kartu\n" +
                       $"player 4 menebak = {d} kartu\n";
        welcomeText.alignment = TextAnchor.UpperLeft;
        welcomeText.fontSize = 180;
        yield return new WaitForSeconds(5.1f);
        SetWelcomeMessage();
        yield return new WaitForSeconds(1.1f);
        InteractStartP2Game2[0].enabled = true;
        InteractStartP2Game2[1].enabled = true;
        InteractStartP2Game2[2].enabled = true;
        InteractStartGame2.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
