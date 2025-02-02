using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// The purpose of this script is to check whether the match box is being striked and to react accordingly
/// with particle effects for fire and sparks based on strike velocity
/// </summary>

[RequireComponent(typeof(Collider))]
public class BoxIgniter : MonoBehaviour
{
    [SerializeField] GameObject rightBoxIgniter;
    [SerializeField] GameObject leftBoxIgniter;
    [SerializeField] float m_fireThreshold = 0.5f;
    [SerializeField] float m_sparkThreshold = 0.1f;

    // Particle System references
    [Header("Particle Systems")]
    [SerializeField] ParticleSystem m_fireParticles;
    [SerializeField] ParticleSystem m_sparkParticles;

    // Reference collider
    Collider m_collider = null;

    void Awake()
    {
        m_collider = GetComponent<Collider>();
        m_collider.isTrigger = true;
        DisableParticleSystems();
    }

    void Start()
    {
        DisableParticleSystems();
        Debug.Log("BoxIgniter initialized. Waiting for stick strike...");
    }

    private void DisableParticleSystems()
    {
        if (m_fireParticles != null)
        {
            m_fireParticles.Stop();
            m_fireParticles.gameObject.SetActive(false);
        }
        if (m_sparkParticles != null)
        {
            m_sparkParticles.Stop();
            m_sparkParticles.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision detected with: {other.gameObject.name}, Tag: {other.tag}");

        if (!other.CompareTag("Stick Igniter"))
        {
            Debug.Log("Object does not have 'Stick Igniter' tag");
            return;
        }

        // Get StickIgniter component
        StickIgniter stickIgniter = other.GetComponentInParent<StickIgniter>();
        if (stickIgniter == null)
        {
            Debug.LogWarning("Object has 'Stick Igniter' tag but no StickIgniter component!");
            return;
        }

        // Is controller data is available?
        if (stickIgniter.m_controllerData == null)
        {
            Debug.LogWarning("StickIgniter found but no controller data available. Is the stick being held?");
            return;
        }

        // controller data found!
        Debug.Log("Valid strike detected, checking velocity...");
        Fire(stickIgniter);
    }

    private void Fire(StickIgniter stickIgniter)
    {
        float stickIgniterStrikeSpeed = stickIgniter.m_controllerData.Velocity.magnitude;
        Debug.Log($"Strike Speed: {stickIgniterStrikeSpeed:F2} / Fire Threshold: {m_fireThreshold} / Spark Threshold: {m_sparkThreshold}");

        if (stickIgniterStrikeSpeed > m_fireThreshold)
        {
            Debug.Log("Speed exceeds fire threshold - Playing fire effect!");
            PlayFireEffect();
        }
        else if (stickIgniterStrikeSpeed > m_sparkThreshold)
        {
            Debug.Log("Speed exceeds spark threshold - Playing spark effect!");
            PlaySparkEffect();
        }
        else
        {
            Debug.Log("Strike speed too low - No effect played");
        }
    }

    private void PlayFireEffect()
    {
        if (m_fireParticles == null)
        {
            Debug.LogWarning("Fire particle system not added in inspector");
            return;
        }

        m_fireParticles.gameObject.SetActive(true);
        m_fireParticles.Play();

        if (m_sparkParticles != null && m_sparkParticles.isPlaying)
        {
            m_sparkParticles.Stop();
            m_sparkParticles.gameObject.SetActive(false);
        }
    }

    private void PlaySparkEffect()
    {
        if (m_sparkParticles == null)
        {
            Debug.LogWarning("Spark particle system not added in inspector");
            return;
        }
        m_sparkParticles.gameObject.SetActive(true);
        m_sparkParticles.Play();
        StartCoroutine(StopSparkEffect());
    }

    private IEnumerator StopSparkEffect()
    {
        yield return new WaitForSeconds(3f);
        m_sparkParticles.Stop();
        m_sparkParticles.gameObject.SetActive(false);
    }

    public void StopAllEffects()
    {
        DisableParticleSystems();
    }
}
