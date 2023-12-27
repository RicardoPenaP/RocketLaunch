using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewStelarSystemData",menuName ="Stelar System Data")]
public class StelarSystemData : ScriptableObject
{
    [Header("Stelar System Data")]
    [SerializeField] private StelarSystem[] stelarSystems;

    public StelarSystem[] GetStelarSystems()
    {
        return stelarSystems;
    }
}
