using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCrashHandler : MonoBehaviour
{
    private PlayerController playerController;
    private new ParticleSystem particleSystem;
    private new Rigidbody rigidbody;
    private MeshRenderer[] meshRenderers;
    private Collider[] colliders;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        rigidbody = GetComponentInParent<Rigidbody>();
        meshRenderers = transform.parent.GetComponentsInChildren<MeshRenderer>();
        colliders = GetComponentsInParent<Collider>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void Start()
    {
        if (playerController)
        {
            playerController.OnPlayerCrash += PlayerController_OnPlayerCrash;
            playerController.OnPlayerReset += PlayerController_OnPlayerReset;
        }
    }

    private void OnDestroy()
    {
        if (playerController)
        {
            playerController.OnPlayerCrash -= PlayerController_OnPlayerCrash;
            playerController.OnPlayerReset -= PlayerController_OnPlayerReset;
        }
    }

    private void PlayerController_OnPlayerCrash()
    {
        PlayerCrash();
    }

    private void PlayerController_OnPlayerReset(object sender, EventArgs e)
    {
        PlayerReset();
    }

    private void PlayerCrash()
    {
        particleSystem.Play();
        rigidbody.useGravity = false;
        rigidbody.Sleep();
        SetMeshRenderersEnabled(false);
        SetCollidersEnabled(false);
        playerMovement.enabled = false;
    }

    private void PlayerReset()
    {
        rigidbody.useGravity = true;
        SetMeshRenderersEnabled(true);
        SetCollidersEnabled(true);
        playerMovement.enabled = true;
    }

    private void SetMeshRenderersEnabled(bool state)
    {
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = state;
        }
    }

    private void SetCollidersEnabled(bool state)
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
    }
}
