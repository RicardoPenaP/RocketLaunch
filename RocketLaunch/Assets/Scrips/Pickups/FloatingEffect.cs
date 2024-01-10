using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    [Header("Floating Effect")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxYOffset;

    private Pickup pickup;
    private float maxY, minY;
    //private float yMovementDirection;

    private void Awake()
    {
        pickup = GetComponentInParent<Pickup>();
    }

    private void Start()
    {
        float startingY = transform.position.y;
        maxY = startingY + maxYOffset;
        minY = startingY - maxYOffset;
        //yMovementDirection = -1;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (pickup.IsBeenAtractedToThePlayer)
        {
            return;
        }
        Vector3 rotation = Vector3.up * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotation);

        //Vector3 newPosition = transform.position + Vector3.up * yMovementDirection * movementSpeed * Time.deltaTime;
        //newPosition.y = Mathf.Clamp(newPosition.y,minY,maxY);

        //if (newPosition.y >= maxY || newPosition.y <= minY)
        //{
        //    yMovementDirection *= -1;
        //}

        //transform.position = newPosition;
    }

    
}
