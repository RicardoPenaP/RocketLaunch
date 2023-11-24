using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameSceneManagement;

public class LevelMananger : MonoBehaviour
{
    [Header("Level Mananger")]
    [SerializeField] private GameScene nextLevel;
    public static LevelMananger Instance { get; private set; }

    public event Action OnGameOver;

    private PlayerController playerController;
    private PlayerLandingController playerLandingController;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        
        playerController = FindAnyObjectByType<PlayerController>();
        playerLandingController = playerController.GetComponent<PlayerLandingController>();

        if (playerController)
        {
            playerController.OnDie += PlayerController_OnDie;
        }
        if (playerLandingController)
        {
            playerLandingController.OnLandingFinished += PlayerLandingController_OnLandingFinished;
        }
    }

    private void OnDestroy()
    {
        if (playerController)
        {
            playerController.OnDie -= PlayerController_OnDie;
        }
        if (playerLandingController)
        {
            playerLandingController.OnLandingFinished += PlayerLandingController_OnLandingFinished;
        }
    }

    private void PlayerController_OnDie(object sender, EventArgs e)
    {
        OnGameOver?.Invoke();
    }

    private void PlayerLandingController_OnLandingFinished(object sender, EventArgs e)
    {
        //level finished behaviour
        Debug.Log("Level finished");
    }

}
