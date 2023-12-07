using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStatsMananger : MonoBehaviour
{
    public const int STATS_AMOUNT = 6;
    public enum StatType { MainEngine, SideEngine, CoolingSystem, LandingSystem, PickupSystem, ShieldSystem}
    public struct Stat
    {
        public StatType statType;
        public int statLevel;

        public Stat(StatType statType)
        {
            this.statType = statType;
            statLevel = 1;
        }

        public void LevelUp()
        {

        }

        public void LevelDown()
        {
           
        }
    }
    public static RocketStatsMananger Instance { get; private set; }

    [Header("Rocket Stats Mananger")]
    [SerializeField] private int statsPointsGivenPerLevelUp = 6;

    private Stat[] stats = new Stat[STATS_AMOUNT];

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        for (int i = 0; i < stats.Length; i++)
        {
            
        }
    }

    private void Start()
    {
        if (RocketLevelMananger.Instance)
        {
            RocketLevelMananger.Instance.OnRocketLevelUp += RocketLevelMananger_OnRocketLevelUp;
        }
    }

    private void OnDestroy()
    {
        if (RocketLevelMananger.Instance)
        {
            RocketLevelMananger.Instance.OnRocketLevelUp -= RocketLevelMananger_OnRocketLevelUp;
        }
    }

    private void RocketLevelMananger_OnRocketLevelUp()
    {

    }
}
