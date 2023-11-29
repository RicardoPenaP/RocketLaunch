using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LandingPlatform : MonoBehaviour
{
    [SerializeField] private Transform preLandingPosition;

    public event Action OnPlatformInsideScreen;
    public event Action<Vector3> OnPlatformOutsideScreen;

    private bool platformOnScreen = false;

    private void Update()
    {
        ItsPlatformOnScreen();
    }

    private void ItsPlatformOnScreen()
    {
        Vector2 platformScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        float screenWidht = Screen.width;
        float screenHeight = Screen.height;

        if (platformScreenPosition.x >= 0 && platformScreenPosition.x <= screenWidht && platformScreenPosition.y >= 0 && platformScreenPosition.y <= screenHeight)
        {
            platformOnScreen = true;

        }
        else
        {
            platformOnScreen = false;
        }

        if (platformOnScreen)
        {
            OnPlatformInsideScreen?.Invoke();
        }
        else
        {
            OnPlatformOutsideScreen?.Invoke(transform.position);
        }
    }

    public Vector3 GetPreLandingPosition()
    {
        return preLandingPosition.position;
    }

    public float GetLandingAngle()
    {
        return Vector3.Angle(transform.up,Vector3.right);
    }
}
