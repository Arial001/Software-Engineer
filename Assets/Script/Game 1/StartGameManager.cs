// ini script buat ngatur StartGame yang banyak
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    public List<StartGame> startGameInstances = new List<StartGame>();
    public List<GameObject> Player;

    private void Start()
    {
        // Opsional: Cari semua instance StartGame di scene jika list kosong
       /* if (startGameInstances.Count == 0)
        {
            startGameInstances.AddRange(FindObjectsOfType<StartGame>());
        }*/
    }

    public void StartAllGames()
    {
        Debug.Log("StartALLGames Ketrigger");

        if (Player[0].activeSelf && Player[1].activeSelf && Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player StartALLGames aktif");
            foreach (StartGame startGame in startGameInstances)
            {
                if (startGame != null)
                {
                    startGame.Begining();
                }
            }
            Debug.Log("Semua game telah dimulai!");
        }
        else if (Player[0].activeSelf && Player[1].activeSelf && !Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 1 2 StartALLGames aktif");
            startGameInstances[0].Begining();
            startGameInstances[1].Begining();
        }
        else if (Player[0].activeSelf && !Player[1].activeSelf && Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 1 3 StartALLGames aktif");
            startGameInstances[0].Begining();
            startGameInstances[2].Begining();
        }
        else if (Player[0].activeSelf && !Player[1].activeSelf && !Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 1 4 StartALLGames aktif");
            startGameInstances[0].Begining();
            startGameInstances[3].Begining();
        }
        else if (!Player[0].activeSelf && Player[1].activeSelf && Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 2 3 StartALLGames aktif");
            startGameInstances[1].Begining();
            startGameInstances[2].Begining();
        }
        else if (!Player[0].activeSelf && Player[1].activeSelf && !Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 2 4 StartALLGames aktif");
            startGameInstances[1].Begining();
            startGameInstances[3].Begining();
        }
        else if (!Player[0].activeSelf && !Player[1].activeSelf && Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 3 4 StartALLGames aktif");
            startGameInstances[2].Begining();
            startGameInstances[3].Begining();
        }
        else if (Player[0].activeSelf && Player[1].activeSelf && Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 1 2 3 StartALLGames hidup");
            startGameInstances[0].Begining();
            startGameInstances[1].Begining();
            startGameInstances[2].Begining();
        }
        else if (Player[0].activeSelf && Player[1].activeSelf && !Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 1 2 4 StartALLGames hidup");
            startGameInstances[0].Begining();
            startGameInstances[1].Begining();
            startGameInstances[3].Begining();
        }
        else if (Player[0].activeSelf && !Player[1].activeSelf && Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 1 3 4 StartALLGames hidup");
            startGameInstances[0].Begining();
            startGameInstances[2].Begining();
            startGameInstances[3].Begining();
        }

        else if (!Player[0].activeSelf && Player[1].activeSelf && Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 2 3 4 StartALLGames aktif");
            startGameInstances[1].Begining();
            startGameInstances[2].Begining();
            startGameInstances[3].Begining();
        }

        else if (Player[0].activeSelf && !Player[1].activeSelf && !Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 2 3 4 StartALLGames mati");
            startGameInstances[0].Begining();
        }
        else if (!Player[0].activeSelf && Player[1].activeSelf && !Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 1 3 4 StartALLGames mati");
            startGameInstances[1].Begining();
        }
        else if (!Player[0].activeSelf && !Player[1].activeSelf && Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 1 2 4 StartALLGames mati");
            startGameInstances[2].Begining();
        }
        else if (!Player[0].activeSelf && !Player[1].activeSelf && !Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 1 2 3 StartALLGames mati");
            startGameInstances[3].Begining();
        }

        else if (!Player[0].activeSelf && !Player[1].activeSelf && !Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player StartALLGames mati");
            startGameInstances[0].enabled = false;
            startGameInstances[1].enabled = false;
            startGameInstances[2].enabled = false;
            startGameInstances[3].enabled = false;
            startGameInstances[0].SetWelcomeMessage();
            startGameInstances[3].SetWelcomeMessage();
        }
        Debug.Log("StartALLGames sudah selesai");
    }
    public void StartWelcomeGames()
    {
        if (Player[0].activeSelf && Player[1].activeSelf && Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player SetWelcomeMessage aktif");
            foreach (StartGame startGame in startGameInstances)
            {
                if (startGame != null)
                {
                    startGame.SetWelcomeMessage();
                }
            }
            Debug.Log("Semua game telah dimulai!");
        }
        else if (Player[0].activeSelf && Player[1].activeSelf && !Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 1 2 SetWelcomeMessage aktif");
            startGameInstances[0].SetWelcomeMessage();
            startGameInstances[1].SetWelcomeMessage();
        }
        else if (Player[0].activeSelf && !Player[1].activeSelf && Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 1 3 SetWelcomeMessage aktif");
            startGameInstances[0].SetWelcomeMessage();
            startGameInstances[2].SetWelcomeMessage();
        }
        else if (Player[0].activeSelf && !Player[1].activeSelf && !Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 1 4 SetWelcomeMessage aktif");
            startGameInstances[0].SetWelcomeMessage();
            startGameInstances[3].SetWelcomeMessage();
        }
        else if (!Player[0].activeSelf && Player[1].activeSelf && Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 2 3 SetWelcomeMessage aktif");
            startGameInstances[1].SetWelcomeMessage();
            startGameInstances[2].SetWelcomeMessage();
        }
        else if (!Player[0].activeSelf && Player[1].activeSelf && !Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 2 4 SetWelcomeMessage aktif");
            startGameInstances[1].SetWelcomeMessage();
            startGameInstances[3].SetWelcomeMessage();
        }
        else if (!Player[0].activeSelf && !Player[1].activeSelf && Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 3 4 SetWelcomeMessage aktif");
            startGameInstances[2].SetWelcomeMessage();
            startGameInstances[3].SetWelcomeMessage();
        }
        else if (Player[0].activeSelf && Player[1].activeSelf && Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 1 2 3 SetWelcomeMessage hidup");
            startGameInstances[0].SetWelcomeMessage();
            startGameInstances[1].SetWelcomeMessage();
            startGameInstances[2].SetWelcomeMessage();
        }
        else if (Player[0].activeSelf && Player[1].activeSelf && !Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 1 2 4 SetWelcomeMessage hidup");
            startGameInstances[0].SetWelcomeMessage();
            startGameInstances[1].SetWelcomeMessage();
            startGameInstances[3].SetWelcomeMessage();
        }
        else if (Player[0].activeSelf && !Player[1].activeSelf && Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 1 3 4 SetWelcomeMessage hidup");
            startGameInstances[0].SetWelcomeMessage();
            startGameInstances[2].SetWelcomeMessage();
            startGameInstances[3].SetWelcomeMessage();
        }

        else if (!Player[0].activeSelf && Player[1].activeSelf && Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 2 3 4 SetWelcomeMessage aktif");
            startGameInstances[1].SetWelcomeMessage();
            startGameInstances[2].SetWelcomeMessage();
            startGameInstances[3].SetWelcomeMessage();
        }

        else if (Player[0].activeSelf && !Player[1].activeSelf && !Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 2 3 4 SetWelcomeMessage mati");
            startGameInstances[0].SetWelcomeMessage();
        }
        else if (!Player[0].activeSelf && Player[1].activeSelf && !Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 1 3 4 SetWelcomeMessage mati");
            startGameInstances[1].SetWelcomeMessage();
        }
        else if (!Player[0].activeSelf && !Player[1].activeSelf && Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player 1 2 4 SetWelcomeMessage mati");
            startGameInstances[2].SetWelcomeMessage();
        }
        else if (!Player[0].activeSelf && !Player[1].activeSelf && !Player[2].activeSelf && Player[3].activeSelf)
        {
            Debug.Log("semua player 1 2 3 SetWelcomeMessage mati");
            startGameInstances[3].SetWelcomeMessage();
        }

        else if (!Player[0].activeSelf && !Player[1].activeSelf && !Player[2].activeSelf && !Player[3].activeSelf)
        {
            Debug.Log("semua player SetWelcomeMessage mati");
            startGameInstances[0].enabled = false;
            startGameInstances[1].enabled = false;
            startGameInstances[2].enabled = false;
            startGameInstances[3].enabled = false;
        }
        Debug.Log("Semua game telah dimulai!");
    }

    // Opsional: Metode untuk menambahkan instance StartGame secara manual
    public void AddStartGameInstance(StartGame instance)
    {
        if (!startGameInstances.Contains(instance))
        {
            startGameInstances.Add(instance);
        }
    }

    public void ResetScript()
    {
        if (Player[0].activeSelf && Player[1].activeSelf && Player[2].activeSelf && Player[3].activeSelf)
        {
            foreach (StartGame startGame in startGameInstances)
            {

                startGame.enabled = true;
            }
        }
        else if (Player[0].activeSelf && !Player[1].activeSelf && !Player[2].activeSelf && !Player[3].activeSelf)
        {
            startGameInstances[0].enabled = true;
            startGameInstances[1].enabled = false;
            startGameInstances[2].enabled = false;
            startGameInstances[3].enabled = false;
        }
        else if (!Player[0].activeSelf && Player[1].activeSelf && !Player[2].activeSelf && !Player[3].activeSelf)
        {
            startGameInstances[0].enabled = false;
            startGameInstances[1].enabled = true;
            startGameInstances[2].enabled = false;
            startGameInstances[3].enabled = false;
        }
        else if (!Player[0].activeSelf && Player[1].activeSelf && Player[2].activeSelf && Player[3].activeSelf)
        {
            startGameInstances[0].enabled = false;
            startGameInstances[1].enabled = true;
            startGameInstances[2].enabled = true;
            startGameInstances[3].enabled = true;
        }
        else if (!Player[0].activeSelf && !Player[1].activeSelf && Player[2].activeSelf && Player[3].activeSelf)
        {
            startGameInstances[0].enabled = false;
            startGameInstances[1].enabled = false;
            startGameInstances[2].enabled = true;
            startGameInstances[3].enabled = true;
        }
        else if (!Player[0].activeSelf && Player[1].activeSelf && !Player[2].activeSelf && Player[3].activeSelf)
        {
            startGameInstances[0].enabled = false;
            startGameInstances[1].enabled = true;
            startGameInstances[2].enabled = false;
            startGameInstances[3].enabled = true;
        }
        else if (Player[0].activeSelf && !Player[1].activeSelf && !Player[2].activeSelf && Player[3].activeSelf)
        {
            startGameInstances[0].enabled = true;
            startGameInstances[1].enabled = false;
            startGameInstances[2].enabled = false;
            startGameInstances[3].enabled = true;
        }
        else if (!Player[0].activeSelf && !Player[1].activeSelf && !Player[2].activeSelf && Player[3].activeSelf)
        {
            startGameInstances[0].enabled = false;
            startGameInstances[1].enabled = false;
            startGameInstances[2].enabled = false;
            startGameInstances[3].enabled = true;
        }
        else if (!Player[0].activeSelf! && Player[1].activeSelf && Player[2].activeSelf && Player[3].activeSelf)
        {
            startGameInstances[0].enabled = false;
            startGameInstances[1].enabled = false;
            startGameInstances[2].enabled = true;
            startGameInstances[3].enabled = true;
        }
        else if (!Player[0].activeSelf && !Player[1].activeSelf && !Player[2].activeSelf && !Player[3].activeSelf)
        {
            startGameInstances[0].enabled = false;
            startGameInstances[1].enabled = false;
            startGameInstances[2].enabled = false;
            startGameInstances[3].enabled = false;
        }
    }
}
