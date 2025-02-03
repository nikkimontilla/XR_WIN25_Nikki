using UnityEngine;

public class Dog : Animal
{
    private void Start()
    {
        Name = "Dog";
    }
    public override void MakeSound()
    {
        Debug.Log("Dog barks");
    }
}
