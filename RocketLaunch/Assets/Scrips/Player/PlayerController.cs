using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Player Controller")]
    [SerializeField] private int maxLifesAmount = 3;    

    public event EventHandler OnLifeRemove;
    public event EventHandler OnDie;
    public event EventHandler OnPlayerReset;

    private PlayerInmune playerInmune;
    private LevelPlatform lastPlatformReached;
    private PlayerCollisionHandler playerCollisionHandler;
    private int currentLifesAmount;
    public bool IsAlive { get; private set; }

    private void Awake()
    {
        playerInmune = GetComponent<PlayerInmune>();
        playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
        currentLifesAmount = maxLifesAmount;
        IsAlive = true;
        OnLifeRemove += PlayerReset;
    }

    private void Start()
    {
        SetStartPlatform();
        playerCollisionHandler.OnCollisionEnterWithObject += PlayerCollisionHandler_OnCollisionEnterWithObject;
    }

    private void OnDestroy()
    {
        OnLifeRemove -= PlayerReset;
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

        if (currentLifesAmount <= 0)
        {
            currentLifesAmount = 0;
            Die();
            IsAlive = false;
        }

        OnLifeRemove?.Invoke(this, EventArgs.Empty);
    }

    private void Die()
    {
        OnDie?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerReset(object sender,EventArgs e)
    {
        if (!IsAlive)
        {
            return;
        }
        transform.position = lastPlatformReached.GetSpawnPoint().position;
        OnPlayerReset?.Invoke(sender, e);
    }

    private void PlayerCollisionHandler_OnCollisionEnterWithObject(object sender, EventArgs e)
    {
        switch (e)
        {
            case PlayerCollisionHandler.CollisionInfo<ObstacleController> collisionInfo:
                RemoveOneLife();
                break;

            case PlayerCollisionHandler.CollisionInfo<LevelPlatform> collisionInfo:
                if (collisionInfo.collisionObject.GetPlatformType() == LevelPlatform.PlatformType.SavePoint)
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

    
}
