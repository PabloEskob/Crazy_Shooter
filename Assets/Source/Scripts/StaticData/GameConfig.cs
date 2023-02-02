using InfimaGames.LowPolyShooterPack;
using System;
using UnityEngine;

namespace Source.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "New_GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public LevelNames[] LevelNames;

        public string GetLevelNameByNumber(int number)
        {
            foreach (var level in LevelNames)
                if (level.LevelNumber == number)
                    return level.SceneName;

            return null;
        }

        public int GetLevelNumberByName(string name)
        {
            foreach (var level in LevelNames)
                if (level.SceneName == name)
                    return level.LevelNumber;

            return 0;
        }

        public int GetLevelReward(int levelNumber)
        {
            foreach (var level in LevelNames)
                if (level.LevelNumber == levelNumber)
                    return level.SoftReward;

            return 0;
        }

        public Weapon GetExtraReward(int levelNumber)
        {
            foreach (var level in LevelNames)
                if (level.LevelNumber == levelNumber)
                    return level.ExtraReward;

            return null;
        }

        public bool HasSoftReward(int levelNumber)
        {
            foreach (var level in LevelNames)
                if (level.LevelNumber == levelNumber && level.SoftReward != 0)
                    return true;

            return false;
        }

        public bool HasExtraReward(int levelNumber)
        {
            foreach (var level in LevelNames)
                if (level.LevelNumber == levelNumber && level.ExtraReward != null)
                    return true;

            return false;
        }
    }

    [Serializable]
    public class LevelNames
    {
        public string SceneName;
        public int LevelNumber;
        public int SoftReward;
        public Weapon ExtraReward;
    }
}