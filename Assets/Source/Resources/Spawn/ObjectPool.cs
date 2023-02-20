using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _capacity;

    private readonly List<GameObject> _pool = new();

    protected void Init(GameObject prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);
        return result != null;
    }
}