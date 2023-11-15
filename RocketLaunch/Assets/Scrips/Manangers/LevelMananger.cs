using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameSceneManagement;

public class LevelMananger : MonoBehaviour
{
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
        playerController = FindAnyObjectByType<PlayerController>();
        playerController.OnDie += PlayerController_OnPlayerDie;
    }

    private void OnDestroy()
    {
        playerController.OnDie -= PlayerController_OnPlayerDie;
    }

    public void PlayerController_OnPlayerDie(object sender, EventArgs e)
    {
        SceneManagement.ReloadCurrentScene();
    }
}
