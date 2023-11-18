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
    private PlayerLandingController playerLandingController;
    private EngineController engineController;
    private new Rigidbody rigidbody;

    private bool canMove = true; 

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerLandingController = GetComponent<PlayerLandingController>();
        engineController = GetComponent<EngineController>();
    }

    private void Start()
    {
        if (playerController)
        {
            playerController.OnLifeRemove += PlayerController_OnLifeRemove;
        }

        if (playerLandingController)
        {
            playerLandingController.OnPreLandingStart += PlayerLandingController_OnPreLandingStart;
        }

    }

    private void Update()
    {
        UpdatePlayerMovement();
    }

    private void OnDestroy()
    {
        if (playerController)
        {
            playerController.OnLifeRemove -= PlayerController_OnLifeRemove;
        }

        if (playerLandingController)
        {
            playerLandingController.OnPreLandingStart -= PlayerLandingController_OnPreLandingStart;
        }
    }

    private void UpdatePlayerMovement()
    {
        if (InputMananger.Instance.GetMoveUpwardsInputIsInProgress())
        {
            if (CanMoveCheck())
            {
                MoveUpwards();
                OnStartMovingUpwards?.Invoke(this, EventArgs.Empty);
            }           
        }

        if (InputMananger.Instance.GetMoveUpwardsInputWasReleasedThisFrame())
        {
            OnStopMovingUpwards?.Invoke(this,EventArgs.Empty);
        }

        if (InputMananger.Instance.TryGetRotationDirectionInput(out float rotationDirectionRaw))
        {
            if (CanMoveCheck())
            {
                Rotate(rotationDirectionRaw);
                ManageRotationVFX(true, rotationDirectionRaw);
            }           
        }

        if (InputMananger.Instance.GetRotationDirectionInputWasReleasedThisFrame())
        {
            ManageRotationVFX(false);
        }
    }

    private bool CanMoveCheck()
    {
        return canMove && !engineController.IsOverHeated && engineController.HasFuel;
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

    private void PlayerLandingController_OnPreLandingStart(object sender, EventArgs e)
    {
        canMove = false;
    }
}
