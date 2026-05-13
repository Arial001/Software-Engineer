using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class VerifJumlahPemain : MonoBehaviour
{
    [Header("UI References")]
    public Text Verif;
    public Text Confirm;

    [Header("Key Settings")]
    public KeyCode VerifKey = KeyCode.Y;

    [Header("Gamepad Settings")]
    public int joystickIndex = 0;
    private Gamepad joystick;

    [Header("Referensi Scripts")]
    public VerifP1 VerifP1;
    public VerifP2 VerifP2;
    public VerifP3 VerifP3;
    public VerifP4 VerifP4;

    public GameObject Body;

    [Header("Referensi Scripts CameraManager")]
    public CameraManager CameraManager;
    [Header("Referensi Scripts InteractScriptBodyP1")]
    public InteractScriptBodyP1 InteractScriptBodyP1;
    [Header("Referensi Scripts InteractScriptBodyP2")]
    public InteractScriptBodyP2 InteractScriptBodyP2;
    [Header("Referensi Scripts InteractScriptBodyP3")]
    public InteractScriptBodyP3 InteractScriptBodyP3;
    [Header("Referensi Scripts InteractScriptBodyP4")]
    public InteractScriptBodyP4 InteractScriptBodyP4;
    [Header("Referensi Scripts ObjectManager")]
    public ObjectManager ObjectManager;

    private int P1 = 0;
    private int P2 = 0;
    private int P3 = 0;
    private int P4 = 0;

    public int A = 0;
    public int B = 0;
    public int C = 0;
    public int D = 0;
    public int E = 0;
    public int F = 0;
    public int G = 0;
    public int H = 0;



    private void Start()
    {
        // Inisialisasi jika diperlukan
    }
    public void ResetScript()
    {
        P1 = 0;
        P2 = 0;
        P3 = 0;
        P4 = 0;
        A = 0;
        B = 0;
        C = 0;
        D = 0;
        E = 0;
        F = 0;
        VerifP1.karakterkompleksP1OFF();
        VerifP2.karakterkompleksP2OFF();
        VerifP3.karakterkompleksP3OFF();
        VerifP4.karakterkompleksP4OFF();
        VerifP1.ResetScript();
        VerifP2.ResetScript();
        VerifP3.ResetScript();
        VerifP4.ResetScript();
    }
    public void Player1ON()
    {
        F = -2;
        Confirm.text = string.Empty;
        ConfirmUION();
        Confirm.text = "Tekan Y pada Keyboard";
        StartCoroutine(CheckKeyboardInput());
    }

    private IEnumerator CheckKeyboardInput()
    {
        while (true)
        {
            if(F < 0)
            {
                if (Input.GetKeyDown(VerifKey))
                {
                    P1 = -2;
                    A = -2;
                    VerifP1.Player1ON();
                    VerifUION();
                    Verif.text = "Controller P1 terdeteksi";
                    Verif.fontSize = 35;
                    yield break;
                }

            }
            else if(F > 0)
            {
                yield break;
            }
            yield return null;
            
        }
    }

    public void Player2ON()
    {
        E = -2;
        Confirm.text = string.Empty;
        joystickIndex = 0;
        ConfirmUION();
        Confirm.text = "Tekan Select pada Gamepad";
        StartCoroutine(CheckJoystickInput(0));
    }

    public void Player3ON()
    {
        G = -2;
        Confirm.text = string.Empty;
        joystickIndex = 1;
        ConfirmUION();
        Confirm.text = "Tekan Select pada Gamepad";
        StartCoroutine(CheckJoystickInput(1));
    }

    public void Player4ON()
    {
        H = -2;
        Confirm.text = string.Empty;
        joystickIndex = 2;
        ConfirmUION();
        Confirm.text = "Tekan Select pada Gamepad";
        StartCoroutine(CheckJoystickInput(2));
    }

    private IEnumerator CheckJoystickInput(int index)
    {
        while (true)
        {
            if (Gamepad.all.Count > index)
            {
                joystick = Gamepad.all[index];
                var Select = joystick["Select"] as ButtonControl;
                if (Select != null && Select.isPressed)
                {
                    int playerNumber = index + 2;
                    SetPlayerDetected(playerNumber);
                    yield break;
                }
            }
            yield return null;
        }
    }

    private void SetPlayerDetected(int playerNumber)
    {
        switch (playerNumber)
        {
            case 2:
                if(E < 0)
                {
                    Confirm.text = string.Empty;
                    P2 = -2;
                    B = -2;
                    VerifP2.Player2ON();
                    VerifUION();
                    Verif.text = $"Controller P{playerNumber} terdeteksi";
                    Verif.fontSize = 35;
                }
                break;
            case 3:
                if(G < 0)
                {
                    Confirm.text = string.Empty;
                    P3 = -2;
                    C = -2;
                    VerifP3.Player3ON();
                    VerifUION();
                    Verif.text = $"Controller P{playerNumber} terdeteksi";
                    Verif.fontSize = 35;
                }
                break;
            case 4:
                if(H < 0)
                {
                    Confirm.text = string.Empty;
                    P4 = -2;
                    D = -2;
                    VerifP4.Player4ON();
                    VerifUION();
                    Verif.text = $"Controller P{playerNumber} terdeteksi";
                    Verif.fontSize = 35;
                }
                break;
        }

    }

    public void SkipP1()
    {
        F = 2;
        StopCoroutine(CheckKeyboardInput());
        P1 = 2;
        A = 2;
        VerifP1.Player1OFF();
    }

    public void SkipP2()
    {
        E = 2;
        StopCoroutine(CheckJoystickInput(0));
        P2 = 2;
        B = 2;
        VerifP2.Player2OFF();
    }

    public void SkipP3()
    {
        G = 2;
        StopCoroutine(CheckJoystickInput(1));
        P3 = 2;
        C = 2;
        VerifP3.Player3OFF();
    }

    public void SkipP4()
    {
        H = 2;
        StopCoroutine(CheckJoystickInput(2));
        P4 = 2;
        D = 2;
        VerifP4.Player4OFF();
    }

    public void VerifUION()
    {
        Verif.gameObject.SetActive(true);
    }

    public void VerifUIOFF()
    {
        Verif.gameObject.SetActive(false);
    }

    public void ConfirmUION()
    {
        Confirm.gameObject.SetActive(true);
    }

    public void ConfirmUIOFF()
    {
        Confirm.gameObject.SetActive(false);
    }

    public void NextP1Single()
    {
        F = 2;
        StopCoroutine(CheckKeyboardInput());
        if (P1 < 0)
        {
            VerifP1.Player1ON();
            P2 = 2;
            P3 = 2;
            P4 = 2;
            B = 2;
            C = 2;
            D = 2;
            Debug.Log($"p1 = {P1}");
        }
        else
        {
            VerifP1.Player1OFF();
            Application.Quit();
        }
    }
    public void NextP1()
    {
        F = 2;
        StopCoroutine(CheckKeyboardInput());
        if (P1 < 0)
        {
            VerifP1.Player1ON();
        }
        else
        {
            VerifP1.Player1OFF();
        }
    }

    public void NextP2()
    {
        E = 2;
        StopCoroutine(CheckJoystickInput(0));
        if (P2 < 0)
        {
            VerifP2.Player2ON();
        }
        else
        {
            VerifP2.Player2OFF();
        }
    }

    public void NextP3()
    {
        G = 2;
        StopCoroutine(CheckJoystickInput(1));
        if (P3 < 0)
        {
            VerifP3.Player3ON();
        }
        else
        {
            VerifP3.Player3OFF();
        }
    }

    public void NextP4()
    {
        H = 2;
        StopCoroutine(CheckJoystickInput(2));
        if (P4 < 0)
        {
            VerifP4.Player4ON();
        }
        else
        {
            VerifP4.Player4OFF();
        }
    }
    public void ChooseBodyP1()
    {
        if (P1 < 0 && A < 0)
        {
            ObjectManager.Gobject2OFF();
            ObjectManager.Gobject3OFF();
            ObjectManager.Gobject4OFF();
            ObjectManager.Gobject1ON();
            InteractScriptBodyP2.enabled = false;
            InteractScriptBodyP3.enabled = false;
            InteractScriptBodyP4.enabled = false;
            InteractScriptBodyP1.enabled = true;
        }
        else
        {
            ObjectManager.Gobject1OFF();
            InteractScriptBodyP1.enabled = false;
            ChooseBodyP2();
        }
    }

    public void ChooseBodyP1Single()
    {
        if (P1 < 0 && A < 0)
        {
            ObjectManager.Gobject2OFF();
            ObjectManager.Gobject3OFF();
            ObjectManager.Gobject4OFF();
            ObjectManager.Gobject1ON();
            InteractScriptBodyP2.enabled = false;
            InteractScriptBodyP3.enabled = false;
            InteractScriptBodyP4.enabled = false;
            InteractScriptBodyP1.enabled = true;
        }
        else
        {
            ObjectManager.Gobject1OFF();
            InteractScriptBodyP1.enabled = false;
            Application.Quit();
        }
    }
    public void ChooseBodyP2()
    {
        if (P2 < 0 && B < 0)
        {
            ObjectManager.Gobject1OFF();
            ObjectManager.Gobject3OFF();
            ObjectManager.Gobject4OFF();
            ObjectManager.Gobject2ON();
            InteractScriptBodyP1.enabled = false;
            InteractScriptBodyP3.enabled = false;
            InteractScriptBodyP4.enabled = false;
            InteractScriptBodyP2.enabled = true;
        }
        else
        {
            ObjectManager.Gobject2OFF();
            InteractScriptBodyP2.enabled = false;
            ChooseBodyP3();
        }
    }
    public void ChooseBodyP3()
    {
        if (P3 < 0 && C < 0)
        {
            ObjectManager.Gobject1OFF();
            ObjectManager.Gobject4OFF();
            ObjectManager.Gobject2OFF();
            ObjectManager.Gobject3ON();
            InteractScriptBodyP1.enabled = false;
            InteractScriptBodyP2.enabled = false;
            InteractScriptBodyP4.enabled = false;
            InteractScriptBodyP3.enabled = true;

        }
        else
        {
            ObjectManager.Gobject3OFF();
            InteractScriptBodyP3.enabled = false;
            ChooseBodyP4();
        }
    }
    public void ChooseBodyP4()
    {

        if (P4 < 0 && D < 0)
        {
            ObjectManager.Gobject1OFF();
            ObjectManager.Gobject2OFF();
            ObjectManager.Gobject3OFF();
            ObjectManager.Gobject4ON();
            InteractScriptBodyP1.enabled = false;
            InteractScriptBodyP2.enabled = false;
            InteractScriptBodyP3.enabled = false;
            InteractScriptBodyP4.enabled = true;



        }
        else
        {
            ObjectManager.Gobject4OFF();
            InteractScriptBodyP4.enabled = false;
            if (!VerifP4.Player4.activeSelf)
            {
                ChoosingEndWithOutP4();
            }
            else
            {
                if (!VerifP1.Player1.activeSelf && VerifP2.Player2.activeSelf && VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
                {
                    StartCoroutine(PlayerOneOFF());
                    return;
                }
                else if (!VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
                {
                    StartCoroutine(PlayerOne2OFF());
                }
                else if (!VerifP1.Player1.activeSelf && VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
                {
                    StartCoroutine(PlayerOne3OFF());
                }
                else if (VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
                {
                    StartCoroutine(PlayerTwo3OFF());
                }
                else if (VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
                {
                    StartCoroutine(PlayerTwoOFF());
                }
                else if (VerifP1.Player1.activeSelf && VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
                {
                    StartCoroutine(PlayerThreeOFF());
                }
                else if (!VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && VerifP4.Player4.activeSelf)
                {
                    PlayerOne23OFF();
                }
                else
                {
                    Application.Quit();
                }
            }
        }
    }

    public void ChoosingEndWithP4()
    {
    }
    public void ChoosingEndWithOutP4()
    {
        if (!VerifP1.Player1.activeSelf && VerifP2.Player2.activeSelf && VerifP3.Player3.activeSelf && !VerifP4.Player4.activeSelf)
        {
            StartCoroutine(PlayerOne4OFF());
            return;
        }
        else if (!VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && VerifP3.Player3.activeSelf && !VerifP4.Player4.activeSelf)
        {
            StartCoroutine(PlayerOne24OFF());
            return;
        }
        else if (!VerifP1.Player1.activeSelf && VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && !VerifP4.Player4.activeSelf)
        {
            StartCoroutine(PlayerOne34OFF());
            return;
        }
        else if (VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && VerifP3.Player3.activeSelf && !VerifP4.Player4.activeSelf)
        {
            StartCoroutine(PlayerTwo4OFF());
        }
        else if (VerifP1.Player1.activeSelf && !VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && !VerifP4.Player4.activeSelf)
        {
            StartCoroutine(PlayerTwo34OFF());
        }
        else if (VerifP1.Player1.activeSelf && VerifP2.Player2.activeSelf && !VerifP3.Player3.activeSelf && !VerifP4.Player4.activeSelf)
        {
            StartCoroutine(PlayerThree4OFF());
        }
        else
        {
            Application.Quit();
        }
    }

    private IEnumerator PlayerOneOFF()
    {
        CameraManager.ChooseCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.MainMenuCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        Body.gameObject.SetActive(false);
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
        CameraManager.ChooseCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.MainMenuCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        Body.gameObject.SetActive(false);
        VerifP4.karakterkompleksP4ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerOne2OFF()
    {
        CameraManager.ChooseCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.MainMenuCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        Body.gameObject.SetActive(false);
        VerifP3.karakterkompleksP3ON();
        yield return new WaitForSeconds(0.1f);
        VerifP4.karakterkompleksP4ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerOne3OFF()
    {
        CameraManager.ChooseCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.MainMenuCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        Body.gameObject.SetActive(false);
        VerifP2.karakterkompleksP2ON();
        yield return new WaitForSeconds(0.1f);
        VerifP4.karakterkompleksP4ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }

    private IEnumerator PlayerTwoOFF()
    {
        CameraManager.ChooseCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.MainMenuCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP1ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        Body.gameObject.SetActive(false);
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
        CameraManager.ChooseCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.MainMenuCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP1ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        Body.gameObject.SetActive(false);
        VerifP1.karakterkompleksP1ON();
        yield return new WaitForSeconds(0.1f);
        VerifP4.karakterkompleksP4ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerThreeOFF()
    {
        CameraManager.ChooseCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.MainMenuCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP1ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP4ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        Body.gameObject.SetActive(false);
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
        CameraManager.ChooseCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.MainMenuCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        Body.gameObject.SetActive(false);
        VerifP2.karakterkompleksP2ON();
        yield return new WaitForSeconds(0.1f);
        VerifP3.karakterkompleksP3ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerOne24OFF()
    {
        CameraManager.ChooseCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.MainMenuCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        Body.gameObject.SetActive(false);
        VerifP3.karakterkompleksP3ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerOne34OFF()
    {
        CameraManager.ChooseCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.MainMenuCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        Body.gameObject.SetActive(false);
        VerifP2.karakterkompleksP2ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }

    private IEnumerator PlayerTwo4OFF()
    {
        CameraManager.ChooseCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.MainMenuCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP1ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP3ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        Body.gameObject.SetActive(false);
        VerifP1.karakterkompleksP1ON();
        yield return new WaitForSeconds(0.1f);
        VerifP3.karakterkompleksP3ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerTwo34OFF()
    {
        CameraManager.ChooseCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.MainMenuCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP1ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        Body.gameObject.SetActive(false);
        VerifP1.karakterkompleksP1ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }
    private IEnumerator PlayerThree4OFF()
    {
        CameraManager.ChooseCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.MainMenuCameraOFF();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP1ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.CameraP2ON();
        yield return new WaitForSeconds(0.1f);
        CameraManager.UpdateCameraViewports();
        yield return new WaitForSeconds(0.1f);
        Body.gameObject.SetActive(false);
        VerifP1.karakterkompleksP1ON();
        yield return new WaitForSeconds(0.1f);
        VerifP2.karakterkompleksP2ON();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Mulai");
    }

    public void ChoosingP1End()
    {
        VerifP1.karakterkompleksP1ON();
        Body.gameObject.SetActive(false);
        CameraManager.ChooseCameraOFF();
        CameraManager.CameraP1ON();
        CameraManager.MainMenuCameraOFF();
        //CameraManager.SetCameraP1SingleViewport();
    }
}
