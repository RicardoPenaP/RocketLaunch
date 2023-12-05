using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StelarSystemPanel : MonoBehaviour
{
    private StelarSystemButton[] stelarSystemButtons;

    private void Awake()
    {
        stelarSystemButtons = GetComponentsInChildren<StelarSystemButton>();
    }

    private void Start()
    {
        if (SelectMissionMenu.Instance)
        {
            SelectMissionMenu.Instance.OnLoadStelarSystemsData += SelectMissionMenu_OnLoadStelarSystemsData;
        }
    }

    private void OnDestroy()
    {
        if (SelectMissionMenu.Instance)
        {
            SelectMissionMenu.Instance.OnLoadStelarSystemsData -= SelectMissionMenu_OnLoadStelarSystemsData;
        }
    }

    private void SelectMissionMenu_OnLoadStelarSystemsData(StelarSystem[] stelarSystems)
    {

    }
}
