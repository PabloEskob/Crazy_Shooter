using UnityEngine;

[RequireComponent(typeof( Rigidbody))]
[RequireComponent(typeof( BoxCollider))]
public class TurnOffRoad : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Road road))
        {
            road.TurnOff();
        }
    }
}
