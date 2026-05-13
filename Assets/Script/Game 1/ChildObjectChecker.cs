using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildObjectChecker : MonoBehaviour
{
    [Header("Pusat Data Anak")]
    [Tooltip("GameObject yang berisi anak-anak yang menjadi referensi untuk pengecekan.")]
    public GameObject referenceParent;

    [Header("GameObject yang Akan Diperiksa")]
    [Tooltip("Daftar GameObject yang akan dicek kelengkapan anaknya.")]
    public List<GameObject> gameObjectsToCheck;

    private Dictionary<string, Transform> referenceChildren = new Dictionary<string, Transform>();

    private void Start()
    {
        if (referenceParent == null)
        {
            Debug.LogError("Reference Parent belum diatur! Harap masukkan GameObject referensi di Inspector.");
            return;
        }

        // Simpan semua anak dari referenceParent ke dalam Dictionary
        foreach (Transform child in referenceParent.transform)
        {
            if (!referenceChildren.ContainsKey(child.name))
            {
                referenceChildren.Add(child.name, child);
            }
        }

        // Mulai pengecekan anak-anak yang hilang
        CheckAndFixChildren();
    }

    public void CheckAndFixChildren()
    {
        if (referenceParent == null)
        {
            Debug.LogError("Reference Parent belum diatur! Harap masukkan GameObject referensi di Inspector.");
            return;
        }

        // Simpan semua anak dari referenceParent ke dalam Dictionary
        foreach (Transform child in referenceParent.transform)
        {
            if (!referenceChildren.ContainsKey(child.name))
            {
                referenceChildren.Add(child.name, child);
            }
        }
        foreach (GameObject parent in gameObjectsToCheck)
        {
            if (parent == null)
            {
                Debug.LogError("Salah satu GameObject dalam daftar kosong!");
                continue;
            }

            // Ambil daftar anak-anak dari GameObject yang diperiksa
            List<string> existingChildNames = new List<string>();
            foreach (Transform child in parent.transform)
            {
                existingChildNames.Add(child.name);
            }

            // Bandingkan dengan referenceParent
            foreach (KeyValuePair<string, Transform> refChild in referenceChildren)
            {
                if (!existingChildNames.Contains(refChild.Key))
                {
                    Debug.Log($"Anak '{refChild.Key}' hilang di '{parent.name}', menambahkan...");
                    AddMissingChild(parent, refChild.Value);
                }
            }
        }
    }

    private void AddMissingChild(GameObject parent, Transform referenceChild)
    {
        // Buat salinan dari anak referensi
        GameObject newChild = Instantiate(referenceChild.gameObject, parent.transform);
        newChild.name = referenceChild.name; // Pastikan nama tetap sama
        Debug.Log($"Menambahkan '{newChild.name}' ke '{parent.name}'");
    }
}
