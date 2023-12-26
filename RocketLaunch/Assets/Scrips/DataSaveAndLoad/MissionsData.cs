using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
