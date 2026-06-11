using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InteractScriptGame1P2 : MonoBehaviour
{
    [Header("Script References")]
    public SelectionManager selectionManager;
    [Header("Script References Game1Manager")]
    public Game1Manager Game1Manager;
    [Header("Script References LoginGame2")]
    public LoginGame2 LoginGame2;
    [Header("Script References LoginGame3")]
    public LoginGame3 LoginGame3;
    [Header("Script References RaycastVisualizer")]
    public RaycastVisualizer raycastVisualizer;
    [Header("Script References BackgroundMusic")]
    public BackgroundMusic BackgroundMusic;
    public string A;
    public GameObject game1;
    public GameObject game2;
    public GameObject game3;
    [Header("Gamepad Settings")]
    [SerializeField] private int joystickIndex = 0;
    private Gamepad joystick;

    private void Update()
    {
        if (Gamepad.all.Count > joystickIndex)
        {
            joystick = Gamepad.all[joystickIndex];
        }
        if (selectionManager == null || raycastVisualizer == null)
        {
            Debug.LogWarning("Satu atau lebih referensi belum diatur!");
            return;
        }

        // Visualisasi raycast
        Debug.DrawRay(raycastVisualizer.transform.position, raycastVisualizer.transform.forward * 10f, Color.red);
        var buttonR2 = joystick["leftShoulder"] as ButtonControl;    // L1

        // Cek apakah tombol reset ditekan
        if (buttonR2 != null && buttonR2.wasPressedThisFrame)
        {
            GameObject objectInFront = GetObjectInFront();
            if (objectInFront != null)
            {
                InteractableObject interactable = objectInFront.GetComponent<InteractableObject>();
                if (interactable != null && selectionManager.IsInteractable(interactable))
                {
                    BackgroundMusic.PlayMusic();
                    /*if (game1.transform.name == A)
                    {
                        Game1Manager.ResetScript();
                        //ForceObjectSwitcher.SwitchObjectPositions();
                        return;
                    }
                    /*if (game2.transform.name == A)
                    {
                        LoginGame2.LoginKeGame2();
                        //ForceObjectSwitcher.SwitchObjectPositions();
                        return;
                    }*/
                    if (game3.transform.name == A)
                    {
                        LoginGame3.LoginKeGame2();
                        //ForceObjectSwitcher.SwitchObjectPositions();
                        return;
                    }
                }
                else
                {
                    BackgroundMusic.PlayMusic();
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
