using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoader : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject ExitButton;
    public GameObject Multiplayer;
    public GameObject Single;
    public GameObject Image;

    public void DisableGameObject()
    {
        // Nonaktifkan kedua game object
        PlayButton.SetActive(false);
        ExitButton.SetActive(false);
    }

    public void EnableGameObject()
    {
        // Aktifkan kedua game object
        Multiplayer.SetActive(true);
        Single.SetActive(true);
    }
    public void DisableMultiplayer()
    {
        Multiplayer.SetActive(false);
        Single.SetActive(false);
        Image.gameObject.SetActive(true);
    }
    public void SinglePlayer()
    {
        Multiplayer.SetActive(false);
        Single.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
