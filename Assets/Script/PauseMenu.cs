using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Canvas canvas;
    [Header("Referensi CameraController")]
    public CameraController CameraController;
    [Header("Referensi karakterkompleks")]
    public karakterkompleks karakterkompleks;
    public static bool isPaused;
    public GameObject Game1;
    public ForceObjectSwitcher ForceObjectSwitcher;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
