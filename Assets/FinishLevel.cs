using System;
using System.Collections;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _delay = 2f;

    private static readonly int Open = Animator.StringToHash("Open");

    public event Action EndedLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>()) 
            StartCoroutine(EndLevel());
    }

    private IEnumerator EndLevel()
    {
        _animator.SetTrigger(Open);
        var delay = new WaitForSeconds(_delay);
        yield return delay;
        EndedLevel?.Invoke();
        Stop();
    }

    private void Stop() =>
        StopCoroutine(EndLevel());
}