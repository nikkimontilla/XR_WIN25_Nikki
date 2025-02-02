using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

/// <summary>
/// This scripts tracks when the match is used + disables the flame when the match is dropped
/// </summary>

[RequireComponent(typeof(XRGrabInteractable))]
public class StickIgniter : MonoBehaviour
{
    // References to required components
    public XRGrabInteractable m_grabInteractable;
    public ControllerData m_controllerData;
    XRBaseInteractor m_interactor;

    // Reference to BoxIgniter to stop fire
    private BoxIgniter m_boxIgniter;

    private void Awake()
    {
        // Get the XRGrabInteractable component
        m_grabInteractable = GetComponent<XRGrabInteractable>();
        // Find the BoxIgniter in the scene
        m_boxIgniter = FindFirstObjectByType<BoxIgniter>();
    }

    private void OnEnable()
    {
        if (m_grabInteractable == null)
            return;
        // Reads velocity of controller instead of interactables
        m_grabInteractable.selectEntered.AddListener(OnSelectEnter);
        m_grabInteractable.selectExited.AddListener(OnSelectExit);
    }

    private void OnSelectExit(SelectExitEventArgs arg0)
    {
        // Stop fire when dropped
        if (m_boxIgniter != null)
        {
            m_boxIgniter.StopAllEffects();
        }
        ResetControllerDataReader(arg0);
    }

    private void OnSelectEnter(SelectEnterEventArgs arg)
    {
        // Set the interactor that is grabbing the match
        m_interactor = arg.interactorObject as XRBaseInteractor;

        // Debug log to check if we're getting the interactor
        Debug.Log($"Interactor found: {m_interactor != null}");

        if (m_interactor != null)
        {
            // Try to find the controller GameObject (parent of the interactor)
            Transform controllerTransform = m_interactor.transform;
            while (controllerTransform != null && !controllerTransform.name.Contains("Controller"))
            {
                controllerTransform = controllerTransform.parent;
            }

            if (controllerTransform != null)
            {
                // Get ControllerDataReader from the controller GameObject
                m_controllerData = controllerTransform.GetComponent<ControllerData>();
                Debug.Log($"Searching on GameObject: {controllerTransform.name}");
                Debug.Log($"ControllerDataReader found: {m_controllerData != null}");
            }
            else
            {
                // Log error if we couldn't find the controller in the hierarchy
                Debug.LogError("Could not find Controller GameObject in hierarchy");
            }
        }
    }

    private void ResetControllerDataReader(SelectExitEventArgs arg0)
    {
        // Reset the controller data reader when the object is released
        m_controllerData = null;
    }

    private void OnDisable()
    {
        if (m_grabInteractable == null)
            return;
        // Remove the listeners when disabled to prevent memory leaks
        m_grabInteractable.selectEntered.RemoveListener(OnSelectEnter);
        m_grabInteractable.selectExited.RemoveListener(OnSelectExit);  
    }
}
