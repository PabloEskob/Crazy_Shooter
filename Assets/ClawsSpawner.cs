using UnityEngine;
using Random = UnityEngine.Random;

public class ClawsSpawner : ObjectPool
{
    [SerializeField] private GameObject _claws;
    [SerializeField] private ZoneClaws _backGround;

    private void Start() =>
        Init(_claws);

    public void Attack()
    {
        if (TryGetObject(out GameObject claws))
        {
            claws.gameObject.SetActive(true);
            var newClaws = claws.GetComponent<Claws>();
            newClaws.Enable();
            newClaws.SetPosition(SetSpawnPosition());
        }
    }

    private Vector3 SetSpawnPosition() =>
        _backGround.SetPosition();
}