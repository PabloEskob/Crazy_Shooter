using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    private EnemyPointers _enemyPointers;
    private Point _point;
    private Transform _player;
    private Camera _camera;
    private Ray _ray;
    private Vector3 _fromPlayerToEnemy;
    private Plane[] _planes;
    private EnemyDeath _enemyDeath;
    
    private void Start()
    {
        _player = GetComponent<EnemyMove>().Player.GetComponent<Player>().TargetPointer.transform;
        _enemyDeath = GetComponent<EnemyDeath>();
        _camera = Camera.main;
        _enemyPointers = GameObject.FindGameObjectWithTag("GameStatusScreen").GetComponentInChildren<EnemyPointers>();
        _point = _enemyPointers.InstantiatePoint();
        _point.Construct(_enemyDeath);
    }

    private void LateUpdate()
    {
        _fromPlayerToEnemy = transform.position - _player.transform.position;
        _ray = new Ray(_player.transform.position, _fromPlayerToEnemy + new Vector3(0, 1.6f, 0));

        _planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        float minDistance = Mathf.Infinity;
        int planeIndex = 0;

        for (int i = 0; i < 4; i++)
            if (_planes[i].Raycast(_ray, out float distance))
                if (distance < minDistance)
                {
                    minDistance = distance;
                    planeIndex = i;
                }

        minDistance = Mathf.Clamp(minDistance, 0, _fromPlayerToEnemy.magnitude);
        Vector3 worldPosition = _ray.GetPoint(minDistance);
        _point.transform.position = _camera.WorldToScreenPoint(worldPosition);
        _point.transform.rotation = GetIconRotation(planeIndex);
    }

    private Quaternion GetIconRotation(int planeIndex)
    {
        return planeIndex switch
        {
            0 => Quaternion.Euler(0, 0, 90),
            1 => Quaternion.Euler(0, 0, -90),
            2 => Quaternion.Euler(0, 0, 180),
            _ => planeIndex == 3 ? Quaternion.Euler(0, 0, 0) : Quaternion.identity
        };
    }
}