using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSceneManagement;

public class LevelCompletedMenu : Menu<LevelCompletedMenu>
{
    [Header("Level Completed Menu")]
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainMenuButton;

    protected override void Awake()
    {
        base.Awake();

        if (playAgainButton)
        {
            playAgainButton.onClick.AddListener(PlayAgainButton_OnClick);
        }

        if (nextLevelButton)
        {
            nextLevelButton.onClick.AddListener(NextLevelButton_OnClick);
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
            LevelMananger.Instance.OnLevelCompleted += LevelMananger_OnLevelCompleted;
        }

        gameObject.SetActive(false);
        menuOpened = false;
    }

    private void OnDestroy()
    {
        if (LevelMananger.Instance)
        {
            LevelMananger.Instance.OnGameOver -= LevelMananger_OnLevelCompleted;
        }

        if (playAgainButton)
        {
            playAgainButton.onClick.RemoveListener(PlayAgainButton_OnClick);
        }

        if (nextLevelButton)
        {
            nextLevelButton.onClick.RemoveListener(NextLevelButton_OnClick);
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

    private void NextLevelButton_OnClick()
    {
        //Go to next level scene
        Debug.Log("Still working on that");
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

    private void LevelMananger_OnLevelCompleted()
    {
        OpenMenu();
    }
}
