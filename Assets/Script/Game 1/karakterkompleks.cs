using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karakterkompleks : MonoBehaviour
{
    [Header("Script References BackgroundMusic")]
    public BackgroundMusic BackgroundMusic;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;

    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;

    // --- Variabel baru untuk menggeser posisi ground check ---
    [SerializeField] private float groundCheckOffset = -0.1f;

    [SerializeField] private float jumpHeight = 3f;

    public CharacterController controller;
    private Animator anim;

    private string currentAnimationState;
    private const string STATE_IDLE = "diam";
    private const string STATE_WALK = "jalan";
    private const string STATE_RUN = "lari";
    private const string STATE_JUMP = "lompat";

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        if (controller == null)
        {
            Debug.LogError("CharacterController tidak ditemukan! Pastikan GameObject memiliki komponen CharacterController.");
        }
        if (anim == null)
        {
            Debug.LogWarning("Animator tidak ditemukan di anak-anak. Pastikan model karakter memiliki Animator.");
        }

        currentAnimationState = STATE_IDLE;
        if (anim != null)
        {
            anim.SetTrigger(STATE_IDLE);
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // --- Posisi baru untuk ground check: di kaki karakter ---
        Vector3 groundCheckPosition = new Vector3(transform.position.x, transform.position.y - (controller.height / 2) + groundCheckOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(groundCheckPosition, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // --- Perbaikan logika strafe: hilangkan tanda minus ---
        moveDirection = transform.right * -horizontalInput + transform.forward * verticalInput;
        moveDirection.Normalize();

        if (isGrounded)
        {
            if (moveDirection.magnitude > 0 && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (moveDirection.magnitude > 0 && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else
            {
                Idle();
            }

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            SetAnimationState(STATE_JUMP);
        }

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void SetAnimationState(string newState)
    {
        if (anim == null) return;

        if (currentAnimationState != newState)
        {
            anim.SetTrigger(newState);
            currentAnimationState = newState;
        }
    }

    private void Idle()
    {
        moveSpeed = 0;
        SetAnimationState(STATE_IDLE);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        SetAnimationState(STATE_WALK);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        SetAnimationState(STATE_RUN);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        SetAnimationState(STATE_JUMP);
    }
}