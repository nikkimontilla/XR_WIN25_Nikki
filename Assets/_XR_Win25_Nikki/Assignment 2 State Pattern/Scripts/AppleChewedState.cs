using UnityEngine;

public class AppleChewedState : AppleBaseState
{
    float destroyCountDown = 2.0f;
    public override void EnterState(AppleStateManager apple)
    {
        // Spawn chewed apple prefab and destroy original
        if (apple.chewedApplePrefab != null)
        {
            // Instantiate the chewed apple at the same position and rotation
            GameObject chewedApple = Object.Instantiate(apple.chewedApplePrefab, apple.transform.position, apple.transform.rotation);

            // Destroy the original apple
            Object.Destroy(apple.gameObject);
        }
    }

    public override void UpdateState(AppleStateManager apple)
    {
        if (destroyCountDown > 0)
        {
            destroyCountDown -= Time.deltaTime;
        }
        else
        {
            Object.Destroy(apple.gameObject);
        }
    }

    public override void OnCollisionEnter(AppleStateManager apple, Collision collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            EnterState(apple);
        }
    }
}
