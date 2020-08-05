using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerInputReciever input;

    [SerializeField]
    Animator animator;

    [SerializeField]
    PlayerCamera cam;

    Rigidbody rb;


    Vector3 movementVector;
    float turnSmoothTime = 0.12f;
    float turnSmoothVelocity;

    float movementSpeed = 2.9f;
    float walkSpeed = 2.9f;
    float runSpeed = 5.5f;

    public float currentStamina
    {
        get; private set;
    }
    public float maxStamina
    {
        get; private set;
    }
    public bool isExhausted
    {
        get; private set;
    }

    Vector3 playerVerticalVelocity;
    float jumpSpeed = 8.0f;
    float gravity = 9.81f;
    public bool grounded = false;




    // Start is called before the first frame update
    void Start()
    {
        currentStamina = 10f;
        maxStamina = 60f;
        isExhausted = false;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
            movementVector.Set(input.LeftStickVector.x, 0, input.LeftStickVector.y);
        if (movementVector.magnitude > 0.1f)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void FixedUpdate()
    {
        Jump();
        Vertical();
        Sprint();
        MovePlayer();
        Attack();
    }

    void Sprint()
    {
        if (isExhausted)
        {
            animator.SetBool("isRunning", false);
            if (currentStamina != maxStamina)
            {
                currentStamina = Mathf.Min(currentStamina += 0.1f, maxStamina);
                movementSpeed = walkSpeed;
            }
            else
            {
                isExhausted = false;
            }
        }
        else
        {
            if (currentStamina <= 0)
            {
                isExhausted = true;
            }
            if (input.sprintPressed && currentStamina > 0 && !isExhausted)
            {
                animator.SetBool("isRunning", true);
                currentStamina -= 0.25f;
                movementSpeed = runSpeed;
            }
            else if (!input.sprintPressed && currentStamina > 0 && !isExhausted)
            {
                animator.SetBool("isRunning", false);
                currentStamina = Mathf.Min(currentStamina += 0.2f, maxStamina);
                movementSpeed = walkSpeed;
            }
        }
    }
    void Vertical()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, 1f, Vector3.down,  out hit, 0f, 10))
        {
            grounded = true;
            
        }
        else
        {
            grounded = false;
        }
        animator.SetBool("isGrounded", grounded);
        animator.SetFloat("yVel", rb.velocity.y);
    }
    void MovePlayer()
    {
        if (movementVector.magnitude >= 0.1f && !animator.GetCurrentAnimatorStateInfo(0).IsName("BroadSlash") )
        {
            float targetAngle = Mathf.Atan2(input.LeftStickVector.x, input.LeftStickVector.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;    
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.velocity = moveDirection.normalized * movementSpeed;
        }
    }
    void Jump()
    {
        if(grounded && input.jumpPressed)
        {
            playerVerticalVelocity.y = jumpSpeed;
        }
    }

    void Attack()
    {

    }


}
