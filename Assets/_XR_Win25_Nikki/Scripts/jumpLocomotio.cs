using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class jumpLocomotion : MonoBehaviour
{
    [SerializeField] InputActionReference jumpInput;

    private void OnEnable()
    {
        jumpInput.action.performed += jump;

    }

    private void jump(InputAction.CallbackContext context)
    {
        Debug.Log("JUMP IS PERFORMING");
    }
}
