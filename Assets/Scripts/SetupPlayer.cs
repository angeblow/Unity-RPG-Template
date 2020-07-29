using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class SetupPlayer : MonoBehaviour
{

    PlayerInput playerInput;
    InputUser user;

    Rigidbody rb;
    [SerializeField]
    GameObject CamAnchor;

    Vector2 stickVector;
    Vector3 movementVector;

    float heading;
    float oldHeading;

    Vector2 rStickVector;
    Vector3 cameraVector;

    float movementSpeed = 5.1f;
    InputDevice jcLeft;
    InputDevice jcRight;


    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        user = playerInput.user;

        rb = GetComponent<Rigidbody>();

        stickVector = new Vector2();
        movementVector = new Vector3();

        rStickVector = new Vector2();
        cameraVector = new Vector3();

        heading = 0f;
        oldHeading = 0f;

        InputUser.PerformPairingWithDevice(InputSystem.devices[2], user);
        InputUser.PerformPairingWithDevice(InputSystem.devices[3], user);
        /// TODO: 
        jcLeft = user.pairedDevices[0];
        jcRight = user.pairedDevices[1];
    }

    // Update is called once per frame
    void Update()
    {
        movementVector.Set(stickVector.x, 0, stickVector.y);
        //cameraVector.Set(cameraVector.x + rStickVector.x, cameraVector.y + rStickVector.y, 0);
    }

    void FixedUpdate()
    {
        rb.velocity =  movementVector * movementSpeed;
    }
    void LateUpdate()
    {
        //CamAnchor.transform.rotation = Quaternion.Euler(cameraVector);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if ( context.control.device == jcLeft)
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

    public void JoinLeftJoycon(InputAction.CallbackContext context)
    {
        Debug.Log("Left activated");
        foreach (var jc in InputSystem.devices)
        {
            if (jc != null && jc.IsPressed(15))
            {
                Debug.Log("15 pressed");
                InputUser.PerformPairingWithDevice(jc, user);
            }
        }
    }
}
