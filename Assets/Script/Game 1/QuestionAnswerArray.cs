using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Untuk UI Text

[System.Serializable]
public class QuestionSet
{
    [Header("Jawaban (GameObject)")]
    public GameObject answer;

    [Header("Pertanyaan (GameObject UI Text)")]
    public List<GameObject> questions = new List<GameObject>();
}

public class QuestionAnswerArray : MonoBehaviour
{
    [Header("Daftar Jawaban dan Pertanyaan")]
    public List<QuestionSet> questionSets = new List<QuestionSet>();

    private void Start()
    {
        PrintAllData();
    }

    // Fungsi untuk menambahkan jawaban
    public void SetAnswer(int index, GameObject answerObject)
    {
        if (index >= 0 && index < questionSets.Count)
        {
            questionSets[index].answer = answerObject;
            //Debug.Log($"Jawaban di index {index} berhasil ditambahkan: {answerObject.name}");
        }
        else
        {
            Debug.LogError("Indeks tidak valid.");
        }
    }

    // Fungsi untuk menambahkan pertanyaan
    public void AddQuestion(int index, GameObject questionObject)
    {
        if (index >= 0 && index < questionSets.Count)
        {
            questionSets[index].questions.Add(questionObject);
            //Debug.Log($"Pertanyaan berhasil ditambahkan ke index {index}: {questionObject.name}");
        }
        else
        {
            Debug.LogError("Indeks tidak valid.");
        }
    }

    // Debug fungsi untuk menampilkan semua jawaban dan isi teks pertanyaan
    public void PrintAllData()
    {
        for (int i = 0; i < questionSets.Count; i++)
        {
            //Debug.Log($"Jawaban {i + 1}: {questionSets[i].answer?.name ?? "Kosong"}");

            for (int j = 0; j < questionSets[i].questions.Count; j++)
            {
                GameObject questionObject = questionSets[i].questions[j];

                if (questionObject != null)
                {
                    // Coba ambil teks dari komponen UI Text
                    Text uiText = questionObject.GetComponent<Text>();
                    if (uiText != null)
                    {
                        //Debug.Log($"   Pertanyaan {j + 1}: {uiText.text}");
                        continue;
                    }

                    
                }
                else
                {
                    Debug.Log($"   Pertanyaan {j + 1}: Kosong");
                }
            }
        }
    }
}
