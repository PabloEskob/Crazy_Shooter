using UnityEngine;
using Random = UnityEngine.Random;

public class ClawsSpawner : MonoBehaviour
{
    [SerializeField] private Claws _claws;
    [SerializeField] private ZoneClaws _backGround;
    
    private Canvas _canvas;
    private float _positionX;
    private float _positionY;

    private void Awake() => 
        _canvas = GetComponentInParent<Canvas>();

    public void Attack()
    { 
        Instantiate(_claws, SetPosition(), Quaternion.identity,_canvas.transform);
    }
    
    private Vector2 SetPosition()
    {
        var bounds = _backGround.GetComponent<BoxCollider>().bounds;
        _positionX = Random.Range(bounds.min.x, bounds.max.x);
        _positionY= Random.Range(bounds.min.y, bounds.max.y);
        Vector2 position = new Vector2(_positionX, _positionY);
        return position;
    }
    
}