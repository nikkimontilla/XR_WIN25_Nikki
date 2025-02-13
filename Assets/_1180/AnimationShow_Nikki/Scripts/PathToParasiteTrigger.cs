using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PathToParasiteTrigger : MonoBehaviour
{
    [SerializeField] private Animator spotLightsAnim;
    [SerializeField] private Animator cubesAnim;
    [SerializeField] private string spotlightTrigger = "Stairs";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spotLightsAnim = GetComponent<Animator>();
        cubesAnim = GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("XROrigin"))  // Make sure to set the appropriate tag on your player
        {
            StartCoroutine(PlayAnimationsSequence());
        }
    }

    private IEnumerator PlayAnimationsSequence()
    {
        // Trigger spotlight animation
        spotLightsAnim.SetTrigger(spotlightTrigger);

        // Wait for the spotlight animation to complete
        // Get the length of the current animation clip
        AnimatorStateInfo stateInfo = spotLightsAnim.GetCurrentAnimatorStateInfo(0);
        float spotlightAnimationLength = stateInfo.length;

        // Wait for the animation to complete
        yield return new WaitForSeconds(spotlightAnimationLength);

        // After spotlight animation is complete, trigger cube animation
        cubesAnim.SetTrigger("Start");
    }
}
