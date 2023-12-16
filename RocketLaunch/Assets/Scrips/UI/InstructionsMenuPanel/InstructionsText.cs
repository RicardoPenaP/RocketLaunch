using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InstructionsText : MonoBehaviour
{
    [Header("Instructions Text")]
    [SerializeField] private TextMeshProUGUI instructionsText;

    private void Start()
    {
        InstructionsButton.OnAnyInstructionsButtonPressed += InstructionsButton_OnAnyInstructionsButtonPressed;
        if (InstructionsMenu.Instance)
        {
            InstructionsMenu.Instance.OnMenuOpened += InstructionsMenu_OnMenuOpened;
        }
    }

    private void OnDestroy()
    {
        InstructionsButton.OnAnyInstructionsButtonPressed -= InstructionsButton_OnAnyInstructionsButtonPressed;
        if (InstructionsMenu.Instance)
        {
            InstructionsMenu.Instance.OnMenuOpened -= InstructionsMenu_OnMenuOpened;
        }
    }

    private void InstructionsButton_OnAnyInstructionsButtonPressed(string instructionsText)
    {
        this.instructionsText.text = instructionsText;
    }

    private void InstructionsMenu_OnMenuOpened()
    {
        this.instructionsText.text = "";
    }
}
