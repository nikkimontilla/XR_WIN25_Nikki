using UnityEngine;

public class AppleRottenState : AppleBaseState
{
    public override void EnterState(AppleStateManager apple)
    {
        
    }

    public override void UpdateState(AppleStateManager apple)
    {

    }

    public override void OnCollisionEnter(AppleStateManager apple, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<PlayerController>().detractHealth();

            apple.SwitchState(apple.ChewedState);
        }
    }
}
