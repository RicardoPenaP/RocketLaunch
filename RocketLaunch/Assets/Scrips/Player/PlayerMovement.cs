using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float engineForce = 10f;
    [SerializeField] private float rotationForce = 5f;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdatePlayerMovement();
    }


    private void UpdatePlayerMovement()
    {
        if (InputMananger.Instance.GetStartEngineInput())
        {
            StartEngine();
            
        }
        if (InputMananger.Instance.TryGetRotationDirectionInput(out float rotationDirection))
        {
            Rotate(rotationDirection);
        }
    }

    private void StartEngine()
    {
        Vector3 forceDirection = transform.up * engineForce;
        rigidbody.AddForce(forceDirection,ForceMode.Acceleration);
    }

    private void Rotate(float rotationDirection)
    {
        Vector3 rotationVector = Vector3.forward * rotationDirection * rotationForce * Time.deltaTime;
        transform.Rotate(rotationVector);
        //Use it if you want to restrinct the rotation only when the ship is in movement
        //if (Mathf.Abs(rigidbody.velocity.y) > Mathf.Epsilon)
        //{
           
        //}  
    }
}
