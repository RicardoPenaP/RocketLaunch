using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu<MainMenu>
{
    [Header("Main Menu")]
    [Header("Buttons references")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button instrucctionsButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;


    protected override void Awake()
    {
        base.Awake();

        exitButton.onClick.AddListener(ExitButton_OnClick);
    }

    private void ExitButton_OnClick()
    {
        Application.Quit();
    }
}
