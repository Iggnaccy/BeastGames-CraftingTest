using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractionControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputActionReference _interactionAction;
    [SerializeField] private InputActionReference _toggleEnabledAction;

    [Header("Settings")]
    [SerializeField] private int _interactionLayer = 8;
    [SerializeField] private float _interactionDistance = 2f;
    [SerializeField] private float _interactionCheckRate = 0.1f;

    [Header("Events")]
    public UnityEvent<IInteractable> OnInteractableChanged;

    private float p_lastCheckTime = 0f;
    private IInteractable p_currentInteractable = null;

    private void Start()
    {
        _interactionAction.action.performed += Interact;
        _toggleEnabledAction.action.performed += context => enabled = !enabled;
    }

    private void OnDestroy()
    {
        _interactionAction.action.performed -= Interact;
        _toggleEnabledAction.action.performed -= context => enabled = !enabled;
    }

    private void Update()
    {
        if(Time.time - p_lastCheckTime > _interactionCheckRate && enabled)
        {
            p_lastCheckTime = Time.time;
            CheckForInteractable();
        }
    }

    private void CheckForInteractable()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _interactionDistance, 1 << _interactionLayer))
        {
            if(hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                p_currentInteractable?.OnEndHover();
                p_currentInteractable = interactable;
                OnInteractableChanged?.Invoke(p_currentInteractable);
                p_currentInteractable.OnStartHover();
            }
        }
        else
        {
            if(p_currentInteractable != null)
            {
                p_currentInteractable.OnEndHover();
                p_currentInteractable = null;
                OnInteractableChanged?.Invoke(null);
            }
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        p_currentInteractable?.OnInteract();
        p_currentInteractable = null; // Clear current interactable after interaction to prevent double interactions with objects that disappear on interaction
    }
}
