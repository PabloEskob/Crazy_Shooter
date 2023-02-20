using System;
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
        [SerializeField] private Image _background;
        [SerializeField] private Color32 _playerTextColor;
        [SerializeField] private Image _playerBackgroundImage;
        [SerializeField] private Image _rankImage;
        [SerializeField] private Sprite[] _rankSprites;

        private Challenger _challenger;

        public void Render(Challenger challenger)
        {
            _challenger = challenger;
            _rank.text = challenger.Rank.ToString();
            _name.text = challenger.Name;
            _score.text = challenger.KillCount.ToString();
            _image.sprite = challenger.Avatar;
        }

        public void SetName(string name) => _name.text = name;

        public void SetScore(int score) => _score.text = score.ToString();

        public void SetRank(int rank) => _rank.text = rank.ToString();

        public void SetKillCount(int killCount) => _kill.text = killCount.ToString();

        public void SetDeathCount(int deathCount) => _death.text = deathCount.ToString();

        public void SetLevel(int level) => _level.text = level.ToString();

        public void EnablePlayerBackground() => _playerBackgroundImage.gameObject.SetActive(true);

        public void SetTextColor()
        {
            Text[] texts = new Text[] { _name, _death, _kill, _level, _rank, _score };

            foreach (Text text in texts)
                text.color = _playerTextColor;
        }

        private void SetSprite(int index) => _rankImage.sprite = _rankSprites[index - 1];
        private void EnableRankImage() => _rankImage.enabled = true;

        public void TrySetRankIcon(int rank)
        {
            switch (rank)
            {
                case 1:
                    SetSprite(rank);
                    EnableRankImage();
                    break;
                case 2:
                    SetSprite(rank);
                    EnableRankImage();
                    break;
                case 3:
                    SetSprite(rank);
                    EnableRankImage();
                    break;
            }
        }
    }
}
