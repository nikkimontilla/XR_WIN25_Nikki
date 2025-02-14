using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GateTrigger : MonoBehaviour
{
    private Animator gateAnim;

    void Start()
    {
        // Just get the animator component
        gateAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check specifically for the XR Origin's camera offset
        if (other.gameObject.GetComponentInParent<XROrigin>() != null ||
            other.CompareTag("XROrigin"))
        {
            gateAnim.SetTrigger("Open");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check specifically for the XR Origin's camera offset
        if (other.gameObject.GetComponentInParent<XROrigin>() != null ||
            other.CompareTag("XROrigin"))
        {
           
            gateAnim.SetTrigger("Close");

        }
    }
}