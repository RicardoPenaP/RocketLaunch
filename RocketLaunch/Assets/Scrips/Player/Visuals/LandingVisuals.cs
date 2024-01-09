using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LandingVisuals : MonoBehaviour
{
    private const float TOTAL_ANGLE_DEGREES = 360f;

    [Header("Landing Visuals")]
    [SerializeField] private Image yellowAreaImage;
    [SerializeField] private Image greenAreaImage;
    [SerializeField] private Image rotationIndicatorImage;

    private PlayerLandingController playerLandingController;

    private void Awake()
    {
        playerLandingController = FindObjectOfType<PlayerLandingController>();            
    }

    private void Start()
    {
        if (playerLandingController)
        {
            playerLandingController.OnPreLandingStart += PlayerLandingController_OnPreLandingStart;
            playerLandingController.OnLandingVisualsUpdated += PlayerLandingController_OnLandingVisualsUpdate;
        }

        transform.parent.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (playerLandingController)
        {
            playerLandingController.OnPreLandingStart -= PlayerLandingController_OnPreLandingStart;
            playerLandingController.OnLandingVisualsUpdated -= PlayerLandingController_OnLandingVisualsUpdate;
        }
    }

    private void PlayerLandingController_OnPreLandingStart(object sender, EventArgs e)
    {
        transform.parent.gameObject.SetActive(true);
    }

    private void PlayerLandingController_OnLandingVisualsUpdate(object sender, EventArgs e)
    {
        PlayerLandingController.LandingData landingData = (e as PlayerLandingController.LandingData);
        yellowAreaImage.fillAmount = landingData.normalizedYellowAreaPercentage;
        greenAreaImage.fillAmount = landingData.normalizedGreenAreaPercentage;
        RotateAreasImages(landingData);
        RotateRotationIndicatorImage(landingData.currentAngle);
        
    }

    private void RotateAreasImages(PlayerLandingController.LandingData landingData)
    {
        float yellowAreaZRotation = landingData.targetAngle + ((TOTAL_ANGLE_DEGREES * landingData.normalizedYellowAreaPercentage) / 2);
        float greenAreaZRotation = landingData.targetAngle + ((TOTAL_ANGLE_DEGREES * landingData.normalizedGreenAreaPercentage) / 2);

        Vector3 newRotation = yellowAreaImage.transform.eulerAngles;
        newRotation.z = yellowAreaZRotation;
        yellowAreaImage.transform.eulerAngles = newRotation;

        newRotation = greenAreaImage.transform.eulerAngles;
        newRotation.z = greenAreaZRotation;
        greenAreaImage.transform.eulerAngles = newRotation;
    }    

    private void RotateRotationIndicatorImage(float currentAngle)
    {
        Vector3 newRotation = rotationIndicatorImage.transform.eulerAngles;
        newRotation.z = currentAngle - 90;
        rotationIndicatorImage.transform.eulerAngles = newRotation;
    }
}
