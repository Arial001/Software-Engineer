using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlayG2 : MonoBehaviour
{
    public List<GameObject> ObjectGame2 = new List<GameObject>();
    [Header("Referensi Scripts")]
    public CameraManager CameraManager;
    public VerifP1 VerifP1;
    public VerifP2 VerifP2;
    public VerifP3 VerifP3;
    public VerifP4 VerifP4;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void BeforeStart()
    {
        VerifP1.karakterkompleksP1OFF();
        VerifP2.karakterkompleksP2OFF();
        VerifP3.karakterkompleksP3OFF();
        VerifP4.karakterkompleksP4OFF();
        
        
    }
    public void MAtikanSemuaKamera()
    {
        CameraManager.DisableAllCameras();
    }

    public void DummyFloorPokerOFF()
    {
        ObjectGame2[0].SetActive(false);
    }
    public void DummyFloorPokerON()
    {
        ObjectGame2[0].SetActive(true);
    }
    public void DummyDeskPokerOFF()
    {
        ObjectGame2[1].SetActive(false);
    }
    public void DummyDeskPokerON()
    {
        ObjectGame2[1].SetActive(true);
    }
    public void FloorPokerOFF()
    {
        ObjectGame2[2].SetActive(false);
    }
    public void FloorPokerON()
    {
        ObjectGame2[2].SetActive(true);
    }
    public void DeskPokerOFF()
    {
        ObjectGame2[3].SetActive(false);
    }
    public void DeskPokerON()
    {
        ObjectGame2[3].SetActive(true);
    }
    public void CameraTOPG2ON()
    {
        ObjectGame2[4].SetActive(true);
    }
    public void CameraTOPG2OFF()
    {
        ObjectGame2[4].SetActive(false);
    }

    public void PlayGame2()
    {
        if (!VerifP4.Player4.activeSelf)
        {
            ChoosingEndWithOutP4();
            Debug.Log("line77");
        }
        else
        {
            if (!VerifP1.Player1.activeSelf && VerifP2.Player2.activeSelf && VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
            {
                StartCoroutine(PlayerOneOFF());
                Debug.Log("line84");
                return;
            }
            else if (!VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
            {
                StartCoroutine(PlayerOne2OFF());
                Debug.Log("line90");
            }
            else if (!VerifP1.Player1.activeSelf && VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
            {
                StartCoroutine(PlayerOne3OFF());
                Debug.Log("line95");
            }
            else if (VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
            {
                StartCoroutine(PlayerTwo3OFF());
                Debug.Log("line100");
            }
            else if (VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
            {
                StartCoroutine(PlayerTwoOFF());
                Debug.Log("line105");
            }
            else if (VerifP1.Player1.activeSelf && VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
            {
                StartCoroutine(PlayerThreeOFF());
                Debug.Log("line110");
            }
            else if (!VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
            {
                PlayerOne23OFF();
                Debug.Log("line115");
            }
            else
            {
                Application.Quit();
            }
        }
    }
    public void ChoosingEndWithOutP4()
    {
        if (!VerifP1.Player1.activeSelf && VerifP2.Player2.activeSelf && VerifP3.Player3.activeSelf && !VerifP4.Player4.activeSelf)
        {
            StartCoroutine(PlayerOne4OFF());
            Debug.Log("line128");
        }
        else if (!VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && VerifP3.Player3.activeSelf && !VerifP4.Player4.activeSelf)
        {
            StartCoroutine(PlayerOne24OFF());
            Debug.Log("line134");
        }
        else if (!VerifP1.Player1.activeSelf && VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && !VerifP4.Player4.activeSelf)
        {
            StartCoroutine(PlayerOne34OFF());
            Debug.Log("line140");
        }
        else if (VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && VerifP3.Player3.activeSelf && !VerifP4.Player4.activeSelf)
        {
            StartCoroutine(PlayerTwo4OFF());
            Debug.Log("line146");
        }
        else if (VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && !VerifP4.Player4.activeSelf)
        {
            StartCoroutine(PlayerTwo34OFF());
            Debug.Log("line151");
        }
        else if (VerifP1.Player1.activeSelf && VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && !VerifP4.Player4.activeSelf)
        {
            StartCoroutine(PlayerThree4OFF());
            Debug.Log("line155");
        }
        else if (VerifP1.Player1.activeSelf && VerifP2.Player2.activeSelf && VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
        {
            PlayerALLON();
            Debug.Log("line162");
        }
        else
        {
            Application.Quit();
        }
    }

    public void EndGame2()
    {
        if (VerifP1.Player1.activeSelf == true)
        {
            VerifP1.cameraON();
        }
    }
    public void CekposisiKamera()
    {
        CameraManager.UpdateCameraViewports();
    }
    public void AfterStart()
    {
        VerifP1.cameraOFF();
        VerifP2.cameraOFF();
        VerifP3.cameraOFF();
        VerifP4.cameraOFF();

    }
    private IEnumerator PlayerOneOFF()
    {
        Debug.Log("line190");
        CameraManager.CameraP1G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP2.karakterkompleksP2ON();
        yield return new WaitForSeconds(0.1f);
        VerifP3.karakterkompleksP3ON();
        yield return new WaitForSeconds(0.1f);
        VerifP4.karakterkompleksP4ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerOne23OFF()
    {
        CameraManager.CameraP1G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP4.karakterkompleksP4ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerOne2OFF()
    {
        CameraManager.CameraP1G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP3.karakterkompleksP3ON();
        yield return new WaitForSeconds(0.1f);
        VerifP4.karakterkompleksP4ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerOne3OFF()
    {
        CameraManager.CameraP1G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP2.karakterkompleksP2ON();
        yield return new WaitForSeconds(0.1f);
        VerifP4.karakterkompleksP4ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }

    private IEnumerator PlayerTwoOFF()
    {
        CameraManager.CameraP2G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP1G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP1.karakterkompleksP1ON();
        yield return new WaitForSeconds(0.1f);
        VerifP3.karakterkompleksP3ON();
        yield return new WaitForSeconds(0.1f);
        VerifP4.karakterkompleksP4ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerTwo3OFF()
    {
        CameraManager.CameraP2G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP1G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP1.karakterkompleksP1ON();
        yield return new WaitForSeconds(0.1f);
        VerifP4.karakterkompleksP4ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerThreeOFF()
    {
        CameraManager.CameraP3G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP1G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP1.karakterkompleksP1ON();
        yield return new WaitForSeconds(0.1f);
        VerifP2.karakterkompleksP2ON();
        yield return new WaitForSeconds(0.1f);
        VerifP4.karakterkompleksP4ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerOne4OFF()
    {
        CameraManager.CameraP1G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP2.karakterkompleksP2ON();
        yield return new WaitForSeconds(0.1f);
        VerifP3.karakterkompleksP3ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerOne24OFF()
    {
        CameraManager.CameraP1G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP3.karakterkompleksP3ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerOne34OFF()
    {
        CameraManager.CameraP1G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP2.karakterkompleksP2ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }

    private IEnumerator PlayerTwo4OFF()
    {
        CameraManager.CameraP2G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP1G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP1.karakterkompleksP1ON();
        yield return new WaitForSeconds(0.1f);
        VerifP3.karakterkompleksP3ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerTwo34OFF()
    {
        CameraManager.CameraP2G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP1G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP1.karakterkompleksP1ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerThree4OFF()
    {
        CameraManager.CameraP3G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2OFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP1G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP1.karakterkompleksP1ON();
        yield return new WaitForSeconds(0.1f);
        VerifP2.karakterkompleksP2ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerALLON()
    {
        CameraManager.CameraP2G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4G2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP1G2ON();
        yield return new WaitForSeconds(0.1f);
        //CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        VerifP1.karakterkompleksP1ON();
        yield return new WaitForSeconds(0.1f);
        VerifP2.karakterkompleksP2ON();
        yield return new WaitForSeconds(0.1f);
        VerifP3.karakterkompleksP3ON();
        yield return new WaitForSeconds(0.1f);
        VerifP4.karakterkompleksP4ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
