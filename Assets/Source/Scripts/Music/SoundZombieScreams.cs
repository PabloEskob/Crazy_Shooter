using System.Collections;
using UnityEngine;

namespace Source.Scripts.Music
{
    public class SoundZombieScreams: ZombieSounds
    {
        [SerializeField] private float _minDelay;
        [SerializeField] private float _maxDelay;
        
        public IEnumerator PlaySound()
        {
            var seconds = new WaitForSeconds(Random.Range(_minDelay,_maxDelay));
        
            while (true)
            {
                yield return seconds;
                ChooseSound();
            }
        }
    }
}