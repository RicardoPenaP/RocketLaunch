using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionButtonLayout : MonoBehaviour
{
    private void Awake()
    {
        StelarSystemButton.OnAnyStelarSystemButtonPressed += StelarSystemButton_OnAnyStelarSystemButtonPressed;
    }

    private void OnDestroy()
    {
        StelarSystemButton.OnAnyStelarSystemButtonPressed -= StelarSystemButton_OnAnyStelarSystemButtonPressed;
    }

    private void StelarSystemButton_OnAnyStelarSystemButtonPressed(object sender, StelarSystemID stelarSystemID)
    {
        Vector3 position = transform.position;
        position.y = 0;
        transform.position = position;
    }
}
