using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GameSceneManagement;

public class GameOverMenu : Menu<GameOverMenu>
{
    [Header("Game Over Menu")]
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;

    protected override void Awake()
    {
        base.Awake();

        if (playAgainButton)
        {
            playAgainButton.onClick.AddListener(PlayAgainButton_OnClick);
        }

        if (mainMenuButton)
        {
            mainMenuButton.onClick.AddListener(MainMenuButton_OnClick);
        }
    }

    private void Start()
    {
        if (LevelMananger.Instance)
        {
            LevelMananger.Instance.OnGameOver += LevelMananger_OnGameOver;
        }

        gameObject.SetActive(false);
        menuOpened = false;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (LevelMananger.Instance)
        {
            LevelMananger.Instance.OnGameOver -= LevelMananger_OnGameOver;
        }

        if (playAgainButton)
        {
            playAgainButton.onClick.RemoveListener(PlayAgainButton_OnClick);
        }

        if (mainMenuButton)
        {
            mainMenuButton.onClick.RemoveListener(MainMenuButton_OnClick);
        }
    }

    private void PlayAgainButton_OnClick()
    {
        CloseMenu(PlayAgain);
    }

    private void MainMenuButton_OnClick()
    {
        //Go back to main menu scene
        Debug.Log("Still working on that");
    }

    private void PlayAgain()
    {
        SceneManagement.ReloadCurrentScene();
    }

    private void LevelMananger_OnGameOver()
    {
        OpenMenu();
    }
}
