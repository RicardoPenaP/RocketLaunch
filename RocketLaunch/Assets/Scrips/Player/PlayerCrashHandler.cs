using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrashHandler : MonoBehaviour
{
    private PlayerController playerController;
    private new ParticleSystem particleSystem;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        if (playerController)
        {
            playerController.OnPlayerCrash += PlayerController_OnPlayerCrash;
        }
    }

    private void PlayerController_OnPlayerCrash()
    {
        particleSystem.Play();
    }
}
