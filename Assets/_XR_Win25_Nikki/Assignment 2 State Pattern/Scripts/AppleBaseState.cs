using UnityEngine;

// This script serves as the ABSTRACT STATE

public abstract class AppleBaseState
{
    // Create 3 new methods with the CONTEXT Apple State Manager

    public abstract void EnterState(AppleStateManager apple);

    public abstract void UpdateState(AppleStateManager apple);

    public abstract void OnCollisionEnter(AppleStateManager apple, Collision collision);

}
