using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TurningPoint))]
public class TurningPointEditor: Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]

    public static void RenderCustomGizmoTurningPoint(TurningPoint turningPoint,GizmoType gizmoType)
    {
       Gizmos.color=Color.blue;
       Gizmos.DrawSphere(turningPoint.transform.position,0.5f);
    }
}