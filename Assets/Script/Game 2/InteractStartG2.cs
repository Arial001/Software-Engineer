using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractStartG2 : MonoBehaviour
{
    [Header("Script References")]
    //public QuestionIndexElimination questionIndexElimination;
    public SelectionManager selectionManager;
    public RaycastVisualizer raycastVisualizer;
    [Header("Referensi StartGame")]
    public StartGame2 StartGame2;
    [Header("Referensi StartGameManager")]
    public StartGameManager StartGameManager;

    [Header("Key Settings")]
    public KeyCode resetScriptKey = KeyCode.R;

    private void Update()
    {
        if (selectionManager == null || raycastVisualizer == null )
        {
            Debug.LogWarning("Satu atau lebih referensi belum diatur!");
            return;
        }

        // Visualisasi raycast
        Debug.DrawRay(raycastVisualizer.transform.position, raycastVisualizer.transform.forward * 10f, Color.red);

        // Cek apakah tombol reset ditekan
        if (Input.GetKeyDown(resetScriptKey))
        {
            GameObject objectInFront = GetObjectInFront();
            if (objectInFront != null)
            {
                InteractableObject interactable = objectInFront.GetComponent<InteractableObject>();
                if (interactable != null && selectionManager.IsInteractable(interactable))
                {
                    StartGame2.StartGame();
                    Debug.Log("tombol R sudah ditekan");
                    if (!StartGame2.enabled)
                    {
                        //TriggerResetOnQuestionIndexElimination();
                    }
                    else
                    {
                        //StartGameManager.StartAllGames();
                        //StartGame3.StartGame();
                    }
                }
                else
                {
                    Debug.Log("Objek bukan InteractableObject yang valid dalam daftar SelectionManager");
                }

            }
        }
    }

    private GameObject GetObjectInFront()
    {
        Ray ray = new Ray(raycastVisualizer.transform.position, raycastVisualizer.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastVisualizer.raycastLayerMask))
        {
            if (hit.transform != null)
            {
                Debug.Log($"Objek di depan: {hit.transform.name}");
                return hit.transform.gameObject;
            }
        }

        return null;
    }

    //private void TriggerResetOnQuestionIndexElimination()
    //{
    //    questionIndexElimination.ResetScript();
    //    Debug.Log("Reset triggered on QuestionIndexElimination");
    //}
}
