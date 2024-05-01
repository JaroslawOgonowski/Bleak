using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ExamplePlayerController : MonoBehaviour
{
    public Color materialColor;
    private Rigidbody m_rigidbody;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public string jumpButton = "Jump";

    public float moveSpeed = 5f; // Stała prędkość poruszania się do przodu
    public float rotationSpeed = 1f; // Prędkość obrotu obiektu

    private float inputHorizontal;
    private float inputVertical;
    private Animator animator;
    private bool isMoving = false; // Flaga wskazująca, czy postać się porusza
    private BoxCollider boxCollider;
    void Awake()
    {
        animator = GetComponent<Animator>();
        //GetComponent<Renderer>().material.color = materialColor;
        m_rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();


    }

    void Update()
    {
        inputHorizontal = SimpleInput.GetAxis(horizontalAxis);
        inputVertical = SimpleInput.GetAxis(verticalAxis);

        transform.Rotate(0f, inputHorizontal * rotationSpeed, 0f, Space.World);

        // Sprawdź czy postać porusza się do przodu
        isMoving = Mathf.Abs(inputVertical) > 0.1f;
        Debug.Log(IsGrounded());

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            m_rigidbody.AddForce(0f, 2f, 0f, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }



        if (SimpleInput.GetButtonDown(jumpButton) && IsGrounded())
        {
            m_rigidbody.AddForce(0f, 5f, 0f, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        // Modyfikuj pozycję obiektu w każdej klatce zgodnie z wejściem pionowym
        Vector3 movement = transform.forward * inputVertical * moveSpeed * Time.fixedDeltaTime;
        m_rigidbody.MovePosition(m_rigidbody.position + movement);

        // Określ czy postać porusza się do przodu czy do tyłu
        float forwardSpeed = Vector3.Dot(movement.normalized, transform.forward);

        // Ustaw animację w zależności od kierunku poruszania się postaci
        if (forwardSpeed > 0.1f && IsGrounded())
        {
            animator.SetBool("RunForward", true);
            animator.SetBool("RunBackward", false);
        }
        else if (forwardSpeed < -0.1f && IsGrounded())
        {
            animator.SetBool("RunForward", false);
            animator.SetBool("RunBackward", true);
        }
        else
        {
            animator.SetBool("RunForward", false);
            animator.SetBool("RunBackward", false);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            m_rigidbody.AddForce(collision.contacts[0].normal * 10f, ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        // Pozycja centrum i rozmiar collidera
        Vector3 center = boxCollider.bounds.center;
        Vector3 size = boxCollider.bounds.size;

        // Promień rzucony w dół do sprawdzenia kolizji z ziemią
        float rayLength = size.y * 0.51f; // 51% wysokości collidera

        // Wykrycie kolizji z warstwą "Ground" w promieniu
        bool isGrounded = Physics.CheckBox(center, size * 0.5f, Quaternion.identity, LayerMask.GetMask("Ground")) ||
                          Physics.Raycast(center, Vector3.down, rayLength, LayerMask.GetMask("Ground"));

        return isGrounded;
    }
}
