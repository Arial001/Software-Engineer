using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject interaction_Info_UI;
    private Text interaction_text;

    [Header("Interactable Objects")]
    public List<InteractableObject> interactableObjects;

    [Header("Raycast Reference")]
    [SerializeField] private RaycastVisualizer raycastVisualizer;

    private void Start()
    {
        // Ambil komponen Text dari UI
        interaction_text = interaction_Info_UI.GetComponent<Text>();
        interaction_Info_UI.SetActive(false);

        // Validasi RaycastVisualizer
        if (raycastVisualizer == null)
        {
            //Debug.LogError("RaycastVisualizer belum diatur di SelectionManager!");
            enabled = false;
            return;
        }

        // Panggil fungsi untuk menambahkan anak-anak yang memiliki InteractableObject
        AddChildInteractableObjects();
    }

    void Update()
    {
        if (raycastVisualizer == null) return;

        // Menggunakan data dari RaycastVisualizer
        if (raycastVisualizer.HasHit)
        {
            Transform selectionTransform = raycastVisualizer.CurrentHit.transform;

            // Cek apakah objek yang kena ada dalam daftar interactableObjects
            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();
            if (interactable != null && interactableObjects.Contains(interactable))
            {
                // Tampilkan nama objek di UI
                interaction_text.text = interactable.GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else
            {
                // Sembunyikan UI jika objek tidak valid
                interaction_Info_UI.SetActive(false);
            }
        }
        else
        {
            // Sembunyikan UI jika raycast tidak mengenai apa pun
            interaction_Info_UI.SetActive(false);
        }
    }

    private void AddChildInteractableObjects()
    {
        List<InteractableObject> newInteractables = new List<InteractableObject>();

        foreach (InteractableObject interactable in interactableObjects)
        {
            // Tambahkan anak-anak dan cucu-cucu yang memiliki InteractableObject ke dalam daftar sementara
            AddChildInteractables(interactable.transform, newInteractables);
        }

        // Setelah iterasi selesai, tambahkan semua anak baru ke dalam daftar utama
        interactableObjects.AddRange(newInteractables);
    }

    private void AddChildInteractables(Transform parent, List<InteractableObject> newInteractables)
    {
        foreach (Transform child in parent)
        {
            InteractableObject childInteractable = child.GetComponent<InteractableObject>();
            if (childInteractable != null && !newInteractables.Contains(childInteractable))
            {
                newInteractables.Add(childInteractable);
            }

            // Panggil fungsi ini secara rekursif untuk mencari anak-anak dari child saat ini
            AddChildInteractables(child, newInteractables);
        }
    }

    // Method untuk menambahkan objek interaktif secara manual
    public void AddInteractableObject(InteractableObject obj)
    {
        if (!interactableObjects.Contains(obj))
        {
            interactableObjects.Add(obj);
        }
    }

    // Method untuk menghapus objek interaktif
    public void RemoveInteractableObject(InteractableObject obj)
    {
        if (interactableObjects.Contains(obj))
        {
            interactableObjects.Remove(obj);
        }
    }

    // Method untuk mengecek apakah objek ada dalam daftar
    public bool IsInteractable(InteractableObject obj)
    {
        return interactableObjects.Contains(obj);
    }
}
