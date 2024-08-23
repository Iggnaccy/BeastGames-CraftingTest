using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingItemSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 10f;

    private void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
