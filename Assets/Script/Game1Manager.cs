using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game1Manager : MonoBehaviour
{
    [Header("Referensi LayarP1G1manager")]
    public LayarP1G1manager LayarP1G1manager;
    [Header("Referensi LayarP2G1manager")]
    public LayarP2G1manager LayarP2G1manager;
    [Header("Referensi LayarP3G1manager")]
    public LayarP3G1manager LayarP3G1manager;
    [Header("Referensi LayarP4G1manager")]
    public LayarP4G1manager LayarP4G1manager;
    [Header("Referensi ForceObjectSwitcher")]
    public ForceObjectSwitcher ForceObjectSwitcher;
    [Header("References StartGameManager")]
    public StartGameManager StartGameManager;
    public List<GameObject> gameObjects;
    public GameObject Game1;
    [Header("Referensi BackgroundMusic")]
    public BackgroundMusic BackgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void PlayGame()
    {
        // Cek apakah semua objek aktif
        if (gameObjects[0].activeSelf && gameObjects[1].activeSelf && gameObjects[2].activeSelf && gameObjects[3].activeSelf)
        {
            StartGameManager.enabled = true;
            StartGameManager.ResetScript();
            StartGameManager.StartAllGames();
        }
        else
        {
            // Cek setiap objek secara terpisah dan panggil fungsi sesuai statusnya
            if (!gameObjects[0].activeSelf)
            {
                LayarP1G1manager.ALLOFF();

            }
            else
            {
                LayarP1G1manager.StartGameON();
                //StartGameManager.enabled = true;
            }

            if (!gameObjects[1].activeSelf)
            {
                LayarP2G1manager.ALLOFF();
            }
            else
            {
                LayarP2G1manager.StartGameON();
                //StartGameManager.enabled = true;
            }

            if (!gameObjects[2].activeSelf)
            {
                LayarP3G1manager.ALLOFF();
            }
            else
            {
                LayarP3G1manager.StartGameON();
            }

            if (!gameObjects[3].activeSelf)
            {
                LayarP4G1manager.ALLOFF();
            }
            else
            {
                LayarP4G1manager.StartGameON();
            }
        }

    }
    public void ResetScript()
    {
        StartCoroutine(Reload());
        //BackgroundMusic.PlayMusic();

        //HideResults

        /*GameON();
        LayarP1G1manager.ALLOFF();
        LayarP2G1manager.ALLOFF();
        LayarP3G1manager.ALLOFF();
        LayarP4G1manager.ALLOFF();
        PlayGame();
        ForceObjectSwitcher.SwitchObjectPositions();
        Debug.Log("sudah pindah");*/

    }
    private IEnumerator Reload()
    {
        GameON();
        yield return new WaitForSeconds(0.1f);
        LayarP1G1manager.ALLOFF();
        yield return new WaitForSeconds(0.1f);
        LayarP2G1manager.ALLOFF();
        yield return new WaitForSeconds(0.1f);
        LayarP3G1manager.ALLOFF();
        yield return new WaitForSeconds(0.1f);
        LayarP4G1manager.ALLOFF();
        yield return new WaitForSeconds(0.1f);
        CheckALL();
        yield return new WaitForSeconds(0.5f);
        PlayGame();
        yield return new WaitForSeconds(0.5f);
        ForceObjectSwitcher.SwitchObjectPositions();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("sudah pindah");
    }
    public void GameON()
    {
        Game1.gameObject.SetActive(true);
    }
    public void GameOFF()
    {
        Game1.gameObject.SetActive(false);
    }

    public void CheckALL()
    {
        LayarP1G1manager.CheckandFixChildrenON();
        LayarP2G1manager.CheckandFixChildrenON();
        LayarP3G1manager.CheckandFixChildrenON();
        LayarP4G1manager.CheckandFixChildrenON();

    }
    


}
