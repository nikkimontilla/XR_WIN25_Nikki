using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector3 movement;

    private void Update()
    {
        // Get keyboard input
        if (Keyboard.current != null)
        {
            float horizontalInput = 0;
            float verticalInput = 0;

            // WASD input
            if (Keyboard.current.wKey.isPressed) verticalInput += 1;
            if (Keyboard.current.sKey.isPressed) verticalInput -= 1;
            if (Keyboard.current.dKey.isPressed) horizontalInput += 1;
            if (Keyboard.current.aKey.isPressed) horizontalInput -= 1;

            // Create movement vector
            movement = new Vector3(horizontalInput, 0, verticalInput);
            movement = movement.normalized * moveSpeed * Time.deltaTime;

            // Move the character
            transform.Translate(movement);
        }
    }
}