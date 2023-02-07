using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Scripts.UI
{
    public class SurviveTimer : MonoBehaviour
    {
        [SerializeField] private float _timeToSurvive;
        private Timer _timer;

        public Timer Timer => _timer;

        private void Awake() => 
            _timer = new Timer();

        private void Start() => 
            _timer.Start(_timeToSurvive);

        private void OnEnable()
        {
            _timer.Completed += OnTimerCompleted;
            _timer.Stopped += OnTimerStop;
        }

        private void OnDisable()
        {
            _timer.Completed -= OnTimerCompleted;
            _timer.Stopped -= OnTimerStop;
        }

        private void Update() => 
            _timer.Tick(Time.deltaTime);

        private void OnTimerCompleted() => 
            _timer.Stop();

        private void OnTimerStop()
        {
            GameStatusScreen statusScreen = FindObjectOfType<GameStatusScreen>();

            if(statusScreen != null)
            {
                statusScreen.PlayVictory();
            }
            else
            {
                throw new NullReferenceException("GameStatusScreen object not found on current scene");
            }
        }

    }
}
