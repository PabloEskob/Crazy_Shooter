using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CreateLevelAdjustmentTool : MonoBehaviour
{
    private LevelAdjustmentTool _levelAdjustmentTool;

    [MenuItem("GameObject/LevelAdjustmentTool")]
    public static void Create()
    {
        var gameObject = Resources.Load(AssetPath.PathLevelAdjustment);
        Instantiate(gameObject, Vector3.zero, Quaternion.identity).name = "LevelAdjustmentTool";
    }
}