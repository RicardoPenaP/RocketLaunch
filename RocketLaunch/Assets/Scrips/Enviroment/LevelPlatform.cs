using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelPlatform : MonoBehaviour
{
    public enum PlatformType { StartPlatform, SavePointPlatform, EndPlatform}
    public static event EventHandler OnEndPlatformReached;
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
            if (platformType == PlatformType.EndPlatform)
            {
                EndPlatformReached();
            }
            else
            {
                playerController.SetLastPlatformReached(this);
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

    private void EndPlatformReached()
    {
        OnEndPlatformReached?.Invoke(this, EventArgs.Empty);
    }

}
