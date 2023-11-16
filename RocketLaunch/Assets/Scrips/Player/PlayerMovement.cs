using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public enum RotationDirection { Rigth, Left}
    [Header("Player Movement")]
    [SerializeField] private float engineForce = 10f;
    [SerializeField] private float rotationForce = 5f;

    public event EventHandler OnStartMovingUpwards;
    public event EventHandler OnStopMovingUpwards;
    public event EventHandler<RotationDirection> OnStartRotating;
    public event EventHandler<RotationDirection> OnStopRotating;

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
        if (InputMananger.Instance.GetMoveUpwardsInputIsInProgress())
        {
            MoveUpwards();
            OnStartMovingUpwards?.Invoke(this, EventArgs.Empty);
        }

        if (InputMananger.Instance.GetMoveUpwardsInputWasReleasedThisFrame())
        {
            OnStopMovingUpwards?.Invoke(this,EventArgs.Empty);
        }

        if (InputMananger.Instance.TryGetRotationDirectionInput(out float rotationDirectionRaw))
        {
            Rotate(rotationDirectionRaw);
            ManageRotationVFX(true, rotationDirectionRaw);
        }

        if (InputMananger.Instance.GetRotationDirectionInputWasReleasedThisFrame())
        {
            ManageRotationVFX(false);
        }
    }

    private void MoveUpwards()
    {
        Vector3 forceDirection = transform.up * engineForce * Time.deltaTime;
        rigidbody.AddForce(forceDirection,ForceMode.Acceleration);
    }

    private void Rotate(float rotationDirection)
    {
        Vector3 rotationVector = Vector3.forward * rotationDirection * rotationForce * Time.deltaTime;
        transform.Rotate(rotationVector);         
    }

    private void ManageRotationVFX(bool startPlaying, float rotationDirectionRaw = 0)
    {
        RotationDirection rotationDirection = rotationDirectionRaw > Mathf.Epsilon ? RotationDirection.Rigth : RotationDirection.Left;       
        if (startPlaying)
        {
            OnStartRotating?.Invoke(this, rotationDirection);            
        }
        else
        {
            OnStopRotating?.Invoke(this, RotationDirection.Rigth);
            OnStopRotating?.Invoke(this, RotationDirection.Left);
        }
        
    }

    private void PlayerController_OnLifeRemove(object sender, EventArgs e)
    {
        rigidbody.Sleep();
        transform.rotation = Quaternion.identity;
    }
}
