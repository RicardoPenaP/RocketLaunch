using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InstructionsButton : MonoBehaviour
{
    public static event Action<string> OnAnyInstructionsButtonPressed;
    [Header("Instructions Button")]
    [SerializeField, TextArea(minLines: 2, maxLines: 5)] private string instructionsText;
    [SerializeField] private Button instructionsButton;


    private void Awake()
    {
        if (instructionsButton)
        {
            instructionsButton.onClick.AddListener(InstructionsButton_OnClick);
        }
    }

    private void OnDestroy()
    {
        if (instructionsButton)
        {
            instructionsButton.onClick.RemoveListener(InstructionsButton_OnClick);
        }
    }

    private void InstructionsButton_OnClick()
    {
        OnAnyInstructionsButtonPressed?.Invoke(instructionsText);
    }
}
