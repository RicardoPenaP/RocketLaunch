using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CreditsMenu : Menu<CreditsMenu>
{
    [Header("Credits Menu")]
    [SerializeField] private Button mainMenuButton;

    public event Action OnMainMenuButtonPressed;

    protected override void Awake()
    {
        base.Awake();
        if (mainMenuButton)
        {
            mainMenuButton.onClick.AddListener(MainMenuButton_OnClick);
        }
    }

    private void Start()
    {
        if (MainMenu.Instance)
        {
            MainMenu.Instance.OnCreditsButtonPressed += MainMenu_OnCreditsButtonPressed;
        }
        gameObject.SetActive(false);
        menuOpened = false;
    }

    private void OnDestroy()
    {
        if (mainMenuButton)
        {
            mainMenuButton.onClick.RemoveListener(MainMenuButton_OnClick);
        }

        if (MainMenu.Instance)
        {
            MainMenu.Instance.OnCreditsButtonPressed -= MainMenu_OnCreditsButtonPressed;
        }
    }

    private void MainMenuButton_OnClick()
    {
        OnMainMenuButtonPressed?.Invoke();
        CloseMenu();
    }

    private void MainMenu_OnCreditsButtonPressed()
    {
        OpenMenu();
    }

}
