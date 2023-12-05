using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StelarSystemButton : MonoBehaviour
{
    static public event EventHandler OnAnyStelarSystemButtonPressed;
    [Header("Stelar System Button")]
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
        OnAnyStelarSystemButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void StelarSystemButton_OnAnyStelarSystemButtonPressed(object sender, EventArgs e)
    {
        selectedImage.gameObject.SetActive(false);
        if ((sender as StelarSystemButton) == this)
        {
            selectedImage.gameObject.SetActive(true);
        }
    }
}
