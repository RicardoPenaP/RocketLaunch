using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelMananger : MonoBehaviour
{
    [Header("Level Mananger")]
    [SerializeField] private Transform startPoint;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Start()
    {
        playerController.OnPlayerReset += PlayerController_OnPlayerReset;
    }

    private void OnDestroy()
    {
        playerController.OnPlayerReset -= PlayerController_OnPlayerReset;
    }

    private void PlayerController_OnPlayerReset(object sender, EventArgs e)
    {
        playerController.transform.position = startPoint.position;
    }
}
