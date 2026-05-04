using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    Vector2 move;
    [SerializeField] GameObject camHolder;
    [SerializeField] float speed = 5f;
    [SerializeField] float maxForce = 10f;

    [Header("Jump Config")]
    [SerializeField] float jumpForce;
    [SerializeField] GameObject groundCheck;
    [SerializeField] bool isGrounded;
    [SerializeField] float groundDetectRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        camHolder = GameObject.Find("CameraHolder");
        groundCheck = GameObject.Find("GroundCheck");
    }
    void Start() 
    {
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
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
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
        targetVelocity *= speed;
        
        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);
        Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void Jump()
    {
        Vector3 jumpForces = rb.linearVelocity;
        if (isGrounded) jumpForces.y = jumpForce;
        rb.linearVelocity = jumpForces;
    }
    #region Inputs

    public void OnMove (InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnJump (InputAction.CallbackContext context)
    {
        Jump();
    }

    public void OnCrouch (InputAction.CallbackContext context)
    {

    }
    #endregion
}
