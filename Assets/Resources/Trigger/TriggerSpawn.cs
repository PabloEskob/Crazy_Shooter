using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerSpawn : MonoBehaviour
{
    private int _enemySpawnerNumber;

    public event Action<Collider, int> TriggerEnter;

    private void OnTriggerEnter(Collider other) =>
        TriggerEnter?.Invoke(other, _enemySpawnerNumber);

    public void Init(int number)
    {
        _enemySpawnerNumber = number;
    }
}