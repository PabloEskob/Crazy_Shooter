using UnityEngine;

public class EnemyPointers : MonoBehaviour
{
    [SerializeField] private Point _point;
    
    public Point InstantiatePoint()
    {
        var enemyPoint = Instantiate(_point,transform.parent);
        return enemyPoint;
    }
}