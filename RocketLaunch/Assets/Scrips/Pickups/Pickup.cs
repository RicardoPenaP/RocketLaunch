using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pickup : MonoBehaviour
{
    public enum PickupType { Cooland, Fuel}
    
    [Header("Pickup")]
    [SerializeField] private PickupType pickupType;
    [SerializeField] private float pickupRange = 4f;
    [SerializeField,Range(0,100)] private int effectivePercentage;
    [SerializeField] private float defaultMovementSpeed = 3f;
    [SerializeField, Range(0f, 1f)] private float movementSpeedAugmentCoeficient = 0.2f;

    private PlayerController playerController;
    private Transform playerTransform;

    private Vector3 startingPos;

    private float currentMovementSpeed;

    public bool IsBeenAtractedToThePlayer { get; private set; }

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerTransform = playerController.transform;
       
        currentMovementSpeed = defaultMovementSpeed;
        startingPos = transform.position;        
    }

    private void Start()
    {
        if (playerController)
        {
            playerController.OnPlayerReset += PlayerController_OnPlayerReset;
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
            IsBeenAtractedToThePlayer = false;
            return;
        }
        IsBeenAtractedToThePlayer = true;
        Vector3 movementDirection = (playerTransform.position - transform.position).normalized;
        transform.position += movementDirection * currentMovementSpeed * Time.deltaTime;
        currentMovementSpeed += currentMovementSpeed * movementSpeedAugmentCoeficient * Time.deltaTime;
    }

    private void ResetPickup()
    {
        transform.position = startingPos;
        IsBeenAtractedToThePlayer = false;
        gameObject.SetActive(true);
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
        gameObject.SetActive(false);
    }

    private void PlayerController_OnPlayerReset(object sender, EventArgs e)
    {
        ResetPickup();
    }
}
