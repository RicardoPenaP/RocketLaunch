using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputMananger : MonoBehaviour
{
    public static InputMananger Instance { get; private set; }

    public event Action OnPauseInputTriggered;

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

    private void Update()
    {
        InputUpdate();
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

    public bool GetRotationDirectionInputWasReleasedThisFrame()
    {              
        return inputActions.Player.Rotation.WasReleasedThisFrame();
    }

    public bool GetMoveUpwardsInputIsInProgress()
    {        
        return inputActions.Player.MoveUpwards.IsInProgress();
    }

    public bool GetMoveUpwardsInputWasReleasedThisFrame()
    {
        return inputActions.Player.MoveUpwards.WasReleasedThisFrame();
    }

    public bool GetInteractInputWasTriggered()
    {
        return inputActions.Player.Interact.triggered;
    }

    private void InputUpdate()
    {
        if (inputActions.Player.Pause.triggered)
        {
            OnPauseInputTriggered?.Invoke();
        }
    }
}
