using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    public enum FPSTarget { Low = 30, Mid = 60, High = 120,Ultra = 240, Unlimited = -1}

    public static FPSLimiter Instance { get; private set; }

    private FPSTarget currentFPSTarget = FPSTarget.Mid;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        Application.targetFrameRate = (int)currentFPSTarget;
    }


}
