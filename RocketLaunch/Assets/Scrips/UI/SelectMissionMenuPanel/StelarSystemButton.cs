using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class StelarSystemButton : MonoBehaviour
{
    static public event EventHandler<StelarSystemID> OnAnyStelarSystemButtonPressed;
    [Header("Stelar System Button")]   
    [SerializeField] private TextMeshProUGUI stellarSystemNameText;
    [SerializeField] private Button button;
    [SerializeField] private Image selectedImage;
    [SerializeField] private Image lockedImage;

    private StelarSystemID stelarSystemID;

    private void Awake()
    {
        if (button)
        {
            button.onClick.AddListener(Button_OnClick);
        }
        OnAnyStelarSystemButtonPressed += StelarSystemButton_OnAnyStelarSystemButtonPressed;
    }

    private void OnDestroy()
    {
        if (button)
        {
            button.onClick.RemoveListener(Button_OnClick);
        }

        OnAnyStelarSystemButtonPressed -= StelarSystemButton_OnAnyStelarSystemButtonPressed;
    }

    private void Button_OnClick()
    {
        OnAnyStelarSystemButtonPressed?.Invoke(this, stelarSystemID);
    }

    private void StelarSystemButton_OnAnyStelarSystemButtonPressed(object sender, StelarSystemID stelarSystemID)
    {
        selectedImage.gameObject.SetActive(false);
        if (stelarSystemID == this.stelarSystemID)
        {
            SetStelarSystemButtonSelected(true);
        }
    }

    public void SetStelarSystemButtonSelected(bool state)
    {
        selectedImage.gameObject.SetActive(state);
    }

    public void SetStellarSystemNameText(string text)
    {
        stellarSystemNameText.text = text;
    }

    public void SetStellarSystemLocked(bool state)
    {
        if (state)
        {
            SetStellarSystemNameText("???");
        }        
        button.interactable = !state;
        lockedImage.gameObject.SetActive(state);
    }

    public void SetStelarSystemID(StelarSystemID stelarSystemID)
    {
        this.stelarSystemID = stelarSystemID;
    }
}
