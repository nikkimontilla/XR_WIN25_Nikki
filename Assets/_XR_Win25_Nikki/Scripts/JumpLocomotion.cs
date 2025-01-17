using System;
using UnityEngine;

// Reference for keyboard input
using UnityEngine.InputSystem;

public class JumpLocomotion : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpInput;

    // Set default height of jump to 2f but [SerielizeField] allows for adjustmests in the inspector
    [SerializeField] private float jumpHeight = 2f;

    // Declare charactercontroller variable
    private CharacterController characterController;

    //Declare velocity variable which has 3 values so it uses Vector3
    private Vector3 velocity;

    // Declare jump force variable
    public float jumpForce = 10f;

    // Declare gravity variable 
    public float gravity = -9.81f;

    // Start method
    private void Start()
    {
        // Reference to the CharacterController component in XR Origin Rig
        characterController = GetComponentInParent<CharacterController>();
    }

    private void OnEnable()
    {
        // Read the "performed" event of the jump action from the Input Asset
        // Calls the jump method whenever the jump input is activated
        jumpInput.action.Enable();
        jumpInput.action.performed += Jump;
    }

    private void OnDisable()
    {
        //Opposite of OnEnable method
        jumpInput.action.Disable();
        jumpInput.action.performed -= Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        // Disables multiple Jump
        if (characterController.isGrounded)
        {
            ApplyJumpForce();
            velocity.y = jumpForce;
        }
    }

    private void Update()
    {
        // Apply gravity to each frame
        if (characterController != null)
        {
            
            if (!characterController.isGrounded)
            {
                velocity.y += gravity * Time.deltaTime;
            }
            
            else if (velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // Only move in the Y direction
            Vector3 movement = new Vector3(0, velocity.y, 0) * Time.deltaTime;
            characterController.Move(movement);
        }
    }

    private void ApplyJumpForce()
    {
        // Calculate the jump velocity needed to reach the desired jump height
        jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics.gravity.y));
        Debug.Log($"Jump velocity needed for {jumpHeight}m height: {jumpForce} m/s");
    }
}
