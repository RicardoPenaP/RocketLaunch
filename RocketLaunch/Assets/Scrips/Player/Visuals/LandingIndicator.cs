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

        }
    }
}
