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

    public float GetLandingAngle()
    {
        return Vector3.Angle(transform.up,Vector3.right);
    }
}
