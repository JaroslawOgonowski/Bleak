using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using System.Collections;
public class ExamplePlayerController : MonoBehaviour
{
    public static ExamplePlayerController Instance;
    public Color materialColor;
    private Rigidbody m_rigidbody;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public string jumpButton = "Jump";

    public float moveSpeed = 5f; // Stała prędkość poruszania się do przodu
    public float rotationSpeed = 1f; // Prędkość obrotu obiektu
    public float jumpForce = 1f;
    private float inputHorizontal;
    private float inputVertical;
    public Animator animator;
    private bool isMoving = false;
    private BoxCollider boxCollider;
    bool closingJumpState = false;
    private float verticalVelocity;
    private bool falling;
    private bool jumpUp;
    private bool isGathering;
    private bool isMining;
    public bool gatheringMove = false;

    [SerializeField] private float forwardSpeed;
    private bool usedFallingAnimation = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        animator = GetComponent<Animator>();
        //GetComponent<Renderer>().material.color = materialColor;
        m_rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if(IsGrounded() == true && closingJumpState == false && gatheringMove == false)
        {
            StartCoroutine(closeJumpAnimation());
        }
        if (IsGrounded() == false && closingJumpState == false && animator.GetBool("Jump_up") == false && jumpUp == true && gatheringMove == false)
        {
            animator.SetBool("Jump_up", true);
        }
        if (IsGrounded() == false && closingJumpState == false && falling == true && usedFallingAnimation == false && gatheringMove == false)
        {
            usedFallingAnimation = true;
            animator.SetBool("Jump_up", false);
            animator.SetBool("Falling", true);
        }
        inputHorizontal = SimpleInput.GetAxis(horizontalAxis);
        inputVertical = SimpleInput.GetAxis(verticalAxis);

        if (gatheringMove == false)
        {
            transform.Rotate(0f, inputHorizontal * rotationSpeed, 0f, Space.World);
        }

        isMoving = Mathf.Abs(inputVertical) > 0.1f;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true || SimpleInput.GetButtonDown(jumpButton) && IsGrounded() == true && gatheringMove == false)
        {
            m_rigidbody.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded() == true && gatheringMove == false)
        {
            m_rigidbody.AddForce(transform.forward * 3f, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.S) && IsGrounded() == true && gatheringMove == false)
        {
            m_rigidbody.AddForce(-transform.forward * 3f, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.A) && IsGrounded() == true && gatheringMove == false)
        {
            transform.Rotate(0f, -8f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.D) && IsGrounded() == true && gatheringMove == false)
        {
            transform.Rotate(0f, 8f, 0f);
        }
    }

    private IEnumerator closeJumpAnimation()
    {
        closingJumpState = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Falling", false);
        animator.SetBool("Jump_up", false);
        usedFallingAnimation = false;
        closingJumpState = false;
    }

    void FixedUpdate()
    {
        Vector3 movement = transform.forward * inputVertical * moveSpeed * Time.fixedDeltaTime;
        m_rigidbody.MovePosition(m_rigidbody.position + movement);

        forwardSpeed = Vector3.Dot(movement.normalized, transform.forward);

        if (forwardSpeed > 0.1f && IsGrounded() == true && gatheringMove == false)
        {
            animator.SetBool("RunForward", true);
            animator.SetBool("RunBackward", false);
        }
        else if (forwardSpeed < -0.1f && IsGrounded() == true && gatheringMove == false)
        {   
            animator.SetBool("RunForward", false);
            animator.SetBool("RunBackward", true);
        }
        else if (gatheringMove == false) 
        { 
            animator.SetBool("RunForward", false);
            animator.SetBool("RunBackward", false);
        }

        verticalVelocity = m_rigidbody.velocity.y;
        if (verticalVelocity > 0.1f)
        {
            jumpUp = true;
            falling = false;
        }
        else if (verticalVelocity < -0.1f)
        {
            falling = true;
            jumpUp = false;
        }
        else
        {
            jumpUp = false;
            falling = false;
        }
    }

    bool IsGrounded()
    {
        Vector3 center = boxCollider.bounds.center;
        Vector3 size = boxCollider.bounds.size;

        float rayLength = size.y * 0.51f;

        bool isGrounded = Physics.CheckBox(center, size * 0.5f, Quaternion.identity, LayerMask.GetMask("Ground")) ||
                          Physics.Raycast(center, Vector3.down, rayLength, LayerMask.GetMask("Ground"));

        return isGrounded;
    }
}
