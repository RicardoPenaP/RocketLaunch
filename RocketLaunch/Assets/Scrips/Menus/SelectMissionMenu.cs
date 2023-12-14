using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectMissionMenu : Menu<SelectMissionMenu>
{
    [Header("Select Mission Menu")]
    [SerializeField] private Button goBackButton;

    public event Action OnGoBackButtonPressed;
    public event Action<StelarSystem[]> OnLoadStelarSystemsData;

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
        if (PlayMenu.Instance)
        {
            PlayMenu.Instance.OnSelectMissionButtonPressed += PlayMenu_OnSelectMissionButtonPressed;
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

        if (PlayMenu.Instance)
        {
            PlayMenu.Instance.OnSelectMissionButtonPressed -= PlayMenu_OnSelectMissionButtonPressed;
        }
    }

    private void GoBackButton_OnClick()
    {
        OnGoBackButtonPressed?.Invoke();
        CloseMenu();
    }

    private void PlayMenu_OnSelectMissionButtonPressed()
    {
        if (MissionMananger.Instance)
        {
            OnLoadStelarSystemsData?.Invoke(MissionMananger.Instance.GetStelarSystems());
        }
        
        OpenMenu();
    }
}
