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

        public string GetLevelNameByNumber(int name)
        {
            foreach (var level in LevelNames)
            {
                if (level.LevelNumber == name)
                    return level.SceneName;
            }

            return null;
        }
    }

    [System.Serializable]
    public class LevelNames
    {
        public string SceneName;
        public int LevelNumber;
    }
}