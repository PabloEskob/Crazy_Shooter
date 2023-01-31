using Agava.YandexGames;
using Assets.Source.Scripts.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Menus.Rewards
{
    public class RewardHandler : MonoBehaviour
    {
        [SerializeField] private AdvertisementButton _softRewardButton;
        [SerializeField] private AdvertisementButton _grenadeRewardButton;
        [SerializeField] private int _softRewardAmount;
        [SerializeField] private PauseGameHandler _pauseGameHandler;
        [SerializeField] private CurrencyHolder _currencyHolder;
        [SerializeField] private Roulette _roulette;
        [SerializeField] private GrenadesData _grenadesData;

        private void OnEnable()
        {
            _softRewardButton.ButtonClicked += OnSoftButtonClick;
            _grenadeRewardButton.ButtonClicked += OnGrenadeButtonClick;
            _roulette.Stopped += OnRouletteStopped;
        }

        private void OnDisable()
        {
            _softRewardButton.ButtonClicked -= OnSoftButtonClick;
            _grenadeRewardButton.ButtonClicked -= OnGrenadeButtonClick;
            _roulette.Stopped -= OnRouletteStopped;
        }

        private void OnSoftButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            VideoAd.Show(OnAdvertisementStart, null, OnSoftRewardAdClose);
#endif

#if UNITY_EDITOR
            OnSoftRewardAdClose();
#endif
        }

        private void OnGrenadeButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            VideoAd.Show(OnAdvertisementStart, null, OnGrenadeRewardAdClose);
#endif

#if UNITY_EDITOR
            OnGrenadeRewardAdClose();
#endif
        }

        private void OnAdvertisementStart()
        {
            _pauseGameHandler.OnInBackgroundChange(true);
        }

        private void OnAdvertisementError()
        {
            _pauseGameHandler.OnInBackgroundChange(false);
        }

        private void OnGrenadeRewardAdClose()
        {
            _pauseGameHandler.OnInBackgroundChange(false);
            _roulette.gameObject.SetActive(true);
        }

        private void OnSoftRewardAdClose()
        {
            _pauseGameHandler.OnInBackgroundChange(false);
            _currencyHolder.AddSoft(_softRewardAmount);
        }

        private void OnRouletteStopped(int quantity)
        {
            _grenadesData.TryAddGrenade(quantity);
            _roulette.gameObject.SetActive(false);
        }
    }
}
