using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class InteractScriptBodyP4 : MonoBehaviour
{
    [Header("Referensi Scripts VerifP4")]
    public VerifP4 VerifP4;
    [Header("Script References")]
    public SelectionManager selectionManager;
    [Header("Script References VerifJumlahPemain")]
    public VerifJumlahPemain VerifJumlahPemain;
    public RaycastVisualizer raycastVisualizer;
    public string A;
    [Header("Gamepad Settings")]
    [SerializeField] private int joystickIndex = 1;
    private Gamepad joystick;

    private void Start()
    {
        if (Gamepad.all.Count > joystickIndex)
        {
            joystick = Gamepad.all[joystickIndex];
        }
    }

    private void Update()
    {
        if (joystick == null) return;
        if (selectionManager == null || raycastVisualizer == null || VerifP4 == null)
        {
            Debug.LogWarning("Satu atau lebih referensi belum diatur!");
            return;
        }

        // Visualisasi raycast
        Debug.DrawRay(raycastVisualizer.transform.position, raycastVisualizer.transform.forward * 10f, Color.red);

        var button2 = joystick["buttonEast"] as ButtonControl;

        // Cek apakah tombol reset ditekan
        if (button2 != null && button2.wasPressedThisFrame)
        {
            GameObject objectInFront = GetObjectInFront();
            if (objectInFront != null)
            {
                InteractableObject interactable = objectInFront.GetComponent<InteractableObject>();
                if (interactable != null && selectionManager.IsInteractable(interactable))
                {
                    for (int i = 0; i < VerifP4.PlayerBodyP4.Count; i++)
                    {
                        GameObject listObject = VerifP4.PlayerBodyP4[i];

                        if (listObject.name == A)
                        {
                            VerifP4.PlayerBodyP4[i].gameObject.SetActive(true);
                            Debug.Log($"Objek '{A}' ditemukan pada indeks {i}");
                            VerifJumlahPemain.ChoosingEndWithOutP4();
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
