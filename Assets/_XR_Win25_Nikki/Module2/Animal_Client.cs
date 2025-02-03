using UnityEngine;

public class Animal_Client : MonoBehaviour
{
    [SerializeField] Animal[] animals;

    void Start()
    {
        foreach (var animal in animals)
        {
            animal.MakeSound();
        }
    }
}
