using System;
using UnityEngine;

/// <summary>
/// The purpose of this script is to check whether its being hit by a blage and react accordingly
/// 3 possible behaviours
/// </summary>

[RequireComponent(typeof(Collider))]

public class BoxIgniter : MonoBehaviour
{
    [SerializeField] GameObject rightBoxIgniter;
    [SerializeField] GameObject leftBoxIgniter;

    [SerializeField] float m_fireThreshold = 1f;
    [SerializeField] float m_sparkThreshold = 0.5f;

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

        StickIgniter stickIgniter = null;

        if (other.CompareTag("Stick Igniter"))
        {
            stickIgniter = other.GetComponentInParent<StickIgniter>();
        }

        // Quit this func earlier if blade script is not found
        if (stickIgniter == null)
            return;

        if (stickIgniter.m_controllerData == null)
            return;

        Fire(stickIgniter);
    }


    private void Fire(StickIgniter stickIgniter)
    {
        float stickIgniterStrikeSpeed = stickIgniter.m_controllerData.Velocity.magnitude;
        Debug.Log($"Stick Igniter Strike Speed: {stickIgniterStrikeSpeed} / Fire: {m_fireThreshold} / Spark: {m_sparkThreshold}");

        if (stickIgniterStrikeSpeed > m_fireThreshold)
        {
            //add fire animation
        }

        // Axe gets stuck if speed is slow
        else if (stickIgniterStrikeSpeed > m_sparkThreshold)
        {
            //add spark animation
        }
    }
}