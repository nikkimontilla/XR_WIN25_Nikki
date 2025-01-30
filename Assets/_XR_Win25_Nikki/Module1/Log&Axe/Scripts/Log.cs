using System;
using UnityEngine;

/// <summary>
/// The purpose of this script is to check whether its being hit by a blage and react accordingly
/// 3 possible behaviours
/// </summary>

[RequireComponent(typeof(Collider))]

public class Log : MonoBehaviour
{
    [SerializeField] GameObject logOne;
    [SerializeField] GameObject logTwo;

    [SerializeField] float m_splitThreshold = 1f;
    [SerializeField] float m_stickThreshold = 0.5f;

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

        Split(blade);
    }
    

    private void Split(Blade blade)
    {
        // Split Log  when speed is greater than threshold

        /*Rigidbody rgOne = logOne.GetComponent<Rigidbody>();
        Rigidbody rgTwo = logTwo.GetComponent<Rigidbody>();

        rgOne.useGravity = true;
        rgOne.isKinematic = false;

        rgTwo.useGravity = true;
        rgTwo.isKinematic = false;
        */

        float bladeHitSpeed = blade.m_controllerDataReader.Velocity.magnitude;
        Debug.Log($"Blade Hit Speed: {bladeHitSpeed} / Split: {m_splitThreshold} / Stick: {m_stickThreshold}");

        if (bladeHitSpeed > m_splitThreshold)
        {
            EnablePhysics(logOne);
            EnablePhysics(logTwo);

            // Disable collision so we can only split once 
            m_collider.enabled = false;
        }

        // Axe gets stuck if speed is slow
        else if (bladeHitSpeed > m_stickThreshold)
        {
            blade.Drop();
            blade.DisablePhysics();
        }
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
