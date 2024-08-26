using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorModeControl : MonoBehaviour
{
    [SerializeField] private InputActionReference _cursorModeToggleAction;

    private void Start()
    {
        _cursorModeToggleAction.action.performed += ToggleCursorMode;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ToggleCursorMode(InputAction.CallbackContext obj)
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (Cursor.lockState == CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
