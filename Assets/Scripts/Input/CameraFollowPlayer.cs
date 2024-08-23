using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _target;
    [Header("Settings")]
    [SerializeField] private float _smoothSpeed = 0.125f;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, _smoothSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, _target.rotation, _smoothSpeed);
    }
}
