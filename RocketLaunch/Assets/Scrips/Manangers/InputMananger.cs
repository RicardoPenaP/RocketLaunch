using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMananger : MonoBehaviour
{
    public static InputMananger Instance { get; private set; }

    private PlayerInputActions inputActions;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public bool TryGetRotationDirectionInput(out float rotationDirection)
    {
        if (inputActions.Player.Rotation.IsInProgress())
        {
            rotationDirection = inputActions.Player.Rotation.ReadValue<float>();
            return true;
        }
        else
        {
            rotationDirection = 0f;
            return false;
        }
    }

    public bool GetMoveUpwardsInputIsInProgress()
    {        
        return inputActions.Player.StartEnginge.IsInProgress();
    }

    public bool GetMoveUpwardsInputWasReleasedThisFrame()
    {
        return inputActions.Player.StartEnginge.WasReleasedThisFrame();
    }




}
