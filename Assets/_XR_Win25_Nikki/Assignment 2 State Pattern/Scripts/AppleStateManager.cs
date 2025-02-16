using UnityEngine;

// This script serves as the CONTEXT of the apple state machine
public class AppleStateManager : MonoBehaviour
{
    [SerializeField] public GameObject chewedApplePrefab; // Reference to the chewed apple prefab
    [SerializeField] public GameObject rottenApplePrefab; // Reference to rotten apple prefab

    AppleBaseState currentState; // Holds a reference to the active state in a state machine

    // Instantiate a new instance of each of the 4 states of the apple
    public AppleGrowingState GrowingState = new AppleGrowingState();
    public AppleWholeState WholeState = new AppleWholeState();
    public AppleRottenState RottenState = new AppleRottenState();
    public AppleChewedState ChewedState = new AppleChewedState();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = GrowingState; // Indicate the starting state for the state machine

        currentState.EnterState(this); // Referencing the apple's context

    }

    public void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this); // Call logic in Update State from current state every frame
    }

    public void SwitchState(AppleBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
