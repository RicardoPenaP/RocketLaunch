using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSceneManagement;


[CreateAssetMenu(fileName = "NewMissionExperienceData", menuName = "Mission Experience Data")]
public class MissionsExperienceData : ScriptableObject
{
    [Header("Missions Experience Data")]
    [SerializeField] private Dictionary<GameScene,float> missionsExperienceDictionary;

    public float GetMissionExperience(GameScene gameScene)
    {
        if (missionsExperienceDictionary.ContainsKey(gameScene))
        {
            return missionsExperienceDictionary[gameScene];
        }

        return 0f;
    }
}
