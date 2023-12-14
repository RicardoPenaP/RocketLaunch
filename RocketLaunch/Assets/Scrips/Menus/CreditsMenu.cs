using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CreditsMenu : Menu<CreditsMenu>
{
    [Header("Credits Menu")]
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
            MainMenu.Instance.OnCreditsButtonPressed += MainMenu_OnCreditsButtonPressed;
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
            MainMenu.Instance.OnCreditsButtonPressed -= MainMenu_OnCreditsButtonPressed;
        }
    }

    private void GoBackButton_OnClick()
    {
        OnGoBackButtonPressed?.Invoke();
        CloseMenu();
    }

    private void MainMenu_OnCreditsButtonPressed()
    {
        OpenMenu();
    }

}
