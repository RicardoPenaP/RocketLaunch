using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotator : MonoBehaviour
{
    [Header("Obstacle Rotator")]
    [SerializeField] private bool rotationActive = false;
    [SerializeField] private bool randomRotationAxis = false;
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] private float rotationSpeed = 20;

    private void OnValidate()
    {
        if (randomRotationAxis)
        {
            rotationAxis = Vector3.zero;
        }
    }

    private void Start()
    {
        if (randomRotationAxis)
        {
            rotationAxis = Random.insideUnitSphere;
        }
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (!rotationActive)
        {
            return;
        }
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
