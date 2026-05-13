using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifP3 : MonoBehaviour
{
    public GameObject Player3;
    public KarakterKompleksP2 KarakterKompleksP3;
    public List<GameObject> PlayerBodyP3;
    public Camera CameraP3;
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
    public void karakterkompleksP3OFF()
    {
        KarakterKompleksP3.enabled = false;
    }
    public void karakterkompleksP3ON()
    {
        KarakterKompleksP3.enabled = true;
    }
    public void Player3ON()
    {
        Player3.gameObject.SetActive(true);
    }
    public void Player3OFF()
    {
        Player3.gameObject.SetActive(false);
    }
    public void Body1()
    {
        PlayerBodyP3[0].gameObject.SetActive(true);
    }
    public void Body2()
    {
        PlayerBodyP3[1].gameObject.SetActive(true);
    }
    public void Body3()
    {
        PlayerBodyP3[2].gameObject.SetActive(true);
    }
    public void Body4()
    {
        PlayerBodyP3[3].gameObject.SetActive(true);
    }
    public void Body1OFF()
    {
        PlayerBodyP3[0].gameObject.SetActive(false);
    }
    public void Body2OFF()
    {
        PlayerBodyP3[1].gameObject.SetActive(false);
    }
    public void Body3OFF()
    {
        PlayerBodyP3[2].gameObject.SetActive(false);
    }
    public void Body4OFF()
    {
        PlayerBodyP3[3].gameObject.SetActive(false);
    }

    public void cameraON()
    {
        CameraP3.gameObject.SetActive(true);

    }
    public void cameraOFF()
    {
        CameraP3.gameObject.SetActive(false);

    }
    
}
