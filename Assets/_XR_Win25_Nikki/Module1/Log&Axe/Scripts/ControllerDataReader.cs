using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script is in charge of reading data from the touch controller 
/// </summary>
/// 

public class ControllerDataReader : MonoBehaviour
{
    //Calling Action from the XR Input Action in the Inspector
    [SerializeField] InputActionProperty velocityProperty;

    // get function reads
    // private set function limits it to this script
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
