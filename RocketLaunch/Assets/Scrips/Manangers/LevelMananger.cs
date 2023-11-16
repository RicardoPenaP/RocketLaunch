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

    private PlayerController playerController;

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
        playerController.OnDie += PlayerController_OnPlayerDie;
    }

    private void OnDestroy()
    {
        if (playerController)
        {
            playerController.OnDie -= PlayerController_OnPlayerDie;
        }
    }

    public void PlayerController_OnPlayerDie(object sender, EventArgs e)
    {
        SceneManagement.ReloadCurrentScene();
    }

}
