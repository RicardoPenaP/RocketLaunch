using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionButton : MonoBehaviour
{
    private TextMeshProUGUI buttonText;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetMissionButtonIndex(StelarSystemID stelarSystemID, int missionIndex)
    {
        buttonText.text = $"Mission {(int)stelarSystemID}- {missionIndex}";
    }
}
