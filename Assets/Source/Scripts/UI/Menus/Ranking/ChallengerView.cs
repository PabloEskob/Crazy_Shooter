using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Ranking
{
    public class ChallengerView : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _score;
        [SerializeField] private Image _image;
        [SerializeField] private Text _rank;
        [SerializeField] private Text _kill;
        [SerializeField] private Text _death;
        [SerializeField] private Text _level;

        private Challenger _challenger;

        public void Render(Challenger challenger)
        {
            _challenger = challenger;
            _rank.text = challenger.Rank.ToString();
            _name.text = challenger.Name;
            _score.text = challenger.KillCount.ToString();
            _image.sprite = challenger.Avatar;
        }

        public void SetName(string name)
        {
            _name.text = name;
        }

        public void SetScore(int score)
        {
            _score.text = score.ToString();
        }
    }
}
