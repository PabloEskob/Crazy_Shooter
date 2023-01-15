using UnityEditor; 
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "New_GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public LevelNames[] LevelConfigs;
    }

    [System.Serializable]
    public class LevelNames
    {
        public string SceneName;
    }
}