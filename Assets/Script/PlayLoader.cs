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
    public GameObject Image_logo;
    public GameObject Image_Tema;
    public GameObject IPA;
    public GameObject IPS;
    public GameObject SEJARAH;
    public GameObject Image_Tingkat_Kesulitan;
    public GameObject SD;
    public GameObject SMP;
    public GameObject SMA;
    public GameObject Random;
    public GameObject CUSTOM;

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
    public void TemaAktif()
    {
        //Image_logo.SetActive(true);
        Image_Tema.SetActive(true);
        IPA.SetActive(true);
        IPS.SetActive(true);
        SEJARAH.SetActive(true);
    }

    public void TemaMati()
    {
        Image_Tema.SetActive(false);
        IPA.SetActive(false);
        IPS.SetActive(false);
        SEJARAH.SetActive(false);
    }
    public void TingkatKesulitanAktif()
    {
        Image_Tingkat_Kesulitan.SetActive(true);
        SD.SetActive(true);
        SMP.SetActive(true);
        SMA.SetActive(true);
        Random.SetActive(true);
        CUSTOM.SetActive(true);
    }

    public void TingkatKesulitanMati()
    {
        Image_Tingkat_Kesulitan.SetActive(false);
        SD.SetActive(false);
        SMP.SetActive(false);
        SMA.SetActive(false);
        Random.SetActive(false);
        CUSTOM.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
