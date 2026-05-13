using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class KarakterKompleksP2 : MonoBehaviour
{
    [Header("Script References BackgroundMusic")]
    public BackgroundMusic BackgroundMusic;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private int joystickIndex = 0;

    private Gamepad joystick;
    private Vector3 moveDirection;

    private Vector3 velocity;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float groundCheckOffset = -0.1f;

    [SerializeField] private float jumpHeight;

    public CharacterController controller;
    private Animator anim;

    // Tambahkan referensi transform melalui Inspector
    public Transform movementTransform;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Debug.Log($"{gameObject.name} menggunakan controller: {controller}");
        anim = GetComponentInChildren<Animator>();

        if (Gamepad.all.Count > joystickIndex)
        {
            joystick = Gamepad.all[joystickIndex];
        }

        // Validasi transform movement
        if (movementTransform == null)
        {
            Debug.LogError("Movement Transform belum diatur di Inspector!");
        }
    }

    private void Update()
    {
        if (Gamepad.all.Count > joystickIndex)
        {
            joystick = Gamepad.all[joystickIndex];
        }
        Move();
    }

    private void Move()
    {
        if (joystick == null || movementTransform == null) return;
        Vector3 groundCheckPosition = new Vector3(transform.position.x, transform.position.y - (controller.height / 2) + groundCheckOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(groundCheckPosition, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        var leftStick = joystick.leftStick as StickControl;
        var button5 = joystick["leftShoulder"] as ButtonControl; // L1
        var button3 = joystick["buttonSouth"] as ButtonControl; // X button

        Vector2 stickPosition = leftStick.ReadValue();
        moveDirection = new Vector3(-stickPosition.x, 0, stickPosition.y);

        // Gunakan transform dari objek yang diisi melalui Inspector
        moveDirection = movementTransform.TransformDirection(moveDirection);

        if (isGrounded)
        {
            if (stickPosition.magnitude > 0.1f && (button5 == null || !button5.isPressed))
            {
                //BackgroundMusic.PlayMusic();
                Walk();
            }
            else if (stickPosition.magnitude > 0.1f && button5.isPressed)
            {
                //BackgroundMusic.PlayMusic();
                Run();
            }
            else if (stickPosition.magnitude <= 0.1f)
            {
                Idle();
            }
            moveDirection *= moveSpeed;

            if (button3 != null && button3.wasPressedThisFrame)
            {
                //BackgroundMusic.PlayMusic();
                Jump();
            }
        }

        controller.Move(moveDirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        moveSpeed = 0;
        anim.SetTrigger("diam");
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetTrigger("jalan");
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetTrigger("lari");
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        anim.SetTrigger("lompat");
    }
}
