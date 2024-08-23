using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputActionReference _moveAction, _rotateAction;
    [SerializeField] private Transform _cameraPitch;

    [Header("Settings")]
    [SerializeField] private bool _allowSideMovement = true;
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _minimumCameraPitch = -80f, _maximumCameraPitch = 80f;

    [Header("Public")]
    public bool movementEnabled = true;

    private float p_currentCameraPitch = 0f;
    private Rigidbody p_rigidbody;

    void Start()
    {
        p_currentCameraPitch = _cameraPitch.localEulerAngles.x;
        p_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        p_rigidbody.angularVelocity = Vector3.zero;
        if (movementEnabled)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector2 movementVector = _moveAction.action.ReadValue<Vector2>();

        Vector3 movementForward = transform.forward * movementVector.y;
        Vector3 movementRight = _allowSideMovement ? transform.right * movementVector.x : Vector3.zero;

        Vector3 movement = (movementForward + movementRight).normalized * _movementSpeed;

        Vector2 rotationVector = _rotateAction.action.ReadValue<Vector2>();

        transform.Translate(movement * Time.deltaTime, Space.World);
        transform.Rotate(0, rotationVector.x * _rotationSpeed * Time.deltaTime, 0);

        p_currentCameraPitch -= rotationVector.y * _rotationSpeed * Time.deltaTime;
        p_currentCameraPitch = Mathf.Clamp(p_currentCameraPitch, _minimumCameraPitch, _maximumCameraPitch);

        _cameraPitch.localEulerAngles = new Vector3(p_currentCameraPitch, 0, 0);
    }
}
