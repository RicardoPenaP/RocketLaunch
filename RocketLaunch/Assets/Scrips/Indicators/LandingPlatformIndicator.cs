using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPlatformIndicator : MonoBehaviour
{
    [Header("Landing Platform Indicator")]
    [SerializeField] private float indicatorOffsetInPixelsFromTheMiddleOfTheScreen;

    private RectTransform rectTransform;

    private LandingPlatform landingPlatform;
    private Vector3 platformPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        landingPlatform = FindObjectOfType<LandingPlatform>();
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
        Vector3 platformDirection = (Camera.main.WorldToScreenPoint(platformPosition)- centerOfTheScreen).normalized;
        transform.right = platformDirection;
        transform.position = centerOfTheScreen + platformDirection * indicatorOffsetInPixelsFromTheMiddleOfTheScreen; 
    }

    private void LandingPlatform_OnPlatformInsideScreen()
    {
        gameObject.SetActive(false);
    }
    private void LandingPlatform_OnPlatformOutsideScreen(Vector3 platformPosition)
    {
        gameObject.SetActive(true);
        this.platformPosition = platformPosition;
        UpdateLandingPlatformIndicator();
    }
}
