using UnityEngine;

public class TriggerFall : MonoBehaviour
{
    private Animator humanoidAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        humanoidAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        humanoidAnim.SetTrigger("Fall");
    }

    private void OnTriggerExit(Collider other)
    {
        humanoidAnim.SetTrigger("Idle");
    }
}