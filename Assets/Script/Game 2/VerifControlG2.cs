using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifControlG2 : MonoBehaviour
{
    // Mengubah tipe list dari GameObject menjadi ParentObjectNameReaderG2
    [Header("Referensi Script ParentObjectNameReaderG2")]
    public List<ParentObjectNameReaderG2> ParentObjectReaders = new List<ParentObjectNameReaderG2>();

    void Start()
    {
        // Debugging untuk memastikan list tidak kosong
       /* if (ParentObjectReaders.Count == 0)
        {
            Debug.LogWarning("VerifControlG2: List 'ParentObjectReaders' kosong. Pastikan Anda menyeret script ke slot di Inspector.", this);
            return;
        }*/

        // --- Mengaktifkan SEMUA instance script ParentObjectNameReaderG2 dalam list ---
        //ActivateAllParentObjectReaders();

       
        // if (ParentObjectReaders.Count > 0 && ParentObjectReaders[0] != null)
        // {
        //     ParentObjectReaders[0].enabled = true;
        //     Debug.Log($"VerifControlG2: Script '{ParentObjectReaders[0].name}' diaktifkan.", this);
        // }
    }

    // Fungsi untuk mengaktifkan semua script ParentObjectNameReaderG2 dalam list
    public void ActivateAllParentObjectReaders()
    {
        foreach (ParentObjectNameReaderG2 readerScript in ParentObjectReaders)
        {
            if (readerScript != null)
            {
                readerScript.enabled = true;
                readerScript.ResetScript();
                Debug.Log($"VerifControlG2: Script '{readerScript.name}' pada GameObject '{readerScript.gameObject.name}' diaktifkan.", this);
                // Panggil juga Initialize jika script ParentObjectNameReaderG2 perlu inisialisasi ulang
                // readerScript.InitializeVerification();
                // readerScript.ProcessChildren();
            }
        }
    }

    // Fungsi untuk menonaktifkan semua script ParentObjectNameReaderG2 dalam list
    public void DeactivateAllParentObjectReaders()
    {
        foreach (ParentObjectNameReaderG2 readerScript in ParentObjectReaders)
        {
            if (readerScript != null)
            {
                readerScript.enabled = false;
                Debug.Log($"VerifControlG2: Script '{readerScript.name}' pada GameObject '{readerScript.gameObject.name}' dinonaktifkan.", this);
            }
        }
    }
}
