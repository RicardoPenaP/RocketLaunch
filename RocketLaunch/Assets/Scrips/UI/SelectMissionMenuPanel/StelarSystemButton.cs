using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StelarSystemButton : MonoBehaviour
{
    static public event EventHandler<StelarSystemID> OnAnyStelarSystemButtonPressed;
    [Header("Stelar System Button")]
    [SerializeField] private StelarSystemID stelarSystemID;
    [SerializeField] private Button button;
    [SerializeField] private Image selectedImage;


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
            selectedImage.gameObject.SetActive(true);
        }
    }
}
