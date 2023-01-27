using Source.Infrastructure;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.Character
{
    public class GrenadesData : MonoBehaviour
    {
        private int _grenadeCount = 1;
        IStorage _storage;

        private const string GrenadeDataKey = "grenadeCount";

        public int GrenadeMaxCount { get; } = 10;

        public event Action<int> GrenadeCountSetted;

        private void Start()
        {
            _storage = AllServices.Container.Single<IStorage>();
            Load();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                TryAddGrenade(1);
            }
        }

        private void Save()
        {
            _storage.SetInt(GrenadeDataKey, _grenadeCount);
            _storage.Save();
        }

        private void Load()
        {
            if (_storage.HasKeyInt(GrenadeDataKey))
                SetGrenadesCount(_storage.GetInt(GrenadeDataKey));
            else
                SetGrenadesCount(_grenadeCount);
        }

        public void TryAddGrenade(int count)
        {
            if (count < GrenadeMaxCount)
                _grenadeCount += count;

            if (_grenadeCount > GrenadeMaxCount)
                _grenadeCount = GrenadeMaxCount;

            Save();
        }

        public void RemoveGrenade()
        {
            _grenadeCount--;
            Save();
        }

        public void SetGrenadesCount(int count)
        {
            _grenadeCount = count;
            GrenadeCountSetted?.Invoke(_grenadeCount);
        }
    }

}
