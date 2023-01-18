using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerDeath))]
[RequireComponent(typeof(PlayerRotate))]
public class Player : MonoBehaviour
{
    private PlayerMove _playerMove;
    private PlayerHealth _playerHealth;
    private PlayerAnimator _playerAnimator;
    private PlayerDeath _playerDeath;
    private PlayerRotate _playerRotate;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerDeath = GetComponent<PlayerDeath>();
        _playerRotate = GetComponent<PlayerRotate>();
    }

    public PlayerMove PlayerMove =>
        _playerMove;

    public PlayerHealth PlayerHealth =>
        _playerHealth;

    public PlayerDeath PlayerDeath =>
        _playerDeath;

    public PlayerAnimator PlayerAnimator =>
        _playerAnimator;

    public PlayerRotate PlayerRotate =>
        _playerRotate;
}