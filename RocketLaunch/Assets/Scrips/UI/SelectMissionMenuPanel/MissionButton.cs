using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionButton : MonoBehaviour
{
    [Header("Mission Button")]
    [SerializeField] private Image lockedImage;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    public void SetMissionButtonIndex(StelarSystemID stelarSystemID, int missionIndex)
    {
        buttonText.text = $"Mission {(int)stelarSystemID}- {missionIndex}";
    }

    public void SetLocked(bool state)
    {
        button.interactable = !state;
        lockedImage.gameObject.SetActive(state);
    }
}
