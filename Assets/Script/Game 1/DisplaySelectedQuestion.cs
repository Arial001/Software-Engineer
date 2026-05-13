using UnityEngine;
using UnityEngine.UI;

public class DisplaySelectedQuestion : MonoBehaviour
{
    [Header("Referensi FinalQuestionIndexElimination")]
    public FinalQuestionIndexElimination finalQuestionIndexElimination;

    [Header("UI Text Target")]
    private int Reset;
    public GameObject interaction_Info_UI;
    private Text interaction_text;

    private void Start()
    {
        finalQuestionIndexElimination.OnResetDisplay += ResetScript;
        Initialize();
    }

   /* private void Update()
    {
        // Cek apakah Reset bernilai 1
        if (Reset == 1)
        {
            Debug.Log("Reset terdeteksi. Mereset DisplaySelectedQuestion.");
            ResetScript();
            
            Reset = 0;
        }
    }*/

    private void Initialize()
    {
        Reset = 0;
        //Reset = PertanyaanTebakGambar.GetReset();
        interaction_Info_UI.gameObject.SetActive(false);

        if (finalQuestionIndexElimination == null)
        {
            Debug.LogError("FinalQuestionIndexElimination belum diatur!");
            return;
        }

        if (interaction_Info_UI == null)
        {
            Debug.LogWarning("UI Text Target belum diatur!");
            return;
        }

        interaction_text = interaction_Info_UI.GetComponent<Text>();

        if (interaction_text == null)
        {
            Debug.LogWarning("Komponen Text tidak ditemukan pada UI Text Target!");
            return;
        }

        DisplayRandomizedQuestion();
    }

    private void DisplayRandomizedQuestion()
    {
        // Mengambil pertanyaan yang telah dirandomisasi dari FinalQuestionIndexElimination
        string randomizedQuestion = finalQuestionIndexElimination.GetRandomizedQuestion();

        if (!string.IsNullOrEmpty(randomizedQuestion))
        {
            interaction_text.text = string.Empty;
            interaction_Info_UI.gameObject.SetActive(true); // Menampilkan UI
            interaction_text.text = randomizedQuestion;
            //Debug.Log($"Pertanyaan yang ditampilkan: {randomizedQuestion}");
        }
        else
        {
            interaction_Info_UI.gameObject.SetActive(false);
            Debug.LogWarning("Tidak ada pertanyaan yang dirandomisasi dari FinalQuestionIndexElimination.");
        }
    }

    public void ResetScript()
    {
        interaction_text.text = string.Empty; // Mengosongkan teks

        interaction_Info_UI?.SetActive(false); // Menyembunyikan UI
        Initialize();
        Debug.Log("Script DisplaySelectedQuestion berhasil di-reset.");
    }
    private void OnDestroy()
    {
        finalQuestionIndexElimination.OnResetDisplay -= ResetScript; // Unsubscribe to prevent memory leaks
    }


}
