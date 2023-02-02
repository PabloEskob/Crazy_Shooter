using Agava.YandexGames;
using UnityEngine;

namespace Assets.Source.Scripts.Advertisement
{
    public class AdvertisementHandler : MonoBehaviour
    {
        public void PlayVideoAdvertisement()
        {
            VideoAd.Show();
        }

        public void PlayInterstitalAdvertisement()
        {
            InterstitialAd.Show();
        }

        private void OnOpen()
        {

        }

        private void OnClose()
        {

        }

        private void OnError()
        {

        }

        private void OnOffline()
        {

        }
    }
}
