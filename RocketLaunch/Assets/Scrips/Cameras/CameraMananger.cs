using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMananger : MonoBehaviour
{
    [Header("Camera Mananger")]
    [SerializeField] private GameObject stillCinemachineVirtualCamera;
    [SerializeField] private GameObject movingCinemachineVirtualCamera;
    [SerializeField,Min(0f)] private float changeCameraTime = 3f;

    private GameObject targetCamera;
    private GameObject currentCamera;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        if (playerMovement)
        {
            playerMovement.OnStartMovingUpwards += PlayerMovement_OnStartMovingUpwards;
            playerMovement.OnStopMovingUpwards += PlayerMovement_OnStopMovingUpwards;
        }
      
    }

    private void OnDestroy()
    {
        if (playerMovement)
        {
            playerMovement.OnStartMovingUpwards -= PlayerMovement_OnStartMovingUpwards;
            playerMovement.OnStopMovingUpwards -= PlayerMovement_OnStopMovingUpwards;
        }
    }

    private void PlayerMovement_OnStartMovingUpwards(object sender, EventArgs e)
    {
        StopAllCoroutines();
        stillCinemachineVirtualCamera.SetActive(false);
        movingCinemachineVirtualCamera.SetActive(true);   
    }

    private void PlayerMovement_OnStopMovingUpwards(object sender, EventArgs e)
    {
        StartCoroutine(ChangeCameraRoutine());
    }

    private IEnumerator ChangeCameraRoutine()
    {
        yield return new WaitForSeconds(changeCameraTime);
        movingCinemachineVirtualCamera.SetActive(false);
        stillCinemachineVirtualCamera.SetActive(true);
    }
}
