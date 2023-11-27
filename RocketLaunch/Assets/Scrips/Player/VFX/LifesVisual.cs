using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesVisual : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();

        if (playerController)
        {
            playerController.OnCurrentLifesChange += PlayerController_OnCurrentLifesChange;
        }
        
    }

    private void OnDestroy()
    {
        if (playerController)
        {
            playerController.OnCurrentLifesChange += PlayerController_OnCurrentLifesChange;
        }
    }

    private void PlayerController_OnCurrentLifesChange(int currentLifesAmount)
    {
        foreach (Transform childs in transform)
        {
            childs.gameObject.SetActive(false);
        }

        for (int i = 0; i < currentLifesAmount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
