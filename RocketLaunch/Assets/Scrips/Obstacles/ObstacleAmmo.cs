using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAmmo : MonoBehaviour
{
    [Header("Obstacle Ammo")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxTravelDistance;

    private Vector3 startPosition;
    private Vector3 movementDirection;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += movementDirection * movementSpeed * Time.deltaTime;
        if (Vector3.Distance(startPosition,transform.position) >= maxTravelDistance)
        {
            SelfDestroy();
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

    public void SetMovementDirection(Vector3 movementDirection)
    {
        this.movementDirection = movementDirection;
    }
}
