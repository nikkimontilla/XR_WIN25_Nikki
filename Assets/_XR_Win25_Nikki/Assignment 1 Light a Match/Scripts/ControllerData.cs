using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script is in charge of reading (velocity) data from the controller 
/// </summary>
/// 

public class ControllerData : MonoBehaviour
{
    //Calling Action from the XR Input Action in the Inspector
    [SerializeField] InputActionProperty velocityProperty;

    // Vertor3.zero is the default value
    public Vector3 Velocity { get; private set; } = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        // Reference the velocity values x, y, z
        Velocity = velocityProperty.action.ReadValue<Vector3>();
        Debug.Log("Velocity:" + Velocity);
    }
}
