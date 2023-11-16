using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelPlatform : MonoBehaviour
{
    public enum PlatformType { Takeoff, SavePoint, Landing}
    public static event EventHandler OnLandingPlatformReached;
    [Header("Level Platform")]
    [SerializeField] private PlatformType platformType;

    private Transform spawnPoint;

    private void Awake()
    {
        spawnPoint = transform.GetChild(1);
    }

    public PlatformType GetPlatformType()
    {
        return platformType;
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

    private void LandingPlatformReached()
    {
        OnLandingPlatformReached?.Invoke(this, EventArgs.Empty);
    }

}
