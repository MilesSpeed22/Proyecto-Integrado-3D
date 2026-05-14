using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{

    Rigidbody rb;
    CapsuleCollider playerCollider;
    float originalHeight;
    Vector3 originalCenter;
    [SerializeField] float rollDuration = 1f;
    [SerializeField] Animator anim;
    Vector2 move;
    float lookRotation;

    [Header("Movement & Look Stats")]
    public float speed;

    [Header("Jumping & GroundCheck Configuration")]
    public float jumpForce;

    [SerializeField] GameObject groundCheck;
    [SerializeField] bool isGrounded;
    [SerializeField] float groundDetectRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;


    [Header("Movement beetwen lanes")]
    [SerializeField] float laneDistance = 2f;
    [SerializeField] float laneChangeSpeed = 10f;
    int currentLane = 0;

    [SerializeField] bool isRolling;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = GameObject.Find("GroundCheck");
        playerCollider = GetComponent<CapsuleCollider>();

        originalCenter = playerCollider.center;
        originalHeight = playerCollider.height;
    }
    void Start()
    {

    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDetectRadius, groundLayer);
    }

    private void FixedUpdate()
    {
        Movement();
        
    }

    void Movement()
    {
        Vector3 targetPosition = new Vector3(currentLane * laneDistance, transform.position.y, transform.position.z);

        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, laneChangeSpeed * Time.fixedDeltaTime);

        rb.MovePosition(newPosition);
    }

    void Jump()
    {
        if (!isGrounded) return;
        Vector3 jumpForces = rb.linearVelocity;
        jumpForces.y = jumpForce;
        rb.linearVelocity = jumpForces;
        anim.SetTrigger("Jump");
    }

    IEnumerator RollAction()
    {
        isRolling = true;

        anim.SetTrigger("Roll");

        playerCollider.height = originalHeight / 2f;

        playerCollider.center = new Vector3(originalCenter.x, originalCenter.y, originalCenter.z);

        yield return new WaitForSeconds(rollDuration);

        playerCollider.height = originalHeight;
        playerCollider.center = originalCenter;

        isRolling = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        float input = context.ReadValue<Vector2>().x;

        if (input > 0)
        {
            anim.SetTrigger("Right");
            currentLane++;
        }
        else if (input < 0) 
        {
            anim.SetTrigger("Left");
            currentLane--;
        }

        currentLane = Mathf.Clamp(currentLane, -1, 1);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed && !isRolling)
        {
            StartCoroutine(RollAction());
        }
    }
}
