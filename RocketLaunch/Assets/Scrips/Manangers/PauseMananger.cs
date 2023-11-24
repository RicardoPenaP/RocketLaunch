using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void OnDestroy()
    {
        if (PauseMenu.Instance)
        {
            PauseMenu.Instance.OnMenuOpened -= PauseMenu_OnMenuOpened;
            PauseMenu.Instance.OnMenuClosed -= PauseMenu_OnMenuClosed;
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
}
