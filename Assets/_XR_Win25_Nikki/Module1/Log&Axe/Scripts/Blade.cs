using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRGrabInteractable))]

public class Blade : MonoBehaviour
{
    public XRGrabInteractable m_grabInteractable;
    public ControllerDataReader m_controllerDataReader;
    XRBaseInteractor m_interactor;

    private void Awake()
    {
        m_grabInteractable = GetComponent<XRGrabInteractable>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

        // Set the controllerDataReader
        m_controllerDataReader = m_interactor.gameObject.GetComponent<ControllerDataReader>();
    }

    private void ResetControllerDataReader(SelectExitEventArgs arg)
    {
        m_controllerDataReader = null;
    }

    private void OnDiasable()
    {
        if (m_grabInteractable == null)
            return;

        m_grabInteractable.selectEntered.RemoveListener(OnSelectEnter);
        m_grabInteractable.selectExited.RemoveListener(ResetControllerDataReader);
    }
}
