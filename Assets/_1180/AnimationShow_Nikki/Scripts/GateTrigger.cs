using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    private Animator gateAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gateAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        gateAnim.SetTrigger("Open");
    }

    private void OnTriggerExit(Collider other)
    {
        gateAnim.SetTrigger("Close");
    }
}
