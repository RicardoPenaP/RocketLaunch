using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSceneManagement;
using System;


[CreateAssetMenu(fileName = "NewMissionExperienceData", menuName = "Mission Experience Data")]
public class MissionsExperienceData : ScriptableObject
{
    [Serializable]
    public class MissionExperience
    {
        public GameScene gameScene;
        public float experience;
    }

    [Header("Missions Experience Data")]
    [SerializeField] private List<MissionExperience> missionsExperienceList;

    public float GetMissionExperience(GameScene gameScene)
    {        
        foreach (MissionExperience missionExperience in missionsExperienceList)
        {
            if (missionExperience.gameScene == gameScene)
            {
                return missionExperience.experience;
            }
        }

        return 0f;
    }
}
