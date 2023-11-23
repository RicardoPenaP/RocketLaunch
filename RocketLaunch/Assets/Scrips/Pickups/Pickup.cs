using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType { Cooland, Fuel}
    [Header("Pickup")]
    [SerializeField] private PickupType pickupType;
    [SerializeField,Range(0,100)] private int effectivePercentage;

    public PickupType GetPickupType()
    {
        return pickupType;
    }

    public int GetEffectivePercentage()
    {
        return effectivePercentage;
    }
}
