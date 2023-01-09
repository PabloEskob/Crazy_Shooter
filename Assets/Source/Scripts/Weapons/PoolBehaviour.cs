using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InfimaGames.LowPolyShooterPack
{
    public class PoolBehaviour<T> where T : MonoBehaviour
    {
        public T Prefab { get; }
        public bool IsAutoExpand { get; set; }
        private Transform _container;

        private List<T> _pool;

        public PoolBehaviour(T prefab, int count)
        {
            Prefab = prefab;
            _container = null;
            
            CreatePool(count);
        }

        public PoolBehaviour(T prefab, int count, Transform container)
        {
            Prefab = prefab;
            _container = container;
            
            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();

            for (int i = 0; i < count; i++) 
                CreateObject();
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = Object.Instantiate(Prefab, _container.position, Quaternion.identity);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);
            return createdObject;
        }

        public bool HasFreeElement(out T element)
        {
            foreach (var obj in _pool)
                if (!obj.gameObject.activeInHierarchy)
                {
                    element = obj;
                    obj.gameObject.SetActive(true);
                    return true;
                }

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out var element))
                return element;

            if (IsAutoExpand)
                return CreateObject(true);

            throw new Exception($"There is no free element in pool of type {typeof(T)}");
        }
    }
}