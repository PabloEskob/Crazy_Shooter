using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerDeath))]
public class Player : MonoBehaviour
{
    private PlayerMove _playerMove;
    private PlayerHealth _playerHealth;
    private PlayerAnimator _playerAnimator;
    private PlayerDeath _playerDeath;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerDeath = GetComponent<PlayerDeath>();
    }

    public PlayerMove PlayerMove =>
        _playerMove;

    public PlayerHealth PlayerHealth =>
        _playerHealth;

    public PlayerDeath PlayerDeath => 
        _playerDeath;

    public PlayerAnimator PlayerAnimator =>
        _playerAnimator;
}