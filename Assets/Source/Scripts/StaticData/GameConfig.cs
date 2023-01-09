using UnityEngine;

namespace Source.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "New_GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Min(1)] public int RepeatGameFromLevel = 1;
        public LevelConfig[] LevelConfigs;
    }

    [System.Serializable]
    public class LevelConfig
    {
        public string SceneName;
    }
}