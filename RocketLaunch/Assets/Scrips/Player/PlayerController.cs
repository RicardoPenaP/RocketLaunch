using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private const int maximunLifesAmount = 10;

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

    private int currentLifesAmount;
    private bool playerCrahsed = false;
    public bool IsAlive { get; private set; }

    private void Awake()
    {
        playerInmune = GetComponent<PlayerInmune>();
        playerCollisionHandler = GetComponent<PlayerCollisionHandler>();        
        IsAlive = true;        
    }

    private void Start()
    {
        currentLifesAmount = maxLifesAmount;
        OnCurrentLifesChange?.Invoke(currentLifesAmount);
        SetStartPlatform();
        playerCollisionHandler.OnCollisionEnterWithObject += PlayerCollisionHandler_OnCollisionEnterWithObject;
    }

    private void OnDestroy()
    {       
        playerCollisionHandler.OnCollisionEnterWithObject -= PlayerCollisionHandler_OnCollisionEnterWithObject;
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

    private void PlayerCollisionHandler_OnCollisionEnterWithObject(object sender, EventArgs e)
    {
        switch (e)
        {
            case PlayerCollisionHandler.CollisionInfo<ObstacleController> collisionInfo:
                if (playerInmune.IsInmune)
                {
                    break;
                }
                if (playerCrahsed)
                {
                    break;
                }
                StartCoroutine(PlayerCrashRoutine());               
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

    public void SetLastPlatformReached(LevelPlatform levelPlatform)
    {
        if (lastPlatformReached != levelPlatform)
        {
            lastPlatformReached = levelPlatform;
        }
    }

    private IEnumerator PlayerCrashRoutine()
    {
        playerCrahsed = true;
        OnPlayerCrash?.Invoke();        
        yield return new WaitForSeconds(crashRoutineWait);        
        RemoveOneLife();        
    }
    
}
