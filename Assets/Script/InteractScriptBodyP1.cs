using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScriptBodyP1 : MonoBehaviour
{
    [Header("Referensi Scripts VerifP1")]
    public VerifP1 VerifP1;
    [Header("Script References")]
    public SelectionManager selectionManager;
    [Header("Script References VerifJumlahPemain")]
    public VerifJumlahPemain VerifJumlahPemain;
    public RaycastVisualizer raycastVisualizer;
    public string A;

    [Header("Key Settings")]
    public KeyCode resetScriptKey = KeyCode.R;

    private void Update()
    {
        if (selectionManager == null || raycastVisualizer == null || VerifP1 == null)
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
                    for (int i = 0; i < VerifP1.PlayerBody.Count; i++)
                    {
                        GameObject listObject = VerifP1.PlayerBody[i];

                        if (listObject.name == A)
                        {
                            if(VerifJumlahPemain.B > 0 && VerifJumlahPemain.C > 0 && VerifJumlahPemain.D > 0)
                            {
                                VerifP1.PlayerBody[i].gameObject.SetActive(true);
                                Debug.Log($"Objek '{A}' ditemukan pada indeks {i}");
                                VerifJumlahPemain.ChoosingP1End();
                            }
                            VerifP1.PlayerBody[i].gameObject.SetActive(true);
                            Debug.Log($"Objek '{A}' ditemukan pada indeks {i}");
                            VerifJumlahPemain.ChooseBodyP2();
                            return;
                        }
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
                A = hit.transform.name;
                return hit.transform.gameObject;
            }
        }

        return null;
    }
    private void LogHitObjectName(GameObject hitObject)
    {
        // Menampilkan nama objek yang terkena raycast
        Debug.Log($"Nama objek yang terkena raycast: {hitObject.name}");
    }
}

