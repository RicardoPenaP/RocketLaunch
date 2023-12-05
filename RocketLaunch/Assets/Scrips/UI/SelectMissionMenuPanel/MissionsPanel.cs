using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MissionsPanel : MonoBehaviour
{
    [Header("Missions Panel")]
    [SerializeField] private MissionButton missionButtonPrefab;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Transform missionsButtonsLayout;

    private void Start()
    {
        StelarSystemButton.OnAnyStelarSystemButtonPressed += StelarSystemButton_OnAnyStelarSystemButtonPressed;
    }

    private void OnDestroy()
    {
        StelarSystemButton.OnAnyStelarSystemButtonPressed -= StelarSystemButton_OnAnyStelarSystemButtonPressed;
    }

    private void StelarSystemButton_OnAnyStelarSystemButtonPressed(object sender, StelarSystemID stelarSystemID)
    {
        EmptyMissionsButtonsLayout();
        if (MissionMananger.Instance)
        {
            StelarSystem selectedStelarSystem = MissionMananger.Instance.GetStelarSystem(stelarSystemID);
            LoadStelarSystemMissions(selectedStelarSystem);
        }
    }

    private void EmptyMissionsButtonsLayout()
    {
        foreach (Transform child in missionsButtonsLayout)
        {
            Destroy(child.gameObject);
        }
    }

    private void LoadStelarSystemMissions(StelarSystem stelarSystem)
    {
        titleText.text = stelarSystem.GetSystemName();
        Mission[] missions = stelarSystem.GetMissions();
        foreach (Mission mission in missions)
        {
            MissionButton missionButton = Instantiate(missionButtonPrefab, missionsButtonsLayout);
            missionButton.SetMissionButtonIndex(stelarSystem.GetStelarSystemID(),mission.GetMissionIndex());
        }

    }


    
}
