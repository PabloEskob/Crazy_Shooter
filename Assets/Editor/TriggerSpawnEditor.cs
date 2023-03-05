using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TriggerSpawn))]
public class TriggerSpawnEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(TriggerSpawn trigger, GizmoType gizmoType)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(trigger.transform.position, 0.5f);
    }
}