using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "New_GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public LevelNames[] LevelNames;

        public string GetLevelNameByNumber(int number)
        {
            foreach (var level in LevelNames)
            {
                if (level.LevelNumber == number)
                    return level.SceneName;
            }

            return null;
        }

        public int GetLevelNumberByName(string name)
        {
            foreach (var level in LevelNames)
            {
                if(level.SceneName == name)
                    return level.LevelNumber;
            }

            return 0;
        }
    }

    [System.Serializable]
    public class LevelNames
    {
        public string SceneName;
        public int LevelNumber;
    }
}