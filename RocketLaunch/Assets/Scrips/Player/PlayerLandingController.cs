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

    public class LandingCompleteData : EventArgs
    {
        public int landingTries;
        public float normalizedAmountOfRemaningLifes;
        public float normalizedLandingScore;        
    }

    [Header("Player Landing Controller")]
    [Header("Landing Settings")]
    [SerializeField] private float landingMinDistance = 1f;
    [SerializeField] private float preLandingDuration = 1f;
    [SerializeField] private float landingDuration = 5f;
    [SerializeField] private float rotationForce = 100f;
    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private bool showGizmos = true;
    //[SerializeField, Min(0)] private float maxSpeedToStartLanding = 2f;    
    [SerializeField, Range(0f, 1f)] private float greenAreaPercentage = 0.15f;
    [SerializeField, Range(0f, 1f)] private float yellowAreaPercentage = 0.4f; 

    public event EventHandler OnPreLandingStart;
    public event EventHandler OnLandingStart;
    public event EventHandler OnLandingVisualsUpdated;
    public event EventHandler OnLandingFinished;
    public event Action<PlayerMovement.RotationDirection> OnSideEngineStarted;
    public event Action<PlayerMovement.RotationDirection> OnSideEngineStoped;
    public event Action<Vector3> OnAbleToLand;
    public event Action OnUnableToLand;

    private PlayerCollisionHandler playerCollisionHandler;

    private LandingPlatform landingPlatform;
    private new Rigidbody rigidbody;
    private Collider[] colliders;

    private Vector3 preLandingPosition;
    private Vector3 landingPosition;

    private bool isPreLanding = false;
    private bool isLanding = false;
    private int landingTries = 0;

    private float angularDragMultiplier;
    private float yellowAreaMultiplier;
    private float greenAreaMultiplier;


    public bool IsPreLanding { get { return isPreLanding; } }
    public bool IsLanding { get { return isLanding; } }

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
        colliders = GetComponents<Collider>();
        OnPreLandingStart += (object sender, EventArgs e) =>
        {
            rigidbody.useGravity = false;
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
        };
        OnLandingStart += (object sender, EventArgs e) =>
        {
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }
        };
        OnLandingFinished += (object sender, EventArgs e) => { rigidbody.useGravity = true; };
    }

    private void Start()
    {
        if (playerCollisionHandler)
        {
            playerCollisionHandler.OnCollisionEnterWithObject += PlayerCollisionHandler_OnCollisionWithObject;
        }

        Initialize();
    }

    private void Update()
    {        
        if (!isPreLanding && !isLanding)
        {
            if (TryFindLandingPlatform())
            {
                OnAbleToLand?.Invoke(landingPlatform.GetPreLandingPosition());
                if (InputMananger.Instance.GetInteractInputWasTriggered())
                {
                    StartPreLanding();
                }
            }
            else
            {
                OnUnableToLand?.Invoke();
            }
        }
        else
        {
            OnUnableToLand?.Invoke();
        }

        if (isLanding)
        {
            LandingUpdate();
        }

        if (isLanding || isPreLanding)
        {
            LandingData landingData = new LandingData();
            //landingData.currentAngle = Vector3.Angle(Vector3.right, transform.up);
            landingData.currentAngle = Vector3.SignedAngle(Vector3.right, transform.up, Vector3.forward);
            landingData.targetAngle = landingPlatform.GetLandingAngle();
            landingData.normalizedGreenAreaPercentage = greenAreaPercentage;
            landingData.normalizedYellowAreaPercentage = yellowAreaPercentage;            
            OnLandingVisualsUpdated?.Invoke(this, landingData);
        }
    }

    private void OnDestroy()
    {
        OnPreLandingStart -= (object sender, EventArgs e) =>
        {
            rigidbody.useGravity = false;
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
        };
        OnLandingStart -= (object sender, EventArgs e) =>
        {
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }
        };
        OnLandingFinished -= (object sender, EventArgs e) => { rigidbody.useGravity = true; };
        if (playerCollisionHandler)
        {
            playerCollisionHandler.OnCollisionEnterWithObject -= PlayerCollisionHandler_OnCollisionWithObject;
        }
    }

    private void Initialize()
    {
        angularDragMultiplier = 1f;
        yellowAreaMultiplier = 1f;
        greenAreaMultiplier = 1f;

        if (RocketStatsMananger.Instance)
        {
            int rocketStatLevel = RocketStatsMananger.Instance.GetRocketStat(StatType.LandingSystem).GetStatLevel();
            float rocketStatMultiplierAugmentCoeficient = RocketStatsMananger.Instance.GetAngularDragMultiplierAugmentCoeficient();
            angularDragMultiplier -= rocketStatMultiplierAugmentCoeficient * rocketStatLevel;
            rocketStatMultiplierAugmentCoeficient = RocketStatsMananger.Instance.GetYellowAreaMultiplierAugmentCoeficient();
            yellowAreaMultiplier += rocketStatMultiplierAugmentCoeficient * rocketStatLevel;
            rocketStatMultiplierAugmentCoeficient = RocketStatsMananger.Instance.GetGreenAreaMultiplierAugmentCoeficient();
            greenAreaMultiplier += rocketStatMultiplierAugmentCoeficient * rocketStatLevel;
        }

        rotationForce *= angularDragMultiplier;

        yellowAreaPercentage *= yellowAreaMultiplier;
        greenAreaPercentage *= greenAreaMultiplier;

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
            Vector3 torqueAxis = Vector3.forward * rotationForce * rotationDirection * Time.deltaTime;
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
        float currentAngle = Vector3.SignedAngle(Vector3.right,transform.up,Vector3.forward);
        float desiredAngle = landingPlatform.GetLandingAngle();
        float greenAreaOffset = (TOTAL_ANGLE_DEGREES * greenAreaPercentage) / 2;
        float yellowAreaOffset = (TOTAL_ANGLE_DEGREES * yellowAreaPercentage) / 2;
        float normalizedLandingScore = 0f;

        if (currentAngle >= desiredAngle - greenAreaOffset && currentAngle <= desiredAngle + greenAreaOffset)
        {
            normalizedLandingScore = 1f;
        }
        else
        {
            if (currentAngle >= desiredAngle - yellowAreaOffset && currentAngle <= desiredAngle + yellowAreaOffset)
            {
                normalizedLandingScore = 0.5f;
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
        LandingCompleteData landingCompleteData = new LandingCompleteData();
        landingCompleteData.normalizedLandingScore = normalizedLandingScore;
        landingCompleteData.normalizedAmountOfRemaningLifes = GetComponent<PlayerController>().GetNormalizedAmountOfRemaningLifes();
        landingCompleteData.landingTries = landingTries;
        OnLandingFinished?.Invoke(this, landingCompleteData);
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
        //float startYPosition = transform.position.y;
        Vector3 startPosition = transform.position;

        while (timer < landingDuration && isLanding)
        {
            timer += Time.deltaTime;
            float progress = timer / landingDuration;
            transform.position = Vector3.Lerp(startPosition, landingPosition, progress);

            yield return null;
        }

        LandingFinished();


    }


}
