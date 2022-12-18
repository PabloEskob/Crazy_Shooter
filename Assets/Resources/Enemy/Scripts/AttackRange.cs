using UnityEngine;

[RequireComponent(typeof(Attack))]
public class AttackRange : MonoBehaviour
{
    [SerializeField] private Attack _attack;
    [SerializeField] private TriggerObserver _triggerObserver;

    private void Start()
    {
        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;

        _attack.DisableAttack();
    }

    private void TriggerEnter(Collider collider)
    {
        _attack.EnableAttack();
    }

    private void TriggerExit(Collider collider)
    {
        _attack.DisableAttack();
    }
}