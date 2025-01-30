using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    private Animator doorAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        doorAnim.SetTrigger("Open");
    }

    private void OnTriggerExit(Collider other)
    {
        doorAnim.SetTrigger("Close");
    }
}
