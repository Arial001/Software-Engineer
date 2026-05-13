using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PauseMenuP2 : MonoBehaviour
{
    public GameObject pauseMenu;
    public Canvas canvas;
    [Header("Referensi CameraController")]
    public CameraControllerP2 CameraController;
    [Header("Referensi karakterkompleks")]
    public KarakterKompleksP2 karakterkompleks;
    public static bool isPaused;
    [SerializeField] private int joystickIndex = 0;
    private Gamepad joystick;
    public GameObject Game1;
    public ForceObjectSwitcher ForceObjectSwitcher;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Gamepad.all.Count > joystickIndex)
        {
            joystick = Gamepad.all[joystickIndex];
        }
        var button10 = joystick["Start"] as ButtonControl;
        if (button10 != null && button10.wasPressedThisFrame)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        canvas.gameObject.SetActive(false);
        CameraController.enabled = false;
        karakterkompleks.enabled = false;
        Time.timeScale = 0f;
        isPaused = true;

        // Mengaktifkan kursor dan membuatnya terlihat
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        canvas.gameObject.SetActive(true);
        CameraController.enabled = true;
        karakterkompleks.enabled = true;
        Time.timeScale = 1f;
        isPaused = false;

        // Mengunci kursor kembali dan membuatnya tidak terlihat
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGame()
    {

        StartCoroutine(Quit());
    }
    private IEnumerator Quit()
    {
        if (Game1.activeSelf)
        {

            ForceObjectSwitcher.SwitchObjectPositions();
            yield return new WaitForSeconds(0.5f);
            Game1.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            Application.Quit();
            yield return new WaitForSeconds(0.1f);
        }
        
        Debug.Log("sudah pindah");
    }
}
