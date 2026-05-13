using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParentObjectNameReaderG2 : MonoBehaviour
{
    [Header("Referensi PertanyaanTebakGambar")]
    public PertanyaanTebakGambar PertanyaanTebakGambar;
    [Header("Referensi RotationObject")]
    public RotationObject RotationObject;
    public GameObject parentObject;
    public string verif;
    public int verifplayer = 0;
    public int IndexPertanyaan = 0;
    public List<string> pertanyaan = new List<string>();
    public List<string> childNames = new List<string>();
    public int jawabanbenarP1;
    public int jawabanbenarP2;
    public int jawabanbenarP3;
    public int jawabanbenarP4;
    public event Action OnJawabanBenar;
    private bool jawabanBenarDipicu = false;
    private int childCount;
    void Start()
    {
        //PertanyaanTebakGambar.OnResetDisplay += ResetScript;
        InitializeVerification();
        Debug.Log("Nilai verif: " + verif);

        //ProcessChildren();

    }

    public void InitializeVerification()
    {
        if (PertanyaanTebakGambar == null)
        {
            Debug.LogError("ParentObjectNameReaderG2: 'PertanyaanTebakGambar' belum di-assign di Inspector! Mohon seret GameObject yang benar.", this);
            verif = ""; // Pastikan verif tidak null
            return;
        }
        pertanyaan = PertanyaanTebakGambar.GetAllRandomizedAnswers();

        if (pertanyaan == null || pertanyaan.Count == 0)
        {
            Debug.LogWarning("ParentObjectNameReaderG2: List hasil randomisasi dari 'PertanyaanTebakGambar' kosong atau null. Tidak dapat mengatur 'verif'.", this);
            verif = ""; // Pastikan verif tidak null
            return;
        }

        // Pastikan IndexPertanyaan berada dalam batas yang valid
        if (IndexPertanyaan >= 0 && IndexPertanyaan < pertanyaan.Count)
        {
            verif = pertanyaan[IndexPertanyaan];
            Debug.Log($"ParentObjectNameReaderG2: Nilai 'verif' disetel ke: '{verif}' (dari indeks {IndexPertanyaan}).", this);
        }
        else
        {
            Debug.LogWarning($"ParentObjectNameReaderG2: 'IndexPertanyaan' ({IndexPertanyaan}) di luar batas list randomisasi (ukuran: {pertanyaan.Count}). Menggunakan indeks 0 sebagai fallback.", this);
            verif = pertanyaan[0]; // Fallback ke indeks 0
        }
    }

    public void ProcessChildren()
    {
        if (!enabled)
        {
            Debug.Log("ProcessChildren() dipanggil, tetapi skrip ini dinonaktifkan. Tidak melakukan apa-apa.");
            return; // Keluar dari fungsi jika skrip tidak aktif
        }
        else
        {
            //RotationObject.ApplyTargetTransformSmoothly();
            childCount = parentObject.transform.childCount;
            if (childCount == 0)
            {
                StartCoroutine(PutartanpabarangdiTangan());
            }
            if (childCount > 0)
            {
                //ebug.Log("Parent Object  memiliki child!");
                //childNames.Clear();
                if (parentObject == null)
                {
                    Debug.Log("Parent Object belum di-assign!");
                    return;
                }
                if (childCount <= 0)
                {
                    
                    Debug.Log("Parent Object tidak memiliki child!");
                    //ProcessChildren();
                    Debug.Log("processchildren rekursif");
                    return;
                }
                foreach (Transform child in parentObject.transform)
                {
                    //Debug.Log("Membaca Transform child in parentObject.transform ");
                    //if (parentObject.transform.childCount > 0)
                    int result = String.Compare(child.name, verif);
                    //Debug.Log($"Membaca result: {result}");
                    //Debug.Log($"Membaca anak: {child.name}");

                    if (result == 1)
                    {
                        StartCoroutine(PutartanpabarangdiTangan());
                    }
                    if (result == 0)
                    {
                        if(verifplayer == 1)
                        {
                            RotationObject.ApplyTargetTransformSmoothly();
                            DisableHierarchy(child);
                            Debug.Log($"Menonaktifkan hierarki objek: {child.name} karena cocok dengan verif");
                            childNames.Add(child.name);
                            Destroy(child.gameObject);
                            result = 1;
                            verif = string.Empty;
                            jawabanbenarP1++;
                            verifplayer = 0;
                            enabled = false;
                            Debug.Log($"JawabanbenarP1: 'jawabanbenarP1' ({jawabanbenarP1}).", this);
                        }
                        if (verifplayer == 2)
                        {
                            RotationObject.ApplyTargetTransformSmoothly();
                            DisableHierarchy(child);
                            Debug.Log($"Menonaktifkan hierarki objek: {child.name} karena cocok dengan verif");
                            childNames.Add(child.name);
                            Destroy(child.gameObject);
                            result = 1;
                            verif = string.Empty;
                            jawabanbenarP2++;
                            verifplayer = 0;
                            enabled = false;
                            Debug.Log($"JawabanbenarP2: 'jawabanbenarP2' ({jawabanbenarP2}).", this);
                        }
                        if (verifplayer == 3)
                        {
                            RotationObject.ApplyTargetTransformSmoothly();
                            DisableHierarchy(child);
                            Debug.Log($"Menonaktifkan hierarki objek: {child.name} karena cocok dengan verif");
                            childNames.Add(child.name);
                            Destroy(child.gameObject);
                            result = 1;
                            verif = string.Empty;
                            jawabanbenarP3++;
                            verifplayer = 0;
                            enabled = false;
                            Debug.Log($"JawabanbenarP3: 'jawabanbenarP3' ({jawabanbenarP3}).", this);
                        }
                        if (verifplayer == 4)
                        {
                            RotationObject.ApplyTargetTransformSmoothly();
                            DisableHierarchy(child);
                            Debug.Log($"Menonaktifkan hierarki objek: {child.name} karena cocok dengan verif");
                            childNames.Add(child.name);
                            Destroy(child.gameObject);
                            result = 1;
                            verif = string.Empty;
                            jawabanbenarP4++;
                            verifplayer = 0;
                            enabled = false;
                            Debug.Log($"JawabanbenarP4: 'jawabanbenarP4' ({jawabanbenarP4}).", this);
                        }

                    }
                    
                    

                }
                return;
            }
            //RotationObject.ApplyTargetTransformSmoothly2();
        }
    }

    void DisableHierarchy(Transform target)
    {

        //if (jawabanBenarDipicu) return;
        // Menonaktifkan semua anak dan cucu secara rekursif
        foreach (Transform child in target)
        {
            //OnJawabanBenar?.Invoke();
            DisableHierarchy(child);
        }
        // Menonaktifkan objek target (parent)
        //jawabanBenarDipicu = true;
        target.gameObject.SetActive(false);
        //OnJawabanBenar?.Invoke();
        //Debug.Log($"Menonaktifkan parent: {target.name}");
        //jawabanbenar = 1;
        //Debug.Log("JAWABAN BENAR = 1");
        //jawabanbenar = 0;
        //StartCoroutine(ResetJawabanBenar());
    }
    /*IEnumerator ResetJawabanBenar()
    {
        yield return null; // Tunggu satu frame agar script lain bisa membaca nilai
        jawabanbenar = 0;
    }*/
    /*void OnTransformChildrenChanged()
    {
        jawabanBenarDipicu = false;
        ProcessChildren();
    }*/    
    public List<string> GetChildNames()
    {
        return childNames;
    }
    public void ResetScript()
    {
        InitializeVerification();
        Debug.Log("Nilai verif: " + verif);

        //ProcessChildren();


    }
    private void OnDestroy()
    {
        //PertanyaanTebakGambar.OnResetDisplay -= ResetScript; // Unsubscribe to prevent memory leaks
    }
    public void DestroyAll()
    {
        foreach (Transform child in parentObject.transform)
        {
            //Debug.Log("Membaca Transform child in parentObject.transform ");
            //if (parentObject.transform.childCount > 0)
            int result = String.Compare(child.name, verif);
            //Debug.Log($"Membaca result: {result}");
            //Debug.Log($"Membaca anak: {child.name}");
            Destroy(child.gameObject);


        }
    }
    private IEnumerator PutartanpabarangdiTangan()
    {
        RotationObject.ApplyTargetTransformSmoothly();
        yield return new WaitForSeconds(3.0f);
        RotationObject.ApplyTargetTransformSmoothly2();
    }
}
