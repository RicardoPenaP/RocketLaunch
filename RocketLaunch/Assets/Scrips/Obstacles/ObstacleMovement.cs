using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [Header("Obstacle Movement")]
    [SerializeField] private bool canMove = false;
    [SerializeField] private bool showGizmos = false;
    [SerializeField] private Vector3 movementDistance;
    [SerializeField] private float movementSpeed = 10f;


    private Vector3 startPosition;
    private Vector3 maxPosition;
    private Vector3 minPosition;

    private void OnDrawGizmosSelected()
    {
        if (!showGizmos)
        {
            return;
        }

        Vector3 startPoint = transform.position;
        Vector3 finishPoint = transform.position;
        startPoint.y -= movementDistance.y / 2;
        finishPoint.y += movementDistance.y / 2;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPoint, finishPoint);

        startPoint = transform.position;
        startPoint.x -= movementDistance.x / 2;
        finishPoint = transform.position;
        finishPoint.x += movementDistance.x / 2;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPoint, finishPoint);

        startPoint = transform.position;
        startPoint.z -= movementDistance.z / 2;
        finishPoint = transform.position;
        finishPoint.z += movementDistance.z / 2;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(startPoint, finishPoint);
    }

    private void Awake()
    {
        startPosition = transform.position;
    }
}
