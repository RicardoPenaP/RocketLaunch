using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InstructionsMenu : Menu<InstructionsMenu>
{
    [Header("Instructions Menu")]
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
            MainMenu.Instance.OnInstructionsButtonPressed += MainMenu_OnInstructionsButtonPressed;
        }
        gameObject.SetActive(false);
        menuOpened = false;
    }

    private void OnDestroy()
    {
        if (goBackButton)
        {
            goBackButton.onClick.RemoveListener(GoBackButton_OnClick);
        }

        if (MainMenu.Instance)
        {
            MainMenu.Instance.OnInstructionsButtonPressed -= MainMenu_OnInstructionsButtonPressed;
        }
    }

    private void GoBackButton_OnClick()
    {
        OnGoBackButtonPressed?.Invoke();
        CloseMenu();
    }

    private void MainMenu_OnInstructionsButtonPressed()
    {
        OpenMenu();
    }

}
