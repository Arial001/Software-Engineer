using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VerifP4 : MonoBehaviour
{
    public GameObject Player4;
    public KarakterKompleksP2 KarakterKompleksP4;
    public List<GameObject> PlayerBodyP4;
    public Camera CameraP1;

    

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
    public void karakterkompleksP4OFF()
    {
        KarakterKompleksP4.enabled = false;
    }
    public void karakterkompleksP4ON()
    {
        KarakterKompleksP4.enabled = true;
    }
    public void Player4ON()
    {
        
        Player4.gameObject.SetActive(true);
    }
    public void Player4OFF()
    {
        Player4.gameObject.SetActive(false);
    }
    public void Body1()
    {
        PlayerBodyP4[0].gameObject.SetActive(true);
    }
    public void Body2()
    {
        PlayerBodyP4[1].gameObject.SetActive(true);
    }
    public void Body3()
    {
        PlayerBodyP4[2].gameObject.SetActive(true);
    }
    public void Body4()
    {
        PlayerBodyP4[3].gameObject.SetActive(true);
    }
    public void Body1OFF()
    {
        PlayerBodyP4[0].gameObject.SetActive(false);
    }
    public void Body2OFF()
    {
        PlayerBodyP4[1].gameObject.SetActive(false);
    }
    public void Body3OFF()
    {
        PlayerBodyP4[2].gameObject.SetActive(false);
    }
    public void Body4OFF()
    {
        PlayerBodyP4[3].gameObject.SetActive(false);
    }

    public void cameraON()
    {
        CameraP1.gameObject.SetActive(true);

    }
    public void cameraOFF()
    {
        CameraP1.gameObject.SetActive(false);

    }

    
    
}
