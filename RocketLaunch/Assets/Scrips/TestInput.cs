using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class TestInput : MonoBehaviour
{
    [SerializeField] private InputAction inputAction;
    [SerializeField] private float testValue;

    public static event Action<float> OnTestInputPressed;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    private void Update()
    {
        if (inputAction.triggered)
        {
            OnTestInputPressed?.Invoke(testValue);
        }
    }
}
