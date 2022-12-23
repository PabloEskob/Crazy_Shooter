using UnityEngine;

[RequireComponent(typeof(EnemyAttack))]
public class AttackRange : MonoBehaviour
{
    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private TriggerObserver _triggerObserver;

    private void Start()
    {
        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;

        _enemyAttack.DisableAttack();
    }

    private void TriggerEnter(Collider collider) => 
        _enemyAttack.EnableAttack();

    private void TriggerExit(Collider collider) => 
        _enemyAttack.DisableAttack();
}