using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Users;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    PlayerInputReciever input;

    Vector3 cameraVector;

    
    // Start is called before the first frame update
    void Start()
    {
        cameraVector = new Vector3();
        //Debug.Log(input.inputUser.controlScheme.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        //if (input.inputUser.controlScheme.ToString() == "DualJoyCon()")
        //{
        //    cameraVector.Set(cameraVector.x + input.RightStickVector.x,
        //    cameraVector.y + input.RightStickVector.y, 0);
        //}
        //else if (input.inputUser.controlScheme.ToString() == "ProController(<SwitchProControllerHID>)")
        //{
            cameraVector.Set(cameraVector.x + input.RightStickVector.y,
            cameraVector.y + input.RightStickVector.x, 0);
        //}

    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(cameraVector);
    }
}
