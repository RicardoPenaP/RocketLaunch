using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MissionButton : MonoBehaviour
{
    public static event Action<StelarSystemID, int> OnAnyMissionButtonPressed;
    [Header("Mission Button")]
    [SerializeField] private Image lockedImage;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    private StelarSystemID stelarSystemID;
    private int missionIndex;

    private void Awake()
    {
        if (button)
        {
            button.onClick.AddListener(Button_OnClick);
        }
    }

    private void OnDestroy()
    {
        if (button)
        {
            button.onClick.RemoveListener(Button_OnClick);
        }
    }

    private void Button_OnClick()
    {
        OnAnyMissionButtonPressed?.Invoke(stelarSystemID, missionIndex);
    }

    public void SetMissionButtonIndex(StelarSystemID stelarSystemID, int missionIndex)
    {
        this.stelarSystemID = stelarSystemID;
        this.missionIndex = missionIndex;
        buttonText.text = $"Mission {(int)stelarSystemID}- {missionIndex}";
    }

    public void SetLocked(bool state)
    {
        button.interactable = !state;
        lockedImage.gameObject.SetActive(state);
    }
}
