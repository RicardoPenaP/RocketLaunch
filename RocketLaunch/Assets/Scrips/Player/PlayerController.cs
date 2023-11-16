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

    //For testing
    [SerializeField] private int currentLifesAmount;

    public bool IsAlive { get; private set; }
    private void Awake()
    {
        playerInmune = GetComponent<PlayerInmune>();
        currentLifesAmount = maxLifesAmount;
        IsAlive = true;
        OnLifeRemove += PlayerReset;
    }

    private void Start()
    {
        SetStartPlatform();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<ObstacleController>(out ObstacleController obstacle))
        {
            if (playerInmune.IsInmune)
            {
                return;
            }
            RemoveOneLife();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (playerInmune.IsInmune)
        {
            return;
        }

        if (collision.transform.TryGetComponent<ObstacleController>(out ObstacleController obstacle))
        {           
            RemoveOneLife();
        }
    }

    private void OnDestroy()
    {
        OnLifeRemove -= PlayerReset;
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

    public void SetLastPlatformReached(LevelPlatform levelPlatform)
    {
        if (lastPlatformReached != levelPlatform)
        {
            lastPlatformReached = levelPlatform;
        }
    }

}
