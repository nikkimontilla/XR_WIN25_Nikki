using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRGrabInteractable))]
public class Blade : MonoBehaviour
{
    // References to required components
    public XRGrabInteractable m_grabInteractable;
    public ControllerDataReader m_controllerDataReader;
    XRBaseInteractor m_interactor;

    private void Awake()
    {
        // Get the XRGrabInteractable component
        m_grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (m_grabInteractable == null)
            return;

        // Reads velocity of controller instead of interactables
        m_grabInteractable.selectEntered.AddListener(OnSelectEnter);
        m_grabInteractable.selectExited.AddListener(ResetControllerDataReader);
    }

    private void OnSelectEnter(SelectEnterEventArgs arg)
    {
        // Set the interactor that is grabbing the axe
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
                m_controllerDataReader = controllerTransform.GetComponent<ControllerDataReader>();
                Debug.Log($"Searching on GameObject: {controllerTransform.name}");
                Debug.Log($"ControllerDataReader found: {m_controllerDataReader != null}");
            }
            else
            {
                // Log error if we couldn't find the controller in the hierarchy
                Debug.LogError("Could not find Controller GameObject in hierarchy");
            }

            EnablePhysics();
        }
    }

    private void ResetControllerDataReader(SelectExitEventArgs arg0)
    {
        // Reset the controller data reader when the object is released
        m_controllerDataReader = null;
    }

    private void OnDisable()
    {
        if (m_grabInteractable == null)
            return;

        // Remove the listeners when disabled to prevent memory leaks
        m_grabInteractable.selectEntered.RemoveListener(OnSelectEnter);
        m_grabInteractable.selectExited.RemoveListener(ResetControllerDataReader);
    }

    public void Drop()
    {
        IXRSelectInteractable grabinteractable = m_grabInteractable;
        m_interactor.interactionManager.CancelInteractableSelection(grabinteractable);
    }

    public void EnablePhysics()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.None;
    }

    public void DisablePhysics()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
