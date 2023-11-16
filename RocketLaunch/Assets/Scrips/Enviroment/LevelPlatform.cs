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
        spawnPoint = transform.GetChild(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            if (playerController.transform.position.y > transform.position.y)
            {
                if (platformType == PlatformType.Landing)
                {
                    LandPlatformReached();
                }
                else
                {
                    playerController.SetLastPlatformReached(this);
                }
            }                      
        }
    }

    public PlatformType GetPlatformType()
    {
        return platformType;
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

    private void LandPlatformReached()
    {
        OnLandingPlatformReached?.Invoke(this, EventArgs.Empty);
    }

}
