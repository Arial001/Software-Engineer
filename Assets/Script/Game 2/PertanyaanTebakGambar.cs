using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class PertanyaanTebakGambar : MonoBehaviour
{

    public List<GameObject> card = new List<GameObject>();    // List asli, tidak diacak
    public List<string> card2 = new List<string>();           // List nama hasil acak
    [Header("Referensi DisplayPertanyaanGambar")]
    public DisplayPertanyaanGambar DisplayPertanyaanGambar;
    [Header("Referensi VerifControlG2")]
    public VerifControlG2 VerifControlG2;

    void Start()
    {
        //Debug.Log("Start");
        //ShuffleCardNamesToCard2();    // Pertama kali jalan, card2 diacak dari card
        //PrintCard2();                 // Tampilkan hasil acak di console (opsional)
        //VerifControlG2.enabled = true;
        //VerifControlG2.ActivateAllParentObjectReaders();
        ////DisplayPertanyaanGambar.ResetScript();
    }

    void Update()
    {
        //Tdown();
    }

    public void Tdown()
    {
        Debug.Log("huruf t ditekan");
        ShuffleCardNamesToCard2();  // Setiap tekan T, card2 diacak ulang dari card
        PrintCard2();               // Tampilkan hasil acak di console (opsional)
                                    //DisplayPertanyaanGambar.Initialize();
        DisplayPertanyaanGambar.ResetScript();
        VerifControlG2.enabled = true;
        VerifControlG2.ActivateAllParentObjectReaders();
        //if (Input.GetKeyDown(KeyCode.T))
        //{
            
        //}
    }

    void ShuffleCardNamesToCard2()
    {
        Debug.Log("pengacakan");
        // Buat list sementara untuk mengacak GameObject
        List<GameObject> tempList = new List<GameObject>(card);

        // Fisher-Yates Shuffle pada tempList
        for (int i = tempList.Count - 1; i > 0; i--)
        {
            Debug.Log("pengacakan dimulai");
            int j = Random.Range(0, i + 1);
            GameObject temp = tempList[i];
            tempList[i] = tempList[j];
            tempList[j] = temp;
            Debug.Log("pengacakan selesai)");
        }

        // Kosongkan card2 lalu isi dengan nama GameObject dari tempList
        card2.Clear();
        foreach (GameObject obj in tempList)
        {
            card2.Add(obj.name);
        }
    }

    void PrintCard2()
    {
        Debug.Log("Urutan nama di card2 setelah diacak:");
        for (int i = 0; i < card2.Count; i++)
        {
            Debug.Log("Index " + i + ": " + card2[i]);
        }
    }
    public List<string> GetAllRandomizedAnswers()
    {
        return card2;
    }
}
