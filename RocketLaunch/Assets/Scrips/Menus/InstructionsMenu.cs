using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InstructionsMenu : Menu<InstructionsMenu>
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
            MainMenu.Instance.OnInstructionsButtonPressed += MainMenu_OnInstructionsButtonPressed;
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
            MainMenu.Instance.OnInstructionsButtonPressed -= MainMenu_OnInstructionsButtonPressed;
        }
    }

    private void MainMenuButton_OnClick()
    {
        OnMainMenuButtonPressed?.Invoke();
        CloseMenu();
    }

    private void MainMenu_OnInstructionsButtonPressed()
    {
        OpenMenu();
    }

}
