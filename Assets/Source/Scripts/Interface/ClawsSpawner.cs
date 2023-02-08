using UnityEngine;

public class ClawsSpawner : ObjectPool
{
    private ZoneClaws _backGround;
    private GameObject _claws;

    private void Awake() => 
        _backGround = GetComponentInParent<ActorUI>().GetComponentInChildren<ZoneClaws>();

    private void Start()
    {
        _claws = (GameObject)Resources.Load(AssetPath.PathClaws);
        Init(_claws);
    }

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