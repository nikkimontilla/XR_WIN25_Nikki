using UnityEngine;

public class BoolCube : MonoBehaviour
{
    private Animator cubeAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cubeAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        cubeAnim.SetBool("isSpinning", true);
    }

    private void OnTriggerExit(Collider other)
    {
        cubeAnim.SetBool("isSpinning", false);
    }
}
