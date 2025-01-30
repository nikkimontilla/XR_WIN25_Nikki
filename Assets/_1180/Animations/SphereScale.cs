using UnityEngine;

public class SphereScale : MonoBehaviour
{
    private Animator sphereAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sphereAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        sphereAnim.SetInteger("Scale Level", 10);
    }

    private void OnTriggerExit(Collider other)
    {
        sphereAnim.SetInteger("Scale Level", 1);
    }

}
