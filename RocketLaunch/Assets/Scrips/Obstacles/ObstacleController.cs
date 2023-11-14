using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleController : MonoBehaviour
{
    public enum MovementType { Static, Linear, Ocillate, Expansion}

    [Header("Obstacle Controller")]
    [Header("General Settings")]
    [SerializeField, Min(0f)] private float changeDirectionTime = 0f;

    [Header("Linear Settings")]
    [SerializeField] private MovementType movementType;
    [SerializeField] private float movementSpeed;
    
    [SerializeField] private Vector3 movementDirection;
    [SerializeField,Range(-1f,1f)] private float startingDirection = 1f;

    [Header("Ocilliate Settings")]
    [SerializeField] private Vector3 maxPositionDistance;    
    [SerializeField] private AnimationCurve xAnimationCurve;
    [SerializeField] private AnimationCurve yAnimationCurve;
    [SerializeField] private AnimationCurve zAnimationCurve;
    [SerializeField] private bool linearAceleration = true;

    public event EventHandler OnTimerStop;

    private float timer;
    private Vector3 maxPosition;
    private Vector3 startingPosition;   

    private void Awake()
    {        
        OnTimerStop += ChangeMovementDirection;
        ChangeMovementDirection(this, EventArgs.Empty);

        timer = 0;
        startingPosition = transform.position;
        maxPosition = startingPosition + maxPositionDistance;
        
    }


    private void OnValidate()
    {
        switch (movementType)
        {
            case MovementType.Static:
                movementSpeed = 0f;
                movementDirection = Vector3.zero;
                maxPositionDistance = Vector3.zero;
                xAnimationCurve = new AnimationCurve();
                yAnimationCurve = new AnimationCurve();
                zAnimationCurve = new AnimationCurve();
                break;
            case MovementType.Linear:
                maxPositionDistance = Vector3.zero;
                xAnimationCurve = new AnimationCurve();
                yAnimationCurve = new AnimationCurve();
                zAnimationCurve = new AnimationCurve();
                break;
            case MovementType.Ocillate:
                movementSpeed = 0f;
                movementDirection = Vector3.zero;
                break;
            case MovementType.Expansion:

                break;
            default:
                break;
        }
        if (movementType == MovementType.Static)
        {
           
           
        }

        if (movementType != MovementType.Ocillate)
        {
           
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
        float movementProgress = timer / changeDirectionTime;
        if (!linearAceleration)
        {
            startingPosition = transform.position;
        }
        Vector3 targetPosition = new Vector3();
        targetPosition.x = Mathf.Lerp(startingPosition.x, maxPosition.x, xAnimationCurve.Evaluate(movementProgress));
        targetPosition.y = Mathf.Lerp(startingPosition.y, maxPosition.y, yAnimationCurve.Evaluate(movementProgress));
        targetPosition.z = Mathf.Lerp(startingPosition.z, maxPosition.z, zAnimationCurve.Evaluate(movementProgress));
        
        transform.position = targetPosition;
    }
}
