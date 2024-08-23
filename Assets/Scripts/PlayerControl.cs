using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction, rotateAction;
    [SerializeField] private Transform cameraPitch;

    [SerializeField] private bool allowSideMovement = true;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float minimumCameraPitch = -80f, maximumCameraPitch = 80f;

    private float currentCameraPitch = 0f;

    void Start()
    {
        currentCameraPitch = cameraPitch.localEulerAngles.x;
    }

    private void Update()
    {
        Vector2 movementVector = moveAction.action.ReadValue<Vector2>();

        Vector3 movementForward = transform.forward * movementVector.y;
        Vector3 movementRight = allowSideMovement ? transform.right * movementVector.x : Vector3.zero;

        Vector3 movement = (movementForward + movementRight).normalized * movementSpeed;

        Vector2 rotationVector = rotateAction.action.ReadValue<Vector2>();

        transform.Translate(movement * Time.deltaTime, Space.World);
        transform.Rotate(0, rotationVector.x * rotationSpeed * Time.deltaTime, 0);

        currentCameraPitch -= rotationVector.y * rotationSpeed * Time.deltaTime;
        currentCameraPitch = Mathf.Clamp(currentCameraPitch, minimumCameraPitch, maximumCameraPitch);

        cameraPitch.localEulerAngles = new Vector3(currentCameraPitch, 0, 0);
    }
}
