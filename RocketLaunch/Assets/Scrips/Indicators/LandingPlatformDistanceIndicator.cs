using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LandingPlatformDistanceIndicator : MonoBehaviour
{
    [Header("Landing Platform Distance Indicator")]
    [SerializeField] private LandingPlatformIndicator landingPlatformIndicator;
    [SerializeField] private float distanceOffset = 10f;
    [SerializeField] private TextMeshProUGUI distanceText;

    private Transform playerController;
    private Transform landingPlatform;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>().transform;
        landingPlatform = FindObjectOfType<LandingPlatform>().transform;

        if (landingPlatformIndicator)
        {
            landingPlatformIndicator.OnIndicatorTurnOn += LandingPlatformIndicator_OnIndicatorTurnOn;
            landingPlatformIndicator.OnIndicatorTurnOff += LandingPlatformIndicator_OnIndicatorTurnOff;
        }
    }    

    private void LandingPlatformIndicator_OnIndicatorTurnOn()
    {
        gameObject.SetActive(true);
    }

    private void LandingPlatformIndicator_OnIndicatorTurnOff()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = landingPlatformIndicator.transform.position - (Vector3.up * distanceOffset);
        if (playerController && landingPlatform)
        {
            distanceText.text = Vector3.Distance(playerController.position, landingPlatform.position).ToString("0");
        }
    }

}
