using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainMenuCameraMananger : MonoBehaviour
{
    [Header("Main Menu Camera Mananager")]
    [SerializeField] private CinemachineVirtualCamera defaulVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera upgradeRocketVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera selectMissionVirtualCamera;

    private void Start()
    {
        if (UpgradeRocketMenu.Instance)
        {
            UpgradeRocketMenu.Instance.OnMenuOpened += UpgradeRocketMenu_OnMenuOpened;
            UpgradeRocketMenu.Instance.OnGoBackButtonPressed += UpgradeRocketMenu_OnGoBackButtonPressed;
        }

        if (SelectMissionMenu.Instance)
        {
            SelectMissionMenu.Instance.OnMenuOpened += SelectMissionMenu_OnMenuOpened;
            SelectMissionMenu.Instance.OnGoBackButtonPressed += SelectMissionMenu_OnGoBackButtonPressed;
        }
    }

    private void OnDestroy()
    {
        if (UpgradeRocketMenu.Instance)
        {
            UpgradeRocketMenu.Instance.OnMenuOpened -= UpgradeRocketMenu_OnMenuOpened;
            UpgradeRocketMenu.Instance.OnGoBackButtonPressed -= UpgradeRocketMenu_OnGoBackButtonPressed;
        }

        if (SelectMissionMenu.Instance)
        {
            SelectMissionMenu.Instance.OnMenuOpened -= SelectMissionMenu_OnMenuOpened;
            SelectMissionMenu.Instance.OnGoBackButtonPressed -= SelectMissionMenu_OnGoBackButtonPressed;
        }
    }

    private void UpgradeRocketMenu_OnMenuOpened()
    {
        defaulVirtualCamera.gameObject.SetActive(false);
        upgradeRocketVirtualCamera.gameObject.SetActive(true);
    }

    private void UpgradeRocketMenu_OnGoBackButtonPressed()
    {
        upgradeRocketVirtualCamera.gameObject.SetActive(false);
        defaulVirtualCamera.gameObject.SetActive(true);        
    }

    private void SelectMissionMenu_OnMenuOpened()
    {
        defaulVirtualCamera.gameObject.SetActive(false);
        selectMissionVirtualCamera.gameObject.SetActive(true);
    }

    private void SelectMissionMenu_OnGoBackButtonPressed()
    {
        selectMissionVirtualCamera.gameObject.SetActive(false);
        defaulVirtualCamera.gameObject.SetActive(true);
        
    }
}
