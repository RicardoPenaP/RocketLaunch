using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsMenu : Menu<SettingsMenu>
{
    [Header("Settings Menu")]
    [SerializeField] private Button goBackButton;

    public event Action OnGoBackButtonPressed;

    protected override void Awake()
    {
        base.Awake();
        if (goBackButton)
        {
            goBackButton.onClick.AddListener(GoBackButton_OnClick);
        }
    }

    private void Start()
    {
        if (MainMenu.Instance)
        {
            MainMenu.Instance.OnSettingsButtonPressed += MainMenu_OnSettingsButtonPressed;
        }
        gameObject.SetActive(false);
        menuOpened = false;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (goBackButton)
        {
            goBackButton.onClick.RemoveListener(GoBackButton_OnClick);
        }

        if (MainMenu.Instance)
        {
            MainMenu.Instance.OnSettingsButtonPressed -= MainMenu_OnSettingsButtonPressed;
        }
    }

    private void GoBackButton_OnClick()
    {
        OnGoBackButtonPressed?.Invoke();
        CloseMenu();
    }

    private void MainMenu_OnSettingsButtonPressed()
    {
        OpenMenu();
    }

}
