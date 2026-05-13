using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifP1 : MonoBehaviour
{
    public GameObject Player1;
    public karakterkompleks karakterkompleksP1;
    public List<GameObject> PlayerBody;
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
    public void karakterkompleksP1OFF()
    {
        karakterkompleksP1.enabled = false;
    }
    public void karakterkompleksP1ON()
    {
        karakterkompleksP1.enabled = true;
    }
    public void Player1ON()
    {
        Player1.gameObject.SetActive(true);
    }
    public void Player1OFF()
    {
        Player1.gameObject.SetActive(false);
    }
    public void Body1()
    {
        PlayerBody[0].gameObject.SetActive(true);
    }
    public void Body2()
    {
        PlayerBody[1].gameObject.SetActive(true);
    }
    public void Body3()
    {
        PlayerBody[2].gameObject.SetActive(true);
    }
    public void Body4()
    {
        PlayerBody[3].gameObject.SetActive(true);
    }
    public void Body1OFF()
    {
        PlayerBody[0].gameObject.SetActive(false);
    }
    public void Body2OFF()
    {
        PlayerBody[1].gameObject.SetActive(false);
    }
    public void Body3OFF()
    {
        PlayerBody[2].gameObject.SetActive(false);
    }
    public void Body4OFF()
    {
        PlayerBody[3].gameObject.SetActive(false);
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
