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

    public bool TryGetSideMovementInput(out Vector2 sideMovementVector)
    {
        if (inputActions.Player.SideMovement.triggered)
        {
            sideMovementVector = inputActions.Player.SideMovement.ReadValue<Vector2>();
            return true;
        }
        else
        {
            sideMovementVector = Vector2.zero;
            return false;
        }
    }

    public bool GetStartEngineInput()
    {        
        return inputActions.Player.StartEnginge.IsInProgress();
    }


}
