using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using System.Collections;
public class ExamplePlayerController : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public string jumpButton = "Jump";
    public float moveSpeed = 5f;
    public float rotationSpeed = 1f;
    public float jumpForce = 1f;
    private float inputHorizontal;
    private float inputVertical;
    private Animator animator;
    public bool isMoving = false;
    private BoxCollider boxCollider;
    bool closingJumpState = false;
    private float verticalVelocity;
    private bool falling;
    private bool jumpUp;
    [SerializeField] private float forwardSpeed;
    private bool usedFallingAnimation = false;
    public bool runJump = false;
    void Awake()
    {
        animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if(IsGrounded() == true && closingJumpState == false)
        {
            StartCoroutine(closeJumpAnimation());
        }
        if (IsGrounded() == false && closingJumpState == false && animator.GetBool("Jump_up") == false && jumpUp == true)
        {
           if (isMoving == false)
            {
                animator.SetBool("Jump_up", true);
            }
            else
            {
                if (runJump == false)
                {
                    animator.SetBool("RunJump", true);
                } else
                {
                    animator.SetBool("RunJump", false);
                }
            }
        }
        if (IsGrounded() == false && closingJumpState == false && falling == true && usedFallingAnimation == false)
        {
            if (isMoving == false)
            {
                usedFallingAnimation = true;
                animator.SetBool("Jump_up", false);
                animator.SetBool("Falling", true);
            }
        }
        inputHorizontal = SimpleInput.GetAxis(horizontalAxis);
        inputVertical = SimpleInput.GetAxis(verticalAxis);

        transform.Rotate(0f, inputHorizontal * rotationSpeed, 0f, Space.World);
        isMoving = Mathf.Abs(inputVertical) > 0.1f;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            m_rigidbody.AddForce(0f, jumpForce, 0f, ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded() == true)
        {
            m_rigidbody.AddForce(transform.forward * 3f, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.S) && IsGrounded() == true)
        {
            m_rigidbody.AddForce(-transform.forward * 3f, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.A) && IsGrounded() == true)
        {
            transform.Rotate(0f, -8f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.D) && IsGrounded() == true)
        {
            transform.Rotate(0f, 8f, 0f);
        }

        if (SimpleInput.GetButtonDown(jumpButton) && IsGrounded() == true)
        {
            m_rigidbody.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
        }
        if (animator.GetBool("RunJump") == true && runJump == false)
        {
             runJump = true;
        }
        if (runJump == true && IsGrounded() == true)
        {
            runJump = false;
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
        // Modyfikuj pozycję obiektu w każdej klatce zgodnie z wejściem pionowym
        Vector3 movement = transform.forward * inputVertical * moveSpeed * Time.fixedDeltaTime;
        m_rigidbody.MovePosition(m_rigidbody.position + movement);

        // Określ czy postać porusza się do przodu czy do tyłu
        forwardSpeed = Vector3.Dot(movement.normalized, transform.forward);

        // Ustaw animację w zależności od kierunku poruszania się postaci
        if (forwardSpeed > 0.1f)
        {
            isMoving = true;

            if(IsGrounded() == true)
            {
                animator.SetBool("RunForward", true);
                animator.SetBool("RunBackward", false);
                animator.SetBool("RunJump", false);
            }
        }
        else if (forwardSpeed < -0.1f && IsGrounded() == true)
        {
            isMoving = true;
            if (IsGrounded() == true)
            {
                animator.SetBool("RunForward", false);
                animator.SetBool("RunBackward", true);
                animator.SetBool("RunJump", false);
            }
        }
        else
        {
            isMoving = false;
            if (IsGrounded() == true)
            {
                animator.SetBool("RunForward", false);
                animator.SetBool("RunBackward", false);
            }
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
        // Pozycja centrum i rozmiar collidera
        Vector3 center = boxCollider.bounds.center;
        Vector3 size = boxCollider.bounds.size;

        // Promień rzucony w dół do sprawdzenia kolizji z ziemią
        float rayLength = size.y * 0.6f; // 51% wysokości collidera

        // Wykrycie kolizji z warstwą "Ground" w promieniu
        bool isGrounded = Physics.CheckBox(center, size * 0.5f, Quaternion.identity, LayerMask.GetMask("Ground")) ||
                          Physics.Raycast(center, Vector3.down, rayLength, LayerMask.GetMask("Ground"));

        return isGrounded;
    }
}
