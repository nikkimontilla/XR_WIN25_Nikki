using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Collider))]

public class Log : MonoBehaviour
{
    [SerializeField] GameObject logOne;
    [SerializeField] GameObject logTwo;

    // Reference collider
    Collider m_collider = null;

    void Awake()
    {
        m_collider = GetComponent<Collider>();
        m_collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_collider != null)
            Debug.Log("collision happening");

        Blade blade = null;
        if (other.CompareTag("Blade"))
        {
            blade = other.GetComponentInParent<Blade>();
        }

        // Quit this func earlier if blade script is not found
        if (blade == null)
            return;

        if (blade.m_controllerDataReader == null)
            return;

        Split();
    }

    private void Split()
    {
        /*Rigidbody rgOne = logOne.GetComponent<Rigidbody>();
        Rigidbody rgTwo = logTwo.GetComponent<Rigidbody>();

        rgOne.useGravity = true;
        rgOne.isKinematic = false;

        rgTwo.useGravity = true;
        rgTwo.isKinematic = false;
        */

        EnablePhysics(logOne);
        EnablePhysics(logTwo);
    }

    // This func refactors Split - more effecient
    private void EnablePhysics(GameObject log)
    {
        log.transform.parent = null;

        Rigidbody rg = log.GetComponent<Rigidbody>();
        rg.useGravity = true;
        rg.isKinematic = false;
    }

}
