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
        playerController.OnLifeRemove += PlayerController_OnLifeRemove;
    }

    private void OnDestroy()
    {
        playerController.OnLifeRemove -= PlayerController_OnLifeRemove;
    }

    private void PlayerController_OnLifeRemove(object sender, EventArgs e)
    {
        playerController.transform.position = startPoint.position;
    }
}
