using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DeleteSavedDataPanel : MonoBehaviour
{
    public static event Action OnDeleteAllButtonPressed;

    [Header("Delete Saved Data Panel")]
    [SerializeField] private Button deleteAllButton;
    [SerializeField] private Button cancelButton;


    private void Awake()
    {
        if (deleteAllButton)
        {
            deleteAllButton.onClick.AddListener(DeleteAllButton_OnClick);
        }

        if (cancelButton)
        {
            cancelButton.onClick.AddListener(CancelButton_OnClick);
        }
    }

    private void Start()
    {
        if (SettingsMenu.Instance)
        {
            SettingsMenu.Instance.OnDeleteSavedDataButtonPressed += SettingsMenu_OnDeleteSavedDataButtonPressed;
            SettingsMenu.Instance.OnMenuClosed += SettingsMenu_OnMenuClosed;
        }

        ClosePanel();
    }

    private void OnDestroy()
    {
        if (deleteAllButton)
        {
            deleteAllButton.onClick.RemoveListener(DeleteAllButton_OnClick);
        }

        if (cancelButton)
        {
            cancelButton.onClick.RemoveListener(CancelButton_OnClick);
        }

        if (SettingsMenu.Instance)
        {
            SettingsMenu.Instance.OnDeleteSavedDataButtonPressed -= SettingsMenu_OnDeleteSavedDataButtonPressed;
            SettingsMenu.Instance.OnMenuClosed -= SettingsMenu_OnMenuClosed;
        }
    }

    private void DeleteAllButton_OnClick()
    {
        OnDeleteAllButtonPressed?.Invoke();
        ClosePanel();
    }

    private void CancelButton_OnClick()
    {        
        ClosePanel();
    }

    private void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void SettingsMenu_OnDeleteSavedDataButtonPressed()
    {
        OpenPanel();
    }

    private void SettingsMenu_OnMenuClosed()
    {
        ClosePanel();
    }
}
