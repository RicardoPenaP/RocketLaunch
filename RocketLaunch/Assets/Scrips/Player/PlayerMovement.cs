using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float engineForce = 10f;
    [SerializeField] private float rotationForce = 5f;

    private PlayerController playerController;
    private new Rigidbody rigidbody;
    

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        playerController.OnLifeRemove += PlayerController_OnLifeRemove;
    }

    private void Update()
    {
        UpdatePlayerMovement();
    }

    private void OnDestroy()
    {
        playerController.OnLifeRemove -= PlayerController_OnLifeRemove;
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

    private void PlayerController_OnLifeRemove(object sender, EventArgs e)
    {
        rigidbody.Sleep();
        transform.rotation = Quaternion.identity;
    }
}
