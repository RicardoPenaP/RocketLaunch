using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingIndicator : MonoBehaviour
{    
    private PlayerLandingController playerLandingController;

    private void Awake()
    {
        playerLandingController = FindObjectOfType<PlayerLandingController>();
    }

    private void Start()
    {
        if (playerLandingController)
        {
            playerLandingController.OnAbleToLand += PlayerLandingController_OnAbleToLand;
            playerLandingController.OnUnableToLand += PlayerLandingController_OnUnableToLand;
        }
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (playerLandingController)
        {
            playerLandingController.OnAbleToLand -= PlayerLandingController_OnAbleToLand;
            playerLandingController.OnUnableToLand -= PlayerLandingController_OnUnableToLand;
        }
    }

    private void PlayerLandingController_OnAbleToLand(Vector3 prelandingPos)
    {
        gameObject.SetActive(true);
        transform.position = Camera.main.WorldToScreenPoint(prelandingPos);
    }

    private void PlayerLandingController_OnUnableToLand()
    {
        gameObject.SetActive(false);
        transform.position = Camera.main.WorldToScreenPoint(Vector3.zero);
    }
}
