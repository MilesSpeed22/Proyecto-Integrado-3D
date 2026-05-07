using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{

    Rigidbody rb;
    Animator anim;
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

    [SerializeField] bool isCrouching;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundCheck = GameObject.Find("GroundCheck");
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
        Vector3 jumpForces = rb.linearVelocity;
        if (isGrounded) jumpForces.y = jumpForce;
        rb.linearVelocity = jumpForces;
    }

    IEnumerator CrouchAction()
    {
        isCrouching = true;

        anim.SetBool("isCrouching", true);

        yield return new WaitForSeconds(1f);

        anim.SetBool("isCrouching", false);

        isCrouching = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        float input = context.ReadValue<Vector2>().x;

        if (input > 0)
        {
            currentLane++;
        }
        else if (input < 0) 
        {
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
        if (context.performed && !isCrouching)
        {
            StartCoroutine(CrouchAction());
        }
    }
}
