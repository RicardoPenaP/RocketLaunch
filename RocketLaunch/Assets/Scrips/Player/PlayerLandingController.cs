using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerLandingController : MonoBehaviour
{
    [Header("Player Landing Controller")]
    [Header("Landing Settings")]
    [SerializeField] private float landingMinDistance = 1f;
    [SerializeField] private float preLandingDuration = 1f;
    [SerializeField] private float landingDuration = 5f;
    [SerializeField] private float rotationSensibility = 1f;
    [SerializeField] private float rotationForce = 100f;
    [SerializeField] private LayerMask platformsLayerMask;

    public event EventHandler OnPreLandingStart;
    public event EventHandler OnLandingStart;
    public event EventHandler OnLandingFinished;

    private LandingPlatform landingPlatform;
    private new Rigidbody rigidbody;

    private Vector3 preLandingPosition;
    private Vector3 landingPosition;

    private bool isPreLanding = false;
    private bool isLanding = false;

    private void OnDrawGizmos()
    {
        //For debugging
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, landingMinDistance);
    }

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
                if (!isPreLanding && !isLanding)
                {
                    StartPreLanding();
                }
            }
        }

        if (isLanding)
        {
            LandingUpdate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isLanding && collision.transform.TryGetComponent<LandingPlatform>(out LandingPlatform landingPlatform))
        {
            if (this.landingPlatform == landingPlatform)
            {
                LandingFinished();
            }
        }
    }

    private bool TryGetLandingPlatform()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, landingMinDistance, platformsLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<LevelPlatform>(out LevelPlatform levelPlatform))
            {
                if (levelPlatform.GetPlatformType() == LevelPlatform.PlatformType.Landing)
                {
                    landingPlatform = levelPlatform.GetComponent<LandingPlatform>();
                    preLandingPosition = landingPlatform.GetPreLandingPosition();
                    landingPosition = landingPlatform.transform.position;
                    return true;
                }
            }
        }

        landingPlatform = null;
        return false;
    }

    private void StartPreLanding()
    {
        isPreLanding = true;
        OnPreLandingStart?.Invoke(this, EventArgs.Empty);
        rigidbody.Sleep();
        StartCoroutine(PreLandingRoutine());
    }

    private void StartLanding()
    {
        isLanding = true;
        OnLandingStart?.Invoke(this, EventArgs.Empty);
        StartCoroutine(LandingRoutine());
    }

    private void LandingUpdate()
    {
        if (InputMananger.Instance.TryGetRotationDirectionInput(out float rotationDirection))
        {
            Vector3 torqueAxis = Vector3.forward * rotationForce * rotationDirection * rotationSensibility * Time.deltaTime;
            rigidbody.AddTorque(torqueAxis, ForceMode.Impulse);
        }
    }

    private void LandingFinished()
    {
        isLanding = false;
        OnLandingFinished?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator PreLandingRoutine()
    {
        float timer = 0;
        Vector3 startPosition = transform.position;

        while (timer < preLandingDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / preLandingDuration;

            transform.position = Vector3.Lerp(startPosition, preLandingPosition, progress);

            yield return null;
        }
        isPreLanding = false;        
        StartLanding();
    }

    private IEnumerator LandingRoutine()
    {
        float timer = 0;
        float startYPosition = transform.position.y;

        while (timer < landingDuration && isLanding)
        {
            timer += Time.deltaTime;
            float progress = timer / landingDuration;
            float yPosition = Mathf.Lerp(startYPosition, landingPosition.y, progress);
            Vector3 newPosition = transform.position;
            newPosition.y = yPosition;
            transform.position = newPosition;

            yield return null;
        }

        LandingFinished();


    }


}
