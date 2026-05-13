using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEditor;



public class ParentObjectNameReader : MonoBehaviour
{
    [Header("Referensi FinalQuestionIndexElimination")]
    public FinalQuestionIndexElimination finalQuestionIndexElimination;
    [Header("Referensi QuestionIndexElimination")]
    public QuestionIndexElimination QuestionIndexElimination;
    public GameObject parentObject;
    private string verif;
    private string Send;
    public List<string> childNames = new List<string>();
    public int jawabanbenar;
    public event Action OnJawabanBenar;
    private bool jawabanBenarDipicu = false;
    private int childCount;
    void Start()
    {
        finalQuestionIndexElimination.OnResetDisplay += ResetScript;
        verif = finalQuestionIndexElimination.GetRandomizedAnswer();
        Debug.Log("Nilai verif: " + verif);
        
        ProcessChildren();
        
    }


    public void ProcessChildren()
    {
        childCount = parentObject.transform.childCount;
        if (childCount > 0)
        {
            //ebug.Log("Parent Object  memiliki child!");
            Send = null;
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

                if (result == 0)
                {
                    Send = verif;
                    DisableHierarchy(child);
                    Debug.Log($"Menonaktifkan hierarki objek: {child.name} karena cocok dengan verif");
                    childNames.Add(child.name);
                    QuestionIndexElimination.ResetScript();
                    Destroy(child.gameObject);
                    jawabanbenar++;
                    result = 1;
                }


            }
            
            return;
        }
        
        
    }

    void DisableHierarchy(Transform target)
    {

        //if (jawabanBenarDipicu) return;
        // Menonaktifkan semua anak dan cucu secara rekursif
        foreach (Transform child in target)
        {
            OnJawabanBenar?.Invoke();
            DisableHierarchy(child);
        }
        // Menonaktifkan objek target (parent)
        //jawabanBenarDipicu = true;
        target.gameObject.SetActive(false);
        OnJawabanBenar?.Invoke();
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
    public int GetNumberAnswers()
    {
        return jawabanbenar;
    }
    public List<string> GetChildNames()
    {
        return childNames;
    }
    public void ResetScript()
    {
        verif = finalQuestionIndexElimination.GetRandomizedAnswer();
        Debug.Log("Nilai verif: " + verif);
        ProcessChildren();
        
        
    }
    private void OnDestroy()
    {
        finalQuestionIndexElimination.OnResetDisplay -= ResetScript; // Unsubscribe to prevent memory leaks
    }
    public string GetRandomizedAnswer()
    {
        return Send;
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
}
