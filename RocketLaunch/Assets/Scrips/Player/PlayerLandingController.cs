using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerLandingController : MonoBehaviour
{
    [Header("Player Landing Controller")]
    [Header("Landing Settings")]
    [SerializeField] private float landingMinDistance = 1f;
    [SerializeField] private float prelandingDuration = 1f;
    [SerializeField] private float landingDuration = 5f;
    [SerializeField] private float rotationSensibility = 1f;
    [SerializeField] private float rotationForce = 100f;
    [SerializeField] private LayerMask platformsLayerMask;

    public event EventHandler OnLandingStart;

    private LevelPlatform landingPlatform;
    private new Rigidbody rigidbody;

    private bool isLanding = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (TryGetLandingPlatform())
        {
            if (InputMananger.Instance.GetInteractInputWasTriggered())
            {
                StartLanding();
            }
        }

        if (isLanding)
        {
            LandingUpdate();
        }
    }

    private bool TryGetLandingPlatform()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, landingMinDistance, platformsLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.TryGetComponent<LevelPlatform>(out LevelPlatform levelPlatform))
            {
                if (levelPlatform.GetPlatformType() == LevelPlatform.PlatformType.Landing)
                {
                    landingPlatform = levelPlatform;
                    return true;
                }
            }
        }

        landingPlatform = null;
        return false;
    }

    private void StartLanding()
    {
        isLanding = true;
        OnLandingStart?.Invoke(this, EventArgs.Empty);
    }

    private void LandingUpdate()
    {
        if (InputMananger.Instance.TryGetRotationDirectionInput(out float rotationDirection))
        {
            Vector3 torqueAxis = Vector3.forward * rotationForce * rotationDirection * rotationSensibility * Time.deltaTime;
            rigidbody.AddTorque(torqueAxis, ForceMode.Impulse);
        }
    }
}
