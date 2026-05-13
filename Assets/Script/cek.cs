using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
public class VerifJumlahPemain1 : MonoBehaviour
{
    [Header("UI References")]
    public Text Verif;
    [Header("UI References")]
    public Text Confirm;
    [Header("Key Settings")]
    public KeyCode VerifKey = KeyCode.Y;
    [Header("Gamepad Settings")]
    public int joystickIndex = 0;
    private Gamepad joystick;
    [Header("Referensi VerifP1")]
    public VerifP1 VerifP1;
    [Header("Referensi VerifP2")]
    public VerifP2 VerifP2;
    [Header("Referensi VerifP4")]
    public VerifP3 VerifP3;
    [Header("Referensi VerifP4")]
    public VerifP4 VerifP4;
    private int P1 = 0;
    private int P2 = 0;
    private int P3 = 0;
    private int P4 = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Player1ON()
    {
        ConfirmUION();
        Confirm.text = "Tekan Y pada Keyboar";
        // Cek apakah tombol reset ditekan
        if (Input.GetKeyDown(VerifKey))
        {
            P1 = -2;
            VerifP1.Player1ON();
            VerifUION();
            Verif.text = "Controller P1 terdeteksi";
            Verif.fontSize = 35;
        }
        else
        {
            P1 = 2;
            Verif.text = "Controller P1 tidak terdeteksi";
        }
    }
    public void Player2ON()
    {
        joystickIndex = 0;
        ConfirmUION();
        Confirm.text = "Tekan Select pada Gamepad";
        if (Gamepad.all.Count > joystickIndex)
        {
            joystick = Gamepad.all[joystickIndex];
        }
        var Select = joystick["button9"] as ButtonControl;
        // Cek apakah tombol reset ditekan
        if (Select != null && Select.wasPressedThisFrame)
        {
            P2 = -2;

            Verif.gameObject.SetActive(true);
            Verif.text = "Controller P2 terdeteksi";
            Verif.fontSize = 35;
        }
        else
        {
            P2 = 2;
            Verif.text = "Controller P2 tidak terdeteksi";
        }
    }
    public void Player3ON()
    {
        joystickIndex = 1;
        ConfirmUION();
        Confirm.text = "Tekan Select pada Gamepad";
        if (Gamepad.all.Count > joystickIndex)
        {
            joystick = Gamepad.all[joystickIndex];
        }
        var Select = joystick["button9"] as ButtonControl;
        // Cek apakah tombol reset ditekan
        if (Select != null && Select.wasPressedThisFrame)
        {
            P3 = -2;

            Verif.gameObject.SetActive(true);
            Verif.text = "Controller P3 terdeteksi";
            Verif.fontSize = 35;
        }
        else
        {
            P3 = 2;
            Verif.text = "Controller P3 tidak terdeteksi";
        }
    }
    public void Player4ON()
    {
        joystickIndex = 2;
        ConfirmUION();
        Confirm.text = "Tekan Select pada Gamepad";
        if (Gamepad.all.Count > joystickIndex)
        {
            joystick = Gamepad.all[joystickIndex];
        }
        var Select = joystick["button9"] as ButtonControl;
        // Cek apakah tombol reset ditekan
        if (Select != null && Select.wasPressedThisFrame)
        {
            P4 = -2;

            Verif.gameObject.SetActive(true);
            Verif.text = "Controller P4 terdeteksi";
            Verif.fontSize = 35;
        }
        else
        {
            P4 = 2;
            Verif.text = "Controller P4 tidak terdeteksi";
        }
    }
    public void SkipP1()
    {
        P1 = 2;
        VerifP1.Player1OFF();
    }
    public void SkipP2()
    {
        P2 = 2;
        VerifP2.Player2OFF();
    }
    public void SkipP3()
    {
        P3 = 2;
        VerifP3.Player3OFF();
    }
    public void SkipP4()
    {
        P4 = 2;
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

    public void NextP1()
    {
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
        if (P1 < 0)
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
        if (P1 < 0)
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
        if (P1 < 0)
        {
            VerifP4.Player4ON();
        }
        else
        {
            VerifP4.Player4OFF();
        }
    }
}

