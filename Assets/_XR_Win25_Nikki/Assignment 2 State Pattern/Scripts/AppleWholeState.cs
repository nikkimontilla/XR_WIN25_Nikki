using UnityEngine;

public class AppleWholeState : AppleBaseState
{
    float rottenCountDown = 5.0f;

    public override void EnterState(AppleStateManager apple)
    {
        apple.GetComponent<Rigidbody>().useGravity = true; // Apple falls when grown
    }

    public override void UpdateState(AppleStateManager apple)
    {
        if (rottenCountDown >= 0)
        {
            rottenCountDown -= Time.deltaTime;
        }
        else
        {
            // Spawn rotten apple at current apple's position and rotation
            Vector3 currentPosition = apple.transform.position;
            Quaternion currentRotation = apple.transform.rotation;
            GameObject rottenApple = GameObject.Instantiate(apple.rottenApplePrefab, currentPosition, currentRotation);

            // Copy over any necessary components or values from the original apple
            if (rottenApple.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.linearVelocity = apple.GetComponent<Rigidbody>().linearVelocity;
            }

            // Destroy the original apple
            GameObject.Destroy(apple.gameObject);
        }

    }

    public override void OnCollisionEnter(AppleStateManager apple, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<PlayerController>().addHealth();

            apple.SwitchState(apple.ChewedState);
        }
    }
}
