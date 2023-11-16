using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingController : MonoBehaviour
{
    [Header("Player Landing Controller")]
    [SerializeField] private float landingMinDistance = 1f;
    [SerializeField] private float prelandingDuration = 1f;
    [SerializeField] private float landingDuration = 5f;
    [SerializeField] private LayerMask platformsLayerMask;

    private LevelPlatform landingPlatform;

    private void Update()
    {
        if (TryGetLandingPlatform())
        {

        }
    }

    private bool TryGetLandingPlatform()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, landingMinDistance, platformsLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.TryGetComponent<LevelPlatform>(out LevelPlatform levelPlatform))
            {
                if (levelPlatform.GetPlatformType() == LevelPlatform.PlatformType.Landing)
                {
                    landingPlatform = levelPlatform;
                    return true;
                }
            }
        }

        landingPlatform = null;
        return false;
    }
}
