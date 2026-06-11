using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManagerG3 : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject interaction_Info_UI;
    private Text interaction_text;

    [Header("Interactable Objects")]
    [Tooltip("Daftar objek yang bisa diinteraksi di scene.")]
    public List<InteractableObject> interactableObjects;

    [Header("Raycast Reference")]
    [SerializeField] private RaycastVisualizer raycastVisualizer;

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();
        interaction_Info_UI.SetActive(false);

        if (raycastVisualizer == null)
        {
            //Debug.LogError("RaycastVisualizer belum diatur di SelectionManager!");
            enabled = false;
            return;
        }

        CleanUpMissingObjects();
    }

    void Update()
    {
        if (raycastVisualizer == null) return;

        if (raycastVisualizer.HasHit)
        {
            Transform selectionTransform = raycastVisualizer.CurrentHit.transform;
            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

            if (interactable != null && interactableObjects.Contains(interactable))
            {
                interaction_text.text = interactable.GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else
            {
                interaction_Info_UI.SetActive(false);
            }
        }
        else
        {
            interaction_Info_UI.SetActive(false);
        }
    }

    public void AddChildInteractableObjects()
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

    private void FindAllInteractableObjectsInScene()
    {
        interactableObjects.Clear();
        InteractableObject[] foundObjects = FindObjectsOfType<InteractableObject>();
        interactableObjects.AddRange(foundObjects);
    }

    public bool IsInteractable(InteractableObject obj)
    {
        return interactableObjects.Contains(obj);
    }
    public void CleanUpMissingObjects()
    {
        // Perintah ini akan mengecek list dan membuang semua elemen yang isinya 'null'
        interactableObjects.RemoveAll(item => item == null);

        Debug.Log("List telah dibersihkan dari objek yang Missing!");
    }
}