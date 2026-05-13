using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifP2 : MonoBehaviour
{
    public GameObject Player2;
    public KarakterKompleksP2 KarakterKompleksP2;
    public List<GameObject> PlayerBodyP2;
    public Camera CameraP2;

    public void Start()
    {
        
    }
    public void ResetScript()
    {
        Body1OFF();
        Body2OFF();
        Body3OFF();
        Body4OFF();
    }
    public void karakterkompleksP2OFF()
    {
        KarakterKompleksP2.enabled = false;
    }
    public void karakterkompleksP2ON()
    {
        KarakterKompleksP2.enabled = true;
    }
    public void Player2ON()
    {
        Player2.gameObject.SetActive(true);
    }
    public void Player2OFF()
    {
        Player2.gameObject.SetActive(false);
    }
    public void Body1()
    {
        PlayerBodyP2[0].gameObject.SetActive(true);
    }
    public void Body2()
    {
        PlayerBodyP2[1].gameObject.SetActive(true);
    }
    public void Body3()
    {
        PlayerBodyP2[2].gameObject.SetActive(true);
    }
    public void Body4()
    {
        PlayerBodyP2[3].gameObject.SetActive(true);
    }
    public void Body1OFF()
    {
        PlayerBodyP2[0].gameObject.SetActive(false);
    }
    public void Body2OFF()
    {
        PlayerBodyP2[1].gameObject.SetActive(false);
    }
    public void Body3OFF()
    {
        PlayerBodyP2[2].gameObject.SetActive(false);
    }
    public void Body4OFF()
    {
        PlayerBodyP2[3].gameObject.SetActive(false);
    }

    public void cameraON()
    {
        CameraP2.gameObject.SetActive(true);

    }
    public void cameraOFF()
    {
        CameraP2.gameObject.SetActive(false);

    }
    
}
