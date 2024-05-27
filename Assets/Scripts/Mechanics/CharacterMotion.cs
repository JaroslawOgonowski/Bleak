using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class CharacterMotion : MonoBehaviour
{
    public float jumpHeight = 4f;
    public float gravity = 20f;
    public float stepDown = 0.3f;
    public float airControl = 2.5f;
    public float jumpDamp = 0.5f;
    public float groundSpeed = 1.2f;
    public float pushPower = 2;

    Animator animator;
    CharacterController characterController;
    Vector2 input;

    Vector3 rootMotion;
    Vector3 velocity;
    bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("inputX", input.x);
        animator.SetFloat("inputY", input.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void OnAnimatorMove()
    {
        rootMotion += animator.deltaPosition;
    }
    private void FixedUpdate()
    {
        if (isJumping)
        {
            UpdateInAir();
        }
        else
        {
            UpdateOnGround();
        }
    }

    private void UpdateOnGround()
    {
        Vector3 stepForwardAmount = rootMotion * groundSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDown;

        characterController.Move(stepForwardAmount + stepDownAmount);
        rootMotion = Vector3.zero;

        if (!characterController.isGrounded)
        {
            SetInAir(0);
        }
    }

    private void UpdateInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacment = velocity * Time.fixedDeltaTime;
        displacment += CalculateAirControl();
        characterController.Move(displacment);
        isJumping = !characterController.isGrounded;
        rootMotion = Vector3.zero;
        animator.SetBool("isJumping", isJumping);
    }


    Vector3 CalculateAirControl()
    {
        return ((transform.forward * input.y)+(transform.right * input.x)) * (airControl/100);
    }
    void Jump()
    {
        if(!isJumping)
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            SetInAir(jumpVelocity);
        }
    }

    private void SetInAir(float jumpVelocity)
    {
        isJumping = true;
        velocity = animator.velocity * jumpDamp * groundSpeed;
        velocity.y = jumpVelocity;
        animator.SetBool("isJumping", true);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}
