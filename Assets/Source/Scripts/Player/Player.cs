using InfimaGames.LowPolyShooterPack;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerDeath))]
[RequireComponent(typeof(PlayerRotate))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerBody _playerBody;
    
    private PlayerMove _playerMove;
    private PlayerHealth _playerHealth;
    private PlayerAnimator _playerAnimator;
    private PlayerDeath _playerDeath;
    private PlayerRotate _playerRotate;
    private Character _character;
    
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
    public Character Character =>
        _character;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerDeath = GetComponent<PlayerDeath>();
        _playerRotate = GetComponent<PlayerRotate>();
        _character = GetComponent<Character>();
    }

    public void SetActive() => 
        _playerBody.Disable();
}