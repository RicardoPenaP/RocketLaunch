using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEngineVFXController : MonoBehaviour
{
    [Header("Side Engine VFX Controller")]
    [SerializeField] PlayerMovement.RotationDirection engineSide;

    private ParticleSystem[] sideEngineParticleSystems;
    private PlayerMovement playerMovement;


    private void Awake()
    {
        sideEngineParticleSystems = GetComponentsInChildren<ParticleSystem>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void Start()
    {
        if (playerMovement)
        {
            playerMovement.OnStartRotating += PlayerMovement_OnStartRotating;
            playerMovement.OnStopRotating += PlayerMovement_OnStopRotating;
        }
    }

    private void OnDestroy()
    {
        if (playerMovement)
        {
            playerMovement.OnStartRotating -= PlayerMovement_OnStartRotating;
            playerMovement.OnStopRotating -= PlayerMovement_OnStopRotating;
        }
    }

    private void TurnOnEngineVFX()
    {
        foreach (ParticleSystem particleSystem in sideEngineParticleSystems)
        {
            particleSystem.Play();
        }
    }

    private void TurnOffEngineVFX()
    {
        foreach (ParticleSystem particleSystem in sideEngineParticleSystems)
        {
            particleSystem.Stop();
        }
    }

    private void PlayerMovement_OnStartRotating(object sender, PlayerMovement.RotationDirection rotationDirection)
    {
        if (engineSide != rotationDirection)
        {
            return;
        }
        TurnOnEngineVFX();
    }

    private void PlayerMovement_OnStopRotating(object sender, PlayerMovement.RotationDirection rotationDirection)
    {
        if (engineSide != rotationDirection)
        {
            return;
        }
        TurnOffEngineVFX();
    }

}
