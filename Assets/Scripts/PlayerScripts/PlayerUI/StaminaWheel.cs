using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaWheel : MonoBehaviour
{
    [SerializeField]
    PlayerController controller;
    Image wheel;
    // Start is called before the first frame update
    void Start()
    {
        wheel = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        wheel.fillAmount = controller.currentStamina / controller.maxStamina;
        if (controller.isExhausted)
        {
            wheel.color = Color.red;
        }
        else
        {
            wheel.color = Color.green;
        }
    }
}
