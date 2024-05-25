using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class CharacterMotion : MonoBehaviour
{
    public float jumpHeight;
    public float gravity;
    public float stepDown;
    public float airControl;

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
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

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
            velocity.y -= gravity*Time.fixedDeltaTime;
            Vector3 displacment = velocity * Time.fixedDeltaTime;
            displacment += CalculateAirControl();
            characterController.Move(displacment);
            isJumping =! characterController.isGrounded;
            rootMotion = Vector3.zero;
        } else
        {
            characterController.Move(rootMotion + Vector3.down * stepDown);
            rootMotion = Vector3.zero;

            if(!characterController.isGrounded)
            {
                isJumping = true;
                velocity = animator.velocity;
                velocity.y = 0f;

            }
        }
    }

    Vector3 CalculateAirControl()
    {
        return ((transform.forward * input.y)+(transform.right * input.x)) * (airControl/100);
    }
    void Jump()
    {
        if(!isJumping)
        {
            isJumping = true;
            velocity = animator.velocity;
            velocity.y = Mathf.Sqrt(2 * gravity * jumpHeight);
        }
    }
}
