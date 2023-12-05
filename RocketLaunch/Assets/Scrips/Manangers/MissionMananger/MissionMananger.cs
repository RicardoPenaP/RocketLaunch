using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionMananger : MonoBehaviour
{
    public static MissionMananger Instance { get; private set; }

    [Header("Mission Mananger")]
    [SerializeField] StelarSystem[] stelarSystems;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public StelarSystem GetStelarSystem(StelarSystemID stelarSystemID)
    {
        foreach (StelarSystem stelarSystem in stelarSystems)
        {
            if (stelarSystem.GetStelarSystemID() == stelarSystemID)
            {
                return stelarSystem;
            }
        }
        return null;
    }

}
