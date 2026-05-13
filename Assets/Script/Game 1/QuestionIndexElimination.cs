using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class QuestionIndexElimination : MonoBehaviour
{
    [Header("Referensi QuestionAnswerArray")]
    public QuestionAnswerArray questionAnswerArray;
    [Header("Referensi QuestionIndexEliminationtwice")]
    public QuestionIndexEliminationtwice QuestionIndexEliminationtwice;
    [Header("Referensi respawnGameobjectAnswer")]
    public respawnGameobjectAnswer respawnGameobjectAnswer;
    [Header("Referensi ParentObjectNameReader")]
    public ParentObjectNameReader ParentObjectNameReader;
    [Header("Referensi FinalQuestionIndexElimination")]
    public FinalQuestionIndexElimination finalQuestionIndexElimination;

    public int reset;
    private string Sudahdipakai;
    public int a;
    private List<string> usedAnswers = new List<string>(); // Menyimpan semua jawaban yang sudah digunakan
    public List<string> selectedAnswers = new List<string>();
    public List<string> combinedAnswersAndQuestions = new List<string>();
    public event Action OnStartTwice;
    private void Start()
    {
        ParentObjectNameReader.OnJawabanBenar += ResetScript;
        Initialize();
    }

    

    private void Initialize()
    {
        
        // Ambil jawaban yang sudah dirandomisasi dari FinalQuestionIndexElimination
        Sudahdipakai = ParentObjectNameReader.GetRandomizedAnswer();
        AddToUsedAnswers(Sudahdipakai); // Tambahkan jawaban ke daftar usedAnswers
        //Debug.Log($"Jawaban baru dipakai: {Sudahdipakai}");
        

        
        //respawnGameobjectAnswer.enabled = false;
        QuestionIndexEliminationtwice.enabled = false;
        ParentObjectNameReader.enabled = false;

        if (questionAnswerArray == null)
        {
            Debug.LogError("QuestionAnswerArray belum diatur!");
            return;
        }

        Dictionary<int, int> jumlahPertanyaanPerJawaban = GetJumlahPertanyaanPerJawaban();
        List<int> selectedJawabanIndexes = RandomizeJawabanIndexes(jumlahPertanyaanPerJawaban.Keys, 6);
        PrintAllQuestionsFromSelectedIndexes(selectedJawabanIndexes);
        //a = 0;
    }

    public Dictionary<int, int> GetJumlahPertanyaanPerJawaban()
    {
        Dictionary<int, int> jumlahPertanyaanPerJawaban = new Dictionary<int, int>();

        for (int i = 0; i < questionAnswerArray.questionSets.Count; i++)
        {
            int jumlahPertanyaan = questionAnswerArray.questionSets[i].questions.Count;
            jumlahPertanyaanPerJawaban.Add(i, jumlahPertanyaan);
        }

        return jumlahPertanyaanPerJawaban;
    }

    public List<int> RandomizeJawabanIndexes(ICollection<int> jawabanIndexes, int count)
    {
        List<int> availableIndexes = new List<int>(jawabanIndexes);
        List<int> selectedIndexes = new List<int>();

        // Pastikan jumlah maksimum iterasi random sesuai dengan jumlah index yang tersedia
        int maxIterations = availableIndexes.Count * 10; // Cegah infinite loop
        int iterations = 0;

        while (selectedIndexes.Count < count && iterations < maxIterations)
        {
            // Ambil index random dari daftar yang tersedia
            int randomIndex = Random.Range(0, availableIndexes.Count);
            int selectedJawabanIndex = availableIndexes[randomIndex];
            string selectedJawabanName = questionAnswerArray.questionSets[selectedJawabanIndex].answer?.name;

            // Jika jawaban tidak ada di usedAnswers atau jika usedAnswers kosong
            if (usedAnswers.Count == 0 || !usedAnswers.Contains(selectedJawabanName))
            {
                selectedIndexes.Add(selectedJawabanIndex);
                availableIndexes.RemoveAt(randomIndex); // Hapus dari daftar yang tersedia
            }
            else
            {
                // Jika cocok dengan usedAnswers, lakukan random ulang untuk elemen ini
                iterations++;
            }
        }

        // Log jika randomisasi mencapai batas iterasi
        if (iterations >= maxIterations)
        {
            Debug.LogWarning("Randomisasi mencapai batas maksimum iterasi.");
        }

        return selectedIndexes;
    }

    public void PrintAllQuestionsFromSelectedIndexes(List<int> selectedJawabanIndexes)
    {
        foreach (int jawabanIndex in selectedJawabanIndexes)
        {
            string jawabanName = questionAnswerArray.questionSets[jawabanIndex].answer?.name ?? "Kosong";
            selectedAnswers.Add(jawabanName);
            Debug.Log($"- Jawaban: {jawabanName}");
            combinedAnswersAndQuestions.Add(jawabanName);

            for (int i = 0; i < questionAnswerArray.questionSets[jawabanIndex].questions.Count; i++)
            {
                GameObject questionObject = questionAnswerArray.questionSets[jawabanIndex].questions[i];
                if (questionObject != null)
                {
                    string pertanyaanText = questionObject.GetComponent<UnityEngine.UI.Text>()?.text ?? "Kosong";
                    //Debug.Log($"   Pertanyaan {i + 1}: {pertanyaanText}");
                    combinedAnswersAndQuestions.Add(pertanyaanText);
                }
            }
        }
        ParentObjectNameReader.enabled = true;
        QuestionIndexEliminationtwice.enabled = true;
        respawnGameobjectAnswer.enabled = true;
    }

    public void ResetScript()
    {
        //a = 1;
        //Debug.Log($"nilai A = {a}");
        //Sudahdipakai = PertanyaanTebakGambar.GetRandomizedAnswer();
        respawnGameobjectAnswer.enabled = false;
        QuestionIndexEliminationtwice.enabled = false;
        selectedAnswers.Clear();
        //Debug.Log("selected index clear");
        combinedAnswersAndQuestions.Clear();
        //Debug.Log("combinedAnswersAndQuestions clear.");
        Initialize();
        OnStartTwice?.Invoke();
        respawnGameobjectAnswer.ResetScript();
        Debug.Log("Script QuestionIndexElimination berhasil di-reset.");
        //Debug.Log($"nilai reset = {reset}");
        //Debug.Log("Initialize.");
        //StartCoroutine(ResetA());
        //Debug.Log($"nilai a = {a}");
        //reset = 0;
    }

    private void OnDestroy()
    {
        ParentObjectNameReader.OnJawabanBenar -= ResetScript; // Unsubscribe to prevent memory leaks
    }
    IEnumerator ResetA()
    {
        yield return null; // Tunggu satu frame agar script lain bisa membaca nilai
        a = 0;
    }

    public List<string> GetAllRandomizedAnswers()
    {
        return selectedAnswers;
    }

    public List<string> GetCombinedAnswersAndQuestions()
    {
        return combinedAnswersAndQuestions;
    }

    public int GetReset()
    {
        return a;
    }
    public void AddToUsedAnswers(string newAnswer)
    {
        // Tambahkan jawaban baru ke usedAnswers pada indeks berikutnya
        if (usedAnswers.Count <= 22) // Misalnya, jika maksimal ada 6 jawaban
        {
            usedAnswers.Insert(usedAnswers.Count, newAnswer); // Menambahkan di akhir
            Debug.Log($"Jawaban baru ditambahkan ke usedAnswers: {newAnswer}");
            Debug.Log($"Jumlah jawaban yang terpakai: {usedAnswers.Count}");

            // Membaca isi setiap index di usedAnswers
            for (int i = 0; i < usedAnswers.Count; i++)
            {
                Debug.Log($"Index {i}: {usedAnswers[i]}");
            }
        }
        else
        {
            Debug.LogWarning("Maksimal jawaban sudah tercapai.");
        }
    }
}
