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
        for (int i = 0; i < stelarSystems.Length; i++)
        {
            if (stelarSystems[i].GetLocked())
            {
                stelarSystemButtons[i].SetStellarSystemLocked(true);
            }
            else
            {
                stelarSystemButtons[i].SetStellarSystemLocked(false);
                stelarSystemButtons[i].SetStellarSystemNameText(stelarSystems[i].GetSystemName());
                stelarSystemButtons[i].SetStelarSystemID(stelarSystems[i].GetStelarSystemID());
            }
           
           
        }
    }
}
