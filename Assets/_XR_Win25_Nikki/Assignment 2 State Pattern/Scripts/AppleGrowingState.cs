using UnityEngine;

public class AppleGrowingState : AppleBaseState
{
    Vector3 startingAppleSize = new Vector3(0f, 0f, 0f);
    Vector3 growApppleScalar = new Vector3(0.025f, 0.025f, 0.025f);
    float targetScale = 0.31057f;

    public override void EnterState(AppleStateManager apple)
    {
        apple.transform.localScale = startingAppleSize; // Set apple starting size when first instantiated
    }

    public override void UpdateState(AppleStateManager apple)
    {
        // If the scale of the apple is less than target scale, increase size
        if (apple.transform.localScale.x < targetScale)
        {
            apple.transform.localScale += growApppleScalar * Time.deltaTime;

            // Clamp the scale to not exceed target
            if (apple.transform.localScale.x > targetScale)
            {
                apple.transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }
        // Switch to whole state once apple is grown
        else
        {
            apple.transform.localScale = new Vector3(targetScale, targetScale, targetScale); // Ensure exact scale
            apple.SwitchState(apple.WholeState);
        }
    }

    public override void OnCollisionEnter(AppleStateManager apple, Collision collision)
    {

    }
}