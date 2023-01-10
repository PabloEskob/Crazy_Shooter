using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMove))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private TargetPointer _targetPointer;

    public TargetPointer TargetPointer => _targetPointer;

    public PlayerMove PlayerMove =>
        _playerMove;

    public PlayerHealth PlayerHealth =>
        _playerHealth;
}