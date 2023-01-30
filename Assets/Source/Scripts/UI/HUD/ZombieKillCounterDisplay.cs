using System;
using TMPro;
using UnityEngine;

public class ZombieKillCounterDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private ZombieKillCounter _counter;

    private void Awake() => UpdateText();

    private void OnEnable() => 
        _counter.QuantityChanged += OnQuantityChanged;

    private void OnDisable() => 
        _counter.QuantityChanged -= OnQuantityChanged;

    private void OnQuantityChanged() => UpdateText();

    private void UpdateText() => 
        _counterText.text = $"{_counter.ZombieKilledQuantity}/{_counter.MaxZombieQuantity}";
}
