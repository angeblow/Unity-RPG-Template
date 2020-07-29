﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;


public class PlayerController : MonoBehaviour
{

    PlayerInput playerInput;
    InputUser user;

    CharacterController controller;
    [SerializeField]
    Animator animations;
    [SerializeField]
    GameObject CamAnchor;

    Vector2 stickVector;
    Vector3 movementVector;

    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    Vector2 rStickVector;
    Vector3 cameraVector;

    float movementSpeed = 2.9f;
    InputDevice jcLeft;
    InputDevice jcRight;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        user = playerInput.user;

        controller = GetComponent<CharacterController>();

        stickVector = new Vector2();
        movementVector = new Vector3();

        rStickVector = new Vector2();
        cameraVector = new Vector3();


        InputUser.PerformPairingWithDevice(InputSystem.devices[2], user);
        InputUser.PerformPairingWithDevice(InputSystem.devices[3], user);
        jcLeft = user.pairedDevices[0];
        jcRight = user.pairedDevices[1];
    }

    // Update is called once per frame
    void Update()
    {      
        if (stickVector.magnitude >= 0.1f)
        {
            animations.SetBool("isMoving", true);
        }
        else
        {
            animations.SetBool("isMoving", false);
        }
        movementVector.Set(-stickVector.y, 0, stickVector.x);
        cameraVector.Set(cameraVector.x + rStickVector.x, cameraVector.y + rStickVector.y, 0);
    }

    void FixedUpdate()
    {
        if(movementVector.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(stickVector.y, -stickVector.x) * Mathf.Rad2Deg + CamAnchor.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
        }
        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down * 9.81f * Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        CamAnchor.transform.rotation = Quaternion.Euler(cameraVector);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.control.device == jcLeft)
        {
            stickVector = context.ReadValue<Vector2>();
        }
    }

    public void OnCamera(InputAction.CallbackContext context)
    {
        if (context.control.device == jcRight)
        {
            rStickVector = context.ReadValue<Vector2>();
        }
    }
}