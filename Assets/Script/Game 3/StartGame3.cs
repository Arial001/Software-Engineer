using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame3 : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("UI References")]
    public Text welcomeText;
    [Header("UI Ending")]
    public Text EndingText;
    [Header("Referensi Script InteractStartG3")]
    public List<InteractStartG3> InteractStartG3 = new List<InteractStartG3>();
    [Header("Referensi Script GeneratorGame3")]
    public GeneratorGame3 GeneratorGame3;
    [Header("Referensi Script Datagame3")]
    public Datagame3 Datagame3;
    [Header("Referensi Script QuestionIndexEliminationG3")]
    public QuestionIndexEliminationG3 QuestionIndexEliminationG3;
    [Header("Referensi Script RotationObjectG3")]
    public List<RotationObjectG3> RotationObjectG3 = new List<RotationObjectG3>();
    [Header("Referensi Scripts TimerGameResultG3")]
    public TimerGameResultG3 TimerGameResultG3;
    [Header("Referensi Scripts InteractLayarG3")]
    public InteractLayarG3 InteractLayarG3;
    [Header("Referensi Scripts InteractLayarP2G3")]
    public List<InteractLayarP2G3> InteractLayarP2G3 = new List<InteractLayarP2G3>();
    [Header("Referensi Scripts ObjectPlayG3")]
    public ObjectPlayG3 ObjectPlayG3;
    [Header("Referensi Scripts ObjectPlayG3")]
    public List<SelectionManager> SelectionManager = new List<SelectionManager>();

    public SelectionManagerG3 SelectionManagerG3;
    private int a;
    private int b;
    private int c;
    private int d;
    private int e;
    private void Start()
    {

        if (welcomeText == null)
        {
            Debug.LogError("Text component belum diatur di Inspector!");
            return;
        }

        SetWelcomeMessage();
    }
    public void TurunkanSemuuaHuruf()
    {
        foreach (RotationObjectG3 RotationObjectG3 in RotationObjectG3)
        {
            if (RotationObjectG3 != null)
            {
                RotationObjectG3.isFirstTargetActive = true;
                RotationObjectG3.ToggleRotation2();
                
                // Panggil juga Initialize jika script ParentObjectNameReaderG2 perlu inisialisasi ulang
                // readerScript.InitializeVerification();
                // readerScript.ProcessChildren();
            }
        }
    }
    public void ActivateAllSelectionManager()
    {
        foreach (SelectionManager SelectionManager in SelectionManager)
        {
            if (SelectionManager != null)
            {
                SelectionManager.enabled = true;
                Debug.Log($"VerifControlG2: Script '{SelectionManager.name}' pada GameObject '{SelectionManager.gameObject.name}' diaktifkan.", this);
                // Panggil juga Initialize jika script ParentObjectNameReaderG2 perlu inisialisasi ulang
                // readerScript.InitializeVerification();
                // readerScript.ProcessChildren();
            }
        }
    }
    public void DeactivateAllSelectionManager()
    {
        foreach (SelectionManager SelectionManager in SelectionManager)
        {
            if (SelectionManager != null)
            {
                SelectionManager.enabled = false;
                Debug.Log($"VerifControlG2: Script '{SelectionManager.name}' pada GameObject '{SelectionManager.gameObject.name}' diaktifkan.", this);
                // Panggil juga Initialize jika script ParentObjectNameReaderG2 perlu inisialisasi ulang
                // readerScript.InitializeVerification();
                // readerScript.ProcessChildren();
            }
        }
    }
    void ActivateAllInteractStartG3()
    {
        foreach (InteractStartG3 InteractStartG3 in InteractStartG3)
        {
            if (InteractStartG3 != null)
            {
                InteractStartG3.enabled = true;
                Debug.Log($"VerifControlG2: Script '{InteractStartG3.name}' pada GameObject '{InteractStartG3.gameObject.name}' diaktifkan.", this);
                // Panggil juga Initialize jika script ParentObjectNameReaderG2 perlu inisialisasi ulang
                // readerScript.InitializeVerification();
                // readerScript.ProcessChildren();
            }
        }
    }
    public void DeactivateAllInteractStartG3()
    {
        foreach (InteractStartG3 InteractStartG3 in InteractStartG3)
        {
            if (InteractStartG3 != null)
            {
                InteractStartG3.enabled = false;
                Debug.Log($"VerifControlG2: Script '{InteractStartG3.name}' pada GameObject '{InteractStartG3.gameObject.name}' diaktifkan.", this);
                // Panggil juga Initialize jika script ParentObjectNameReaderG2 perlu inisialisasi ulang
                // readerScript.InitializeVerification();
                // readerScript.ProcessChildren();
            }
        }
    }
    void ActivateAllInteractLayarP2G3()
    {
        foreach (InteractLayarP2G3 InteractLayarP2G3 in InteractLayarP2G3)
        {
            if (InteractLayarP2G3 != null)
            {
                InteractLayarP2G3.enabled = true;
                Debug.Log($"VerifControlG2: Script '{InteractLayarP2G3.name}' pada GameObject '{InteractLayarP2G3.gameObject.name}' diaktifkan.", this);
                // Panggil juga Initialize jika script ParentObjectNameReaderG2 perlu inisialisasi ulang
                // readerScript.InitializeVerification();
                // readerScript.ProcessChildren();
            }
        }
    }
    public void DeactivateAllInteractLayarP2G3()
    {
        foreach (InteractLayarP2G3 InteractLayarP2G3 in InteractLayarP2G3)
        {
            if (InteractLayarP2G3 != null)
            {
                InteractLayarP2G3.enabled = false;
                Debug.Log($"VerifControlG2: Script '{InteractLayarP2G3.name}' pada GameObject '{InteractLayarP2G3.gameObject.name}' diaktifkan.", this);
                // Panggil juga Initialize jika script ParentObjectNameReaderG2 perlu inisialisasi ulang
                // readerScript.InitializeVerification();
                // readerScript.ProcessChildren();
            }
        }
    }
    public void EnableIsTriggerForRotationObjects()
    {
        foreach (RotationObjectG3 rotObj in RotationObjectG3)
        {
            if (rotObj != null)
            {
                BoxCollider boxCol = rotObj.GetComponent<BoxCollider>();
                if (boxCol != null)
                {
                    boxCol.isTrigger = true;
                    Debug.Log($"isTrigger diaktifkan pada BoxCollider objek '{rotObj.gameObject.name}'.");
                }
                else
                {
                    Debug.LogWarning($"Objek '{rotObj.gameObject.name}' tidak memiliki BoxCollider.");
                }
            }
        }
    }
    public void DeactivateIsTriggerForRotationObjects()
    {
        foreach (RotationObjectG3 rotObj in RotationObjectG3)
        {
            if (rotObj != null)
            {
                BoxCollider boxCol = rotObj.GetComponent<BoxCollider>();
                if (boxCol != null)
                {
                    boxCol.isTrigger = false;
                    Debug.Log($"isTrigger diaktifkan pada BoxCollider objek '{rotObj.gameObject.name}'.");
                }
                else
                {
                    Debug.LogWarning($"Objek '{rotObj.gameObject.name}' tidak memiliki BoxCollider.");
                }
            }
        }
    }




    public void SetWelcomeMessage()
    {
        e = 4;
        //Verif.text = string.Empty;
        //questionAnswerArray.enabled = false;
        //QuestionIndexElimination.enabled = false;
        //ParentObjectNameReader.enabled = false;
        //TimerUIText.enabled = false
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = "SELAMAT DATANG\nTekan R / O untuk Memulai";
        welcomeText.alignment = TextAnchor.MiddleCenter;
        welcomeText.fontSize = 200;
        TimerGameResultG3.timerText.text = string.Empty;
        TurunkanSemuuaHuruf();
        ActivateAllSelectionManager();
        ActivateAllInteractLayarP2G3();
        InteractLayarG3.enabled = true;
        DeactivateAllInteractStartG3();

    }
    public void MatikanPaksa()
    {
        StopCoroutine(Reload());
        StartCoroutine(Force());
    }
    public void StartGame()
    {
        StartCoroutine(Reload());
    }
    private IEnumerator Reload()
    {
        DeactivateIsTriggerForRotationObjects();
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = "SELAMAT DATANG\nTekan R / O untuk Memulai";
        welcomeText.alignment = TextAnchor.MiddleCenter;
        welcomeText.fontSize = 200;
        DeactivateAllSelectionManager();
        yield return new WaitForSeconds(0.2f);
        TurunkanSemuuaHuruf();
        yield return new WaitForSeconds(0.2f);
        InteractLayarG3.enabled = false;
        DeactivateAllInteractLayarP2G3();
        yield return new WaitForSeconds(0.2f);

        TimerGameResultG3.enabled = false;
        //InteractStartP2Game2[0].enabled = false;
        //InteractStartP2Game2[1].enabled = false;
        //InteractStartP2Game2[2].enabled = false;
        //DeactivateAllGrabObjectP2G2();
        ObjectPlayG3.MAtikanSemuaKamera();
        yield return new WaitForSeconds(0.5f);
        ObjectPlayG3.PlayGame2();
        yield return new WaitForSeconds(1.0f);
        ObjectPlayG3.CekposisiKamera();
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = "KALIAN HARUS MENEMUKAN JAWABAN DARI PERTAYAAN YANG AKAN DIBERIKAN";
        welcomeText.text = "KALIAN HARUS MENARUH DAN MENYAMAKAN KARTU DI ATAS MEJA DENGAN YANG ADA DI LANTAI";
        welcomeText.alignment = TextAnchor.UpperLeft;
        welcomeText.fontSize = 190;
        yield return new WaitForSeconds(5.0f);
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = "JAWABAN BERUPA DERET OBJEK HURUF YANG DIRANGKAI MENJADI KATA";
        welcomeText.alignment = TextAnchor.UpperLeft;
        welcomeText.fontSize = 190;
        yield return new WaitForSeconds(5.0f);
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = "RANGKAIAN KATA JAWABAN HANYA MEMPOLA VERTIKAL MAUPUN HORIZONTAL";
        welcomeText.alignment = TextAnchor.UpperLeft;
        welcomeText.fontSize = 190;
        yield return new WaitForSeconds(5.0f);
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = "ARAHKAN PANDANGAN KALIAN KE OBJECT HURUF UNTUK MERANGKAI KATA";
        welcomeText.alignment = TextAnchor.UpperLeft;
        welcomeText.fontSize = 190;
        yield return new WaitForSeconds(5.0f);
        //Verifjawaban.SetActive(true);
        QuestionIndexEliminationG3.SelectRandomQuestion();
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = "KALIAN AKAN DIKASIH WAKTU 5 MENIT";
        welcomeText.alignment = TextAnchor.MiddleCenter;
        welcomeText.fontSize = 190;
        yield return new WaitForSeconds(3.0f);
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = "ATAU JIKA SEMUA JAWABAN DITEMUKAN";
        welcomeText.alignment = TextAnchor.MiddleCenter;
        welcomeText.fontSize = 190;
        yield return new WaitForSeconds(3.0f);
        GeneratorGame3.GenerateWords();
        yield return new WaitForSeconds(1.1f);
        GeneratorGame3.PopulateAvailableRendererNames();
        yield return new WaitForSeconds(1.1f);
        GeneratorGame3.UpdateWordPlacementData();
        yield return new WaitForSeconds(1.1f);
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = QuestionIndexEliminationG3.selectedQuestion; // Ambil dari script lain
        welcomeText.alignment = TextAnchor.MiddleCenter;
        welcomeText.fontSize = 190;
        yield return new WaitForSeconds(0.5f);
        ActivateAllInteractStartG3();
        yield return new WaitForSeconds(0.5f);
        TimerGameResultG3.enabled = true;
        TimerGameResultG3.totalTime = 300;
        yield return new WaitForSeconds(300.0f);
        DeactivateAllInteractStartG3();
        EnableIsTriggerForRotationObjects();
        yield return new WaitForSeconds(0.5f);
        TurunkanSemuuaHuruf();
        a = GeneratorGame3.p1 * 10;
        b = GeneratorGame3.p2 * 10;
        c = GeneratorGame3.p3 * 10;
        d = GeneratorGame3.p4 * 10;
        yield return new WaitForSeconds(1.1f);
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = $"player 1 mendapat poin = {a} poin\n" +
                       $"player 2 mendapat poin = {b} poin\n" +
                       $"player 3 mendapat poin = {c} poin\n" +
                       $"player 4 mendapat poin = {d} poin\n";
        welcomeText.alignment = TextAnchor.UpperLeft;
        welcomeText.fontSize = 180;
        Datagame3.DestroyAllGridBlocks();
        yield return new WaitForSeconds(10.1f);
        GeneratorGame3.clear();
        SelectionManagerG3.CleanUpMissingObjects();
        welcomeText.text = string.Empty;
        yield return new WaitForSeconds(0.1f);
        Datagame3.RestoreMissingBlocks();
        yield return new WaitForSeconds(1.1f);
        SetWelcomeMessage();
        Datagame3.InitializeGrid();
        yield return new WaitForSeconds(2.1f);
        SelectionManagerG3.AddChildInteractableObjects();


    }
    private IEnumerator Force()
    {
        EnableIsTriggerForRotationObjects();
        Debug.Log("PAKSA BERHENTI DI STARTGAME3");
        welcomeText.gameObject.SetActive (true);
        welcomeText.text = string.Empty;
        yield return new WaitForSeconds(0.5f);
        TimerGameResultG3.totalTime = 0;
        DeactivateAllInteractStartG3();
        yield return new WaitForSeconds(0.5f);
        e = 0;
        Datagame3.EnableAllGridBlocks();
        Datagame3.SetAllBlocksToMaterialIndexB();
        TimerGameResultG3.enabled = false;
        TimerGameResultG3.timerText.text = string.Empty;
        yield return new WaitForSeconds(1.1f);
        TurunkanSemuuaHuruf();
        a = GeneratorGame3.p1 * 10;
        b = GeneratorGame3.p2 * 10;
        c = GeneratorGame3.p3 * 10;
        d = GeneratorGame3.p4 * 10;
        yield return new WaitForSeconds(1.1f);
        welcomeText.text = string.Empty;
        welcomeText.gameObject.SetActive(true);
        welcomeText.text = $"player 1 mendapat poin = {a} poin\n" +
                       $"player 2 mendapat poin = {b} poin\n" +
                       $"player 3 mendapat poin = {c} poin\n" +
                       $"player 4 mendapat poin = {d} poin\n";
        welcomeText.alignment = TextAnchor.UpperLeft;
        welcomeText.fontSize = 180;
        yield return new WaitForSeconds(10.1f);
        GeneratorGame3.clear();
        welcomeText.text = string.Empty;
        yield return new WaitForSeconds(0.1f);
        SetWelcomeMessage();

    }

    // Update is called once per frame
    void Update()
    {
        e = GeneratorGame3.A;
        //Debug.Log("E = 1");
        if (e == 1)
        {
            Debug.Log("E = 4");
            GeneratorGame3.A = 4;
            MatikanPaksa();
            Debug.Log("E = 4");
        }
    }
}
