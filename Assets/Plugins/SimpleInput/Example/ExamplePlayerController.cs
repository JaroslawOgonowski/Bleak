using UnityEngine;

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

    void Awake()
    {
        animator = GetComponent<Animator>();
        //GetComponent<Renderer>().material.color = materialColor;
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputHorizontal = SimpleInput.GetAxis(horizontalAxis);
        inputVertical = SimpleInput.GetAxis(verticalAxis);

        transform.Rotate(0f, inputHorizontal * rotationSpeed, 0f, Space.World);

        // Sprawdź czy postać porusza się do przodu
        isMoving = Mathf.Abs(inputVertical) > 0.1f;

        if (SimpleInput.GetButtonDown(jumpButton) && IsGrounded())
            m_rigidbody.AddForce(0f, 10f, 0f, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        // Modyfikuj pozycję obiektu w każdej klatce zgodnie z wejściem pionowym
        Vector3 movement = transform.forward * inputVertical * moveSpeed * Time.fixedDeltaTime;
        m_rigidbody.MovePosition(m_rigidbody.position + movement);

        // Określ czy postać porusza się do przodu czy do tyłu
        float forwardSpeed = Vector3.Dot(movement.normalized, transform.forward);

        // Ustaw animację w zależności od kierunku poruszania się postaci
        if (forwardSpeed > 0.1f)
        {
            animator.SetBool("RunForward", true);
            animator.SetBool("RunBackward", false);
        }
        else if (forwardSpeed < -0.1f)
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
        return Physics.Raycast(transform.position, Vector3.down, 1.75f);
    }
}
