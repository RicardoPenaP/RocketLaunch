using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionsData 
{
    private StelarSystem[] stelarSystems;

    public MissionsData(StelarSystem[] stelarSystems)
    {
        this.stelarSystems = stelarSystems;
    }

    public StelarSystem[] GetStelarSystems()
    {
        return stelarSystems;
    }
   
}
