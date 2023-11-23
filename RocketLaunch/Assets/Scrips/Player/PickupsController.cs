using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupsController : MonoBehaviour
{
    [Header("Pickups Controller")]
    [SerializeField] private LayerMask pickupsLayer;
    [SerializeField] private float magneticRange = 2.3f;

    [Header("Gizmos Settings")]
    [SerializeField] private bool showGizmos = false;

    public event Action<Pickup> OnPickupPicked;

    private void OnDrawGizmos()
    {
        if (!showGizmos)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, magneticRange);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Pickup>(out Pickup pickup))
        {
            OnPickupPicked?.Invoke(pickup);
            pickup.PickPickup();
        }
    }
}
