using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMananger : MonoBehaviour
{
    private enum GameCamera { Still, Moving, Landing}
    [Header("Camera Mananger")]
    [SerializeField] private GameObject stillCinemachineVirtualCamera;
    [SerializeField] private GameObject movingCinemachineVirtualCamera;
    [SerializeField] private GameObject landingCinemachineVirtualCamera;

    [SerializeField,Min(0f)] private float changeToStillCameraTime = 3f;

    private PlayerMovement playerMovement;
    private PlayerLandingController playerLandingController;

    private GameCamera currentCamera;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerLandingController = playerMovement.GetComponent<PlayerLandingController>();
        currentCamera = GameCamera.Moving;
    }

    private void Start()
    {
        if (playerMovement)
        {
            playerMovement.OnStartMovingUpwards += PlayerMovement_OnStartMovingUpwards;
            playerMovement.OnStopMovingUpwards += PlayerMovement_OnStopMovingUpwards;
        }

        if (playerLandingController)
        {
            playerLandingController.OnPreLandingStart += PlayerLandingController_OnPreLandingStart;
        }
    }

    private void OnDestroy()
    {
        if (playerMovement)
        {
            playerMovement.OnStartMovingUpwards -= PlayerMovement_OnStartMovingUpwards;
            playerMovement.OnStopMovingUpwards -= PlayerMovement_OnStopMovingUpwards;
        }

        if (playerLandingController)
        {
            playerLandingController.OnPreLandingStart -= PlayerLandingController_OnPreLandingStart;
        }
    }

    private void SetCurrentGameCamera( GameCamera gameCamera)
    {
        switch (currentCamera)
        {
            case GameCamera.Still:
                stillCinemachineVirtualCamera.SetActive(false);
                break;
            case GameCamera.Moving:
                movingCinemachineVirtualCamera.SetActive(false);
                break;
            case GameCamera.Landing:
                landingCinemachineVirtualCamera.SetActive(false);
                break;
            default:
                break;
        }

        currentCamera = gameCamera;

        switch (currentCamera)
        {
            case GameCamera.Still:
                stillCinemachineVirtualCamera.SetActive(true);
                break;
            case GameCamera.Moving:
                movingCinemachineVirtualCamera.SetActive(true);
                break;
            case GameCamera.Landing:
                landingCinemachineVirtualCamera.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void PlayerMovement_OnStartMovingUpwards(object sender, EventArgs e)
    {
        StopAllCoroutines();
        SetCurrentGameCamera(GameCamera.Moving);  
    }

    private void PlayerMovement_OnStopMovingUpwards(object sender, EventArgs e)
    {
        StartCoroutine(ChangeCameraRoutine());
    }

    private void PlayerLandingController_OnPreLandingStart(object sender, EventArgs e)
    {
        SetCurrentGameCamera(GameCamera.Landing);
    }

    private IEnumerator ChangeCameraRoutine()
    {
        yield return new WaitForSeconds(changeToStillCameraTime);
        SetCurrentGameCamera(GameCamera.Still);
    }
}
