using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPlatform : MonoBehaviour
{
    [SerializeField] private Transform preLandingPosition;
    public Vector3 GetPreLandingPosition()
    {
        return preLandingPosition.position;
    }
}
