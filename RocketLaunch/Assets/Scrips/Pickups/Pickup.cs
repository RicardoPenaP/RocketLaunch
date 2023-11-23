using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType { Cooland, Fuel}

    private static Transform playerTransform;
    [Header("Pickup")]
    [SerializeField] private PickupType pickupType;
    [SerializeField] private float pickupRange = 4f;
    [SerializeField,Range(0,100)] private int effectivePercentage;
    [SerializeField] private float defaultMovementSpeed = 3f;
    [SerializeField, Range(0f, 1f)] private float movementSpeedAugmentCoeficient = 0.2f;

    private float currentMovementSpeed;

    public bool IsBeenAtractedToThePlayer { get; private set; }

    private void Awake()
    {
        currentMovementSpeed = defaultMovementSpeed;
    }

    private void Start()
    {
        if (!playerTransform)
        {
            playerTransform = FindObjectOfType<PlayerController>().transform;
        }
    }

    private void Update()
    {
        if (playerTransform)
        {
            MoveTowardsThePlayer();
        }        
    }

    private void MoveTowardsThePlayer()
    {
        if (Vector3.Distance(transform.position,playerTransform.position) >= pickupRange)
        {
            return;
        }
        Vector3 movementDirection = (playerTransform.position - transform.position).normalized;
        transform.position += movementDirection * currentMovementSpeed * Time.deltaTime;
        currentMovementSpeed += currentMovementSpeed * movementSpeedAugmentCoeficient * Time.deltaTime;
    }

    public PickupType GetPickupType()
    {
        return pickupType;
    }

    public int GetEffectivePercentage()
    {
        return effectivePercentage;
    }
    
    public void PickPickup()
    {
        Destroy(gameObject);
    }
}
