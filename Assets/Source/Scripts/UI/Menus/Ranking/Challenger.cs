using UnityEngine;

namespace Assets.Source.Scripts.UI.Menus.Ranking
{
    public class Challenger : MonoBehaviour
    {
        private string _name;
        private int _level;
        private int _rank;
        private int _killCount;
        private Sprite _avatar;

        public string Name => _name;
        public int Level => _level;
        public int Rank => _rank;
        public int KillCount => _killCount;
        public Sprite Avatar => _avatar;
    }
}
