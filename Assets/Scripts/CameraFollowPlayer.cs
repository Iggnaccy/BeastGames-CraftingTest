using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;
    [Header("Settings")]
    [SerializeField] private float smoothSpeed = 0.125f;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, smoothSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothSpeed);
    }
}
