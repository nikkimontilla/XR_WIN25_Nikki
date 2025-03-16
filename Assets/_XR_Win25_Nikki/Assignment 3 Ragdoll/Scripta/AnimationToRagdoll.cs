using System;
using UnityEngine;
using System.Collections;

public class AnimationToRagdoll : MonoBehaviour
{
    [SerializeField] Collider myCollider;
    [SerializeField] float respawnTime = 30f;
    Rigidbody[] rigidbodies;
    bool bIsRagdoll = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!bIsRagdoll && collision.gameObject.tag == "Projectile")
        {
            ToggleRagdoll(false);
            StartCoroutine(GetBackUp());
        }
    }

    private void ToggleRagdoll(bool bisAnimating)
    {
        bIsRagdoll = !bisAnimating;

        myCollider.enabled = bisAnimating;
        foreach (Rigidbody ragdollBone in rigidbodies)
        {
            ragdollBone.isKinematic = bisAnimating;

            GetComponent<Animator>().enabled = bisAnimating;
            if (bisAnimating)
            {
                RandomAnimation();
            }
        }
    }

    private IEnumerator GetBackUp()
    {
        yield return new WaitForSeconds(respawnTime);
        ToggleRagdoll(true);
    }

    private void RandomAnimation()
    {
        int randomNum = UnityEngine.Random.Range(0, 2);
        Debug.Log(randomNum);


        Animator animator = GetComponent<Animator>();
        if (randomNum == 0)
        {
            animator.SetTrigger("Walk");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
