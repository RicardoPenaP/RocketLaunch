using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlatformHelixRotation : MonoBehaviour
{
    [Header("Level Platform Helix Rotation")]
    [SerializeField] private Transform helixTransform;
    [SerializeField] private float rotationSpeed;


    private void Update()
    {
        helixTransform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
           
    }
}
