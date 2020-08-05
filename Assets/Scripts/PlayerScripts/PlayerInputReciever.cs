using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Switch;



public class PlayerInputReciever : MonoBehaviour
{
    PlayerInput playerInput;
    public InputUser inputUser
    {
        get; private set;
    }
    public InputControlScheme inputControlScheme;
    InputDevice JoyConLeft, JoyConRight;
    Gamepad ProGamepad;
    
    public Vector2 LeftStickVector
    {
        get;
        private set;
    }
    public Vector2 RightStickVector
    {
        get;
        private set;
    }
    public bool jumpPressed
    {
        get;
        private set;
    }
    public bool sprintPressed
    {
        get;
        private set;
    }
    public bool attackPressed
    {
        get;
        private set;
    }
    public bool focusPressed
    {
        get;
        private set;
    }

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        inputUser = playerInput.user;
        inputUser.UnpairDevices();
        Debug.Log(inputUser.controlScheme.ToString());
        foreach (var js in Joystick.all)
        {
            InputUser.PerformPairingWithDevice(js, inputUser);
        }
        foreach (var gp in Gamepad.all)
        {
            InputUser.PerformPairingWithDevice(gp, inputUser);
            ProGamepad = gp;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(ProGamepad != Gamepad.current)
        {
            ProGamepad = Gamepad.current;
        }
    }
    public void OnLeftStick(InputAction.CallbackContext context)
    {
        if(inputUser.controlScheme.ToString() == "DualJoyCon()")
        {           
            if (context.control.device == JoyConLeft)
            {
                Vector2 vec = context.ReadValue<Vector2>();
                LeftStickVector = new Vector2(vec.y, -vec.x);
            }
        }
        else if(context.control.device == ProGamepad)
        {
            LeftStickVector = context.ReadValue<Vector2>();
        }
        
    }
    public void OnRightStick(InputAction.CallbackContext context)
    {
        if (inputUser.controlScheme.ToString() == "DualJoyCon()")
        {
            if (context.control.device == JoyConRight)
            {
                Vector2 vec = context.ReadValue<Vector2>();
                RightStickVector = new Vector2(vec.y, -vec.x);
            }
        }
        else if (context.control.device == ProGamepad)
        {
            RightStickVector = context.ReadValue<Vector2>();
        }
        
    }
    public void OnBindLeft(InputAction.CallbackContext context)
    {
        if(inputUser.controlScheme.ToString() == "DualJoyCon()")
        {
            JoyConLeft = context.control.device;
        }
        // This currently doesn't work
        else if (inputUser.controlScheme.ToString() == "ProController(<SwitchProControllerHID>)")
        {
            JoyConLeft = context.control.device;
            inputUser.ActivateControlScheme("DualJoyCon");
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (inputUser.controlScheme.ToString() == "DualJoyCon()")
        {
            if(context.control.device == JoyConRight)
            {
                attackPressed = context.ReadValueAsButton();
            }
        }
        else if (inputUser.controlScheme.ToString() == "ProController(<SwitchProControllerHID>)")
        {
            attackPressed = context.ReadValueAsButton();
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (inputUser.controlScheme.ToString() == "DualJoyCon()")
        {
            if (context.control.device == JoyConRight)
            {
                jumpPressed = context.ReadValueAsButton();
            }
        }
        else if (inputUser.controlScheme.ToString() == "ProController(<SwitchProControllerHID>)")
        {
            //Debug.LogAssertion("Jump Pressed from Pro");
            jumpPressed = context.ReadValueAsButton();
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (inputUser.controlScheme.ToString() == "DualJoyCon()")
        {
            if (context.control.device == JoyConRight)
            {
                sprintPressed = context.ReadValueAsButton();
            }
        }
        else if (inputUser.controlScheme.ToString() == "ProController(<SwitchProControllerHID>)")
        {
            sprintPressed = context.ReadValueAsButton();
        }
    }
    public void OnBindRight(InputAction.CallbackContext context)
    {
        if (inputUser.controlScheme.ToString() == "DualJoyCon()")
        {
            JoyConRight = context.control.device;
            if(context.control.device == Gamepad.current)
            {
                inputUser.ActivateControlScheme("ProController");
                ProGamepad = Gamepad.current;
            }
        }
        else if (inputUser.controlScheme.ToString() == "ProController(<SwitchProControllerHID>)")
        {
            foreach (var gp in Gamepad.all)
            {
                if(gp != context.control.device)
                {
                    inputUser.UnpairDevice(gp);
                }
            }
            ProGamepad = Gamepad.current;
            if (context.control.device == Joystick.current)
            {
                JoyConRight = context.control.device;
                inputUser.ActivateControlScheme("DualJoyCon");
            }
        }
    }

}
