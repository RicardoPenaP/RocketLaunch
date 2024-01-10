using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LandingPlatformIndicator : MonoBehaviour
{
    [Header("Landing Platform Indicator")]
    [SerializeField] private float indicatorOffsetInPixelsFromTheMiddleOfTheScreen;

    public event Action OnIndicatorTurnOn;
    public event Action OnIndicatorTurnOff;



    private LandingPlatform landingPlatform;
    private PlayerController playerController;

    private void Awake()
    {       
        landingPlatform = FindObjectOfType<LandingPlatform>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        if (landingPlatform)
        {
            landingPlatform.OnPlatformInsideScreen += LandingPlatform_OnPlatformInsideScreen;
            landingPlatform.OnPlatformOutsideScreen += LandingPlatform_OnPlatformOutsideScreen;
        }
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (landingPlatform)
        {
            landingPlatform.OnPlatformInsideScreen -= LandingPlatform_OnPlatformInsideScreen;
            landingPlatform.OnPlatformOutsideScreen -= LandingPlatform_OnPlatformOutsideScreen;
        }
    }

    private void UpdateLandingPlatformIndicator()
    {
        Vector3 centerOfTheScreen = new Vector3(Screen.width/2,Screen.height/2,0f);
        Vector3 platformDirection = (landingPlatform.transform.position - playerController.transform.position).normalized;
        platformDirection.z = 0;
        transform.right = platformDirection;
        transform.position = centerOfTheScreen + (platformDirection * indicatorOffsetInPixelsFromTheMiddleOfTheScreen); 
    }

    private void LandingPlatform_OnPlatformInsideScreen()
    {
        gameObject.SetActive(false);
        OnIndicatorTurnOff?.Invoke();
    }
    private void LandingPlatform_OnPlatformOutsideScreen(Vector3 platformPosition)
    {
        gameObject.SetActive(true);        
        UpdateLandingPlatformIndicator();
        OnIndicatorTurnOn?.Invoke();
    }
}
