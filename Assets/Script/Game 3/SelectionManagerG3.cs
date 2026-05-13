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

        FindAllInteractableObjectsInScene();
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
}