using System.Collections;
using UnityEngine;

public class BloodEffectSpawner : ObjectPool
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _enableDelay;

    private IEnumerator _coroutine;
    
    private void Start() =>
        Init(_prefab);

    public void Init(Vector3 position)
    {
        if (TryGetObject(out GameObject effect))
            SetEffect(effect, position);
    }

    private void SetEffect(GameObject effect, Vector3 position)
    {
        effect.transform.position = position;
        effect.SetActive(true);
        StartCoroutine(EnableFX(effect));
    }

    private IEnumerator EnableFX(GameObject effect)
    {
        var delay = new WaitForSeconds(_enableDelay);
        yield return delay;
        effect.SetActive(false);
    }
}