using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{  
    [Header("Player Controller")]
    [SerializeField] private int maxLifesAmount = 3;
    [SerializeField] private float crashRoutineWait = 2f;

    public event Action<int> OnCurrentLifesChange;
    public event EventHandler OnLifeRemove;
    public event EventHandler OnDie;
    public event EventHandler OnPlayerReset;
    public event Action OnPlayerCrash;

    private PlayerInmune playerInmune;
    private LevelPlatform lastPlatformReached;
    private PlayerCollisionHandler playerCollisionHandler;
    private PlayerLandingController playerLandingController;

    private int currentLifesAmount;
    private bool playerCrahsed = false;    
    public bool IsAlive { get; private set; }

    private void Awake()
    {
        playerInmune = GetComponent<PlayerInmune>();
        playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
        playerLandingController = GetComponent<PlayerLandingController>();
        IsAlive = true;        
    }

    private void Start()
    {
        currentLifesAmount = maxLifesAmount;
        OnCurrentLifesChange?.Invoke(currentLifesAmount);
        SetStartPlatform();
        playerCollisionHandler.OnCollisionEnterWithObject += PlayerCollisionHandler_OnCollisionEnterWithObject;
        playerLandingController.OnPreLandingStart += PlayerLandingController_OnStartPrelanding;
    }

    private void Update()
    {
        if (InputMananger.Instance.GetResetInputWasTriggered() && Time.timeScale > 0f)
        {
            Crash();
        }            
    }

    private void OnDestroy()
    {       
        playerCollisionHandler.OnCollisionEnterWithObject -= PlayerCollisionHandler_OnCollisionEnterWithObject;
        playerLandingController.OnPreLandingStart -= PlayerLandingController_OnStartPrelanding;
    }

    private void SetStartPlatform()
    {
        LevelPlatform[] levelPlatforms = FindObjectsOfType<LevelPlatform>();
        foreach (LevelPlatform levelPlatform in levelPlatforms)
        {
            if (levelPlatform.GetPlatformType() == LevelPlatform.PlatformType.Takeoff)
            {
                lastPlatformReached = levelPlatform;
                return;
            }
        }

        Debug.LogError("The start platform doesnt exist");

    }

    private void RemoveOneLife()
    {       
        currentLifesAmount--;
        OnCurrentLifesChange?.Invoke(currentLifesAmount);
        OnLifeRemove?.Invoke(this, EventArgs.Empty);

        if (currentLifesAmount <= 0)
        {
            currentLifesAmount = 0;
            Die();
            IsAlive = false;            
        }

        PlayerReset();
    }

    private void Die()
    {
        OnDie?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerReset()
    {
        if (!IsAlive)
        {
            return;
        }
        
        transform.position = lastPlatformReached.GetSpawnPoint().position;
        playerCrahsed = false;        
        OnPlayerReset?.Invoke(this, EventArgs.Empty);        
    }

    private void Crash()
    {
        if (playerCrahsed)
        {
            return;
        }
        StartCoroutine(PlayerCrashRoutine());
    }

    private void PlayerCollisionHandler_OnCollisionEnterWithObject(object sender, EventArgs e)
    {
        switch (e)
        {
            case PlayerCollisionHandler.CollisionInfo<ObstacleController> collisionInfo:
                if (playerInmune.IsInmune)
                {
                    break;
                }
                Crash();


                break;

            case PlayerCollisionHandler.CollisionInfo<LevelPlatform> collisionInfo:
                if (collisionInfo.collisionObject.GetPlatformType() == LevelPlatform.PlatformType.CheckPoint)
                {
                    SetLastPlatformReached(collisionInfo.collisionObject);
                }
                break;
            default:
                break;
        }
    }

    private void PlayerLandingController_OnStartPrelanding(object sender, EventArgs e)
    {
        enabled = false;
    }


    public void SetLastPlatformReached(LevelPlatform levelPlatform)
    {
        if (lastPlatformReached != levelPlatform)
        {
            lastPlatformReached = levelPlatform;
        }
    }

    public float GetNormalizedAmountOfRemaningLifes()
    {
        return (float)currentLifesAmount / (float)maxLifesAmount;
    }

    private IEnumerator PlayerCrashRoutine()
    {
        playerCrahsed = true;
        OnPlayerCrash?.Invoke();        
        yield return new WaitForSeconds(crashRoutineWait);        
        RemoveOneLife();        
    }


    
}
