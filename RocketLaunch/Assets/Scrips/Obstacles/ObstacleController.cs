using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleController : MonoBehaviour
{
    public enum MovementType { Static, Linear, Ocillate, Expansion}

    [Header("Obstacle Controller")]

    [Header("General Settings")]
    [SerializeField] private MovementType movementType;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float changeDirectionTime;
    [SerializeField] private Vector3 movementDirection;
    [SerializeField,Range(-1f,1f)] private float startingDirection = 1f;

    [Header("Ocilliate Settings")]
    [SerializeField] private float maxYDiference = 0;


    public event EventHandler OnTimerStop;

    private float timer;
    private float maxY;

    private void Awake()
    {        
        OnTimerStop += ChangeMovementDirection;
        ChangeMovementDirection(this, EventArgs.Empty);

        timer = 0;
        maxY = transform.position.y + maxYDiference;
    }


    private void OnValidate()
    {
        if (movementType == MovementType.Static)
        {
            movementSpeed = 0f;
            movementDirection = Vector3.zero;
            maxYDiference = 0f;
        }

        startingDirection = startingDirection > Mathf.Epsilon ? 1 : -1;
       
        if (movementDirection.magnitude > 1f)
        {
            movementDirection = movementDirection.normalized;
        }
       
    }

    private void Update()
    {
        UpdateMovement();
        UpdateTimer();
    }

    private void OnDestroy()
    {
        OnTimerStop -= ChangeMovementDirection;        
    }

    private void UpdateMovement()
    {
        if (movementType == MovementType.Static)
        {
            return;
        }

        switch (movementType)
        {            
            case MovementType.Linear:
                LinearMovement();
                break;
            case MovementType.Ocillate:
                OcilliatingMovement();
                break;
            case MovementType.Expansion:
                break; 
        }
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        if (timer >= changeDirectionTime)
        {
            ResetTimer();
            OnTimerStop?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ResetTimer()
    {
        timer = 0f;
    }

    private void ChangeMovementDirection(object sender, EventArgs e)
    {
        movementDirection *= startingDirection;
        startingDirection *= -1;
    }

    private void LinearMovement()
    {
        transform.position += movementDirection * movementSpeed * Time.deltaTime;
    }

    private void OcilliatingMovement()
    {
        transform.position += movementDirection * movementSpeed * Time.deltaTime;
    }
}
