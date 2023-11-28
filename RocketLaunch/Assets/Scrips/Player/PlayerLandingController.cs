using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerLandingController : MonoBehaviour
{
    public class LandingData : EventArgs
    {
        public float currentAngle;
        public float targetAngle;
        public float normalizedGreenAreaPercentage;
        public float normalizedYellowAreaPercentage; 
    }

    [Header("Player Landing Controller")]
    [Header("Landing Settings")]
    [SerializeField] private float landingMinDistance = 1f;
    [SerializeField] private float preLandingDuration = 1f;
    [SerializeField] private float landingDuration = 5f;
    [SerializeField] private float rotationSensibility = 1f;
    [SerializeField] private float rotationForce = 100f;
    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private bool showGizmos = true;
    [SerializeField, Min(0)] private float maxSpeedToStartLanding = 2f;    
    [SerializeField, Range(0f, 1f)] private float greenAreaPercentage = 0.15f;
    [SerializeField, Range(0f, 1f)] private float yellowAreaPercentage = 0.4f; 

    public event EventHandler OnPreLandingStart;
    public event EventHandler OnLandingStart;
    public event EventHandler OnLandingUpdated;
    public event EventHandler OnLandingFinished;
    public event Action<PlayerMovement.RotationDirection> OnSideEngineStarted;
    public event Action<PlayerMovement.RotationDirection> OnSideEngineStoped;

    private PlayerCollisionHandler playerCollisionHandler;

    private LandingPlatform landingPlatform;
    private new Rigidbody rigidbody;

    private Vector3 preLandingPosition;
    private Vector3 landingPosition;

    private bool isPreLanding = false;
    private bool isLanding = false;
    private int landingTries = 0;
    private float maxLandingYDistance;    

    private void OnDrawGizmos()
    {
        if (!showGizmos)
        {
            return;
        }
        //For debugging
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, landingMinDistance);
    }

    private void Awake()
    {
        playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
        rigidbody = GetComponent<Rigidbody>();
        OnPreLandingStart += (object sender, EventArgs e) => { rigidbody.useGravity = false; };
        OnLandingFinished += (object sender, EventArgs e) => { rigidbody.useGravity = true; };
    }

    private void Start()
    {
        if (playerCollisionHandler)
        {
            playerCollisionHandler.OnCollisionEnterWithObject += PlayerCollisionHandler_OnCollisionWithObject;
        }
    }

    private void Update()
    {        
        if (!isPreLanding && !isLanding)
        {
            if (TryFindLandingPlatform())
            {
                if (Mathf.Abs(rigidbody.velocity.x) <= maxSpeedToStartLanding)
                {
                    if (InputMananger.Instance.GetInteractInputWasTriggered())
                    {
                        StartPreLanding();
                    }
                }
            }                       
        }        

        if (isLanding)
        {
            LandingUpdate();
        }

        if (isLanding || isPreLanding)
        {
            LandingData landingData = new LandingData();
            landingData.currentAngle = Vector3.Angle(Vector3.right, transform.up);
            landingData.targetAngle = landingPlatform.GetLandingAngle();
            landingData.normalizedGreenAreaPercentage = greenAreaPercentage;
            landingData.normalizedYellowAreaPercentage = yellowAreaPercentage;            
            OnLandingUpdated?.Invoke(this, landingData);
        }
    }

    private void OnDestroy()
    {
        OnPreLandingStart -= (object sender, EventArgs e) => { rigidbody.useGravity = false; };
        OnLandingFinished -= (object sender, EventArgs e) => { rigidbody.useGravity = true; };
        if (playerCollisionHandler)
        {
            playerCollisionHandler.OnCollisionEnterWithObject -= PlayerCollisionHandler_OnCollisionWithObject;
        }
    }

    private bool TryFindLandingPlatform()
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
        landingTries++;
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
            if (rotationDirection > 0f)
            {
                OnSideEngineStarted?.Invoke(PlayerMovement.RotationDirection.Rigth);
            }
            else
            {
                OnSideEngineStarted?.Invoke(PlayerMovement.RotationDirection.Left);
            }
        }

        if (InputMananger.Instance.GetRotationDirectionInputWasReleasedThisFrame())
        {
            OnSideEngineStoped?.Invoke(PlayerMovement.RotationDirection.Rigth);
            OnSideEngineStoped?.Invoke(PlayerMovement.RotationDirection.Left);
        }       
    }

    private void LandingFinished()
    {
        const float TOTAL_ANGLE_DEGREES = 360f;
        float currentAngle = Vector3.Angle(Vector3.right, transform.up);
        float desiredAngle = landingPlatform.GetLandingAngle();
        float greenAreaOffset = (TOTAL_ANGLE_DEGREES * greenAreaPercentage) / 2;
        float yellowAreaOffset = (TOTAL_ANGLE_DEGREES * yellowAreaPercentage) / 2;

        if (currentAngle >= desiredAngle - greenAreaOffset && currentAngle <= desiredAngle + greenAreaOffset)
        {
            //Landing with green score
          
        }
        else
        {
            if (currentAngle >= desiredAngle - yellowAreaOffset && currentAngle <= desiredAngle + yellowAreaOffset)
            {
                //Landing with yellow score

            }
            else
            {
                //Reset the landing process
                ResetLanding();
                return;
            }
        }

        isLanding = false;
        rigidbody.isKinematic = true;
        rigidbody.Sleep();        
        OnLandingFinished?.Invoke(this, EventArgs.Empty);
        //Implement landing score system
    }

    private void ResetLanding()
    {
        isLanding = false;
        StartPreLanding();
    }

    private void PlayerCollisionHandler_OnCollisionWithObject(object sender, EventArgs e)
    {
        if (e is PlayerCollisionHandler.CollisionInfo<LevelPlatform> collisionInfo)
        {
            if (collisionInfo.collisionObject.GetPlatformType() == LevelPlatform.PlatformType.Landing && isLanding)
            {
                LandingFinished();
            }            
        }
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
