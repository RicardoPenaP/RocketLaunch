using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseMananger : MonoBehaviour
{
    private const float defaultTimeScale = 1;
    private const float pausedTimeScale = 0;
    private void Start()
    {
        if (PauseMenu.Instance)
        {
            PauseMenu.Instance.OnMenuOpened += PauseMenu_OnMenuOpened;
            PauseMenu.Instance.OnMenuClosed += PauseMenu_OnMenuClosed;
        }

        if (GameOverMenu.Instance)
        {
            GameOverMenu.Instance.OnMenuOpened += GameOver_OnMenuOpened;
            GameOverMenu.Instance.OnMenuClosed += GameOver_OnMenuClosed;
        }
    }

    private void OnDestroy()
    {        
        if (PauseMenu.Instance)
        {
            PauseMenu.Instance.OnMenuOpened -= PauseMenu_OnMenuOpened;
            PauseMenu.Instance.OnMenuClosed -= PauseMenu_OnMenuClosed;
        }

        if (GameOverMenu.Instance)
        {
            GameOverMenu.Instance.OnMenuOpened -= GameOver_OnMenuOpened;
            GameOverMenu.Instance.OnMenuClosed -= GameOver_OnMenuClosed;
        }
    }

    private void PauseMenu_OnMenuOpened()
    {
        Time.timeScale = pausedTimeScale;
    }

    private void PauseMenu_OnMenuClosed()
    {
        Time.timeScale = defaultTimeScale;
    }

    private void GameOver_OnMenuOpened()
    {
        Time.timeScale = pausedTimeScale;
    }

    private void GameOver_OnMenuClosed()
    {
        Time.timeScale = defaultTimeScale;
    }

    //private void Menu_OnAnyMenuOpened(object sender, EventArgs e)
    //{
    //    Time.timeScale = pausedTimeScale;
    //}

    //private void Menu_OnAnyMenuClosed(object sender, EventArgs e)
    //{
    //    Time.timeScale = defaultTimeScale;
    //}
}
