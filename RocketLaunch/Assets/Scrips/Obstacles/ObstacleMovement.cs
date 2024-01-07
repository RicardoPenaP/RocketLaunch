using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [Header("Obstacle Movement")]
    [SerializeField] private bool canMove = false;
    [SerializeField] private bool showGizmos = false;
    [SerializeField] private Vector3 movementDistance;
    [SerializeField,Min(0f)] private float movementSpeed = 10f;


    private Vector3 startPosition;
    private Vector3 maxPosition;
    private Vector3 minPosition;
    private Vector3 movementDirection;

    private float maxDistance;
    //private float changeDirectionCooldownTime = 0.1f;

    //private bool canChangeDirection = true;

    private void OnDrawGizmosSelected()
    {
        if (!showGizmos)
        {
            return;
        }

        Vector3 position;

        if (Application.isPlaying)
        {
            position = startPosition;
        }
        else
        {
            position = transform.position;
        }

        Vector3 startPoint = position;
        Vector3 finishPoint = position;
        startPoint.y -= movementDistance.y / 2;
        finishPoint.y += movementDistance.y / 2;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPoint, finishPoint);

        startPoint = position;
        startPoint.x -= movementDistance.x / 2;
        finishPoint = position;
        finishPoint.x += movementDistance.x / 2;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPoint, finishPoint);

        startPoint = position;
        startPoint.z -= movementDistance.z / 2;
        finishPoint = position;
        finishPoint.z += movementDistance.z / 2;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(startPoint, finishPoint);
    }

    private void OnValidate()
    {
        if (movementDistance.x < 0)
        {
            movementDistance.x = 0;
        }

        if (movementDistance.y < 0)
        {
            movementDistance.y = 0;
        }

        if (movementDistance.z < 0)
        {
            movementDistance.z = 0;
        }
    }

    private void Awake()
    {
        startPosition = transform.position;
        maxPosition = startPosition + (movementDistance / 2);
        minPosition = startPosition - (movementDistance / 2);       

        movementDirection = (maxPosition - transform.position).normalized;

        maxDistance = Vector3.Distance(startPosition, maxPosition);
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        transform.position += movementDirection * movementSpeed * Time.fixedDeltaTime;

        if (Vector3.Distance(transform.position, startPosition) >= maxDistance)
        {
            if (movementDirection == (maxPosition - transform.position).normalized)
            {
                movementDirection = (minPosition - transform.position).normalized;
            }
            else
            {
                movementDirection = (maxPosition - transform.position).normalized;
            }
            //canChangeDirection = false;
            //StartCoroutine(ChangeDirectionCooldownRoutine());
        }

    }

    //private IEnumerator ChangeDirectionCooldownRoutine()
    //{
    //    yield return new WaitForSeconds(changeDirectionCooldownTime);
    //    canChangeDirection = true; 
    //}

}
