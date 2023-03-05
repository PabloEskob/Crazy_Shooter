using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class CreateLevelAdjustmentTool : MonoBehaviour
{
    private LevelAdjustmentTool _levelAdjustmentTool;

    [MenuItem("GameObject/LevelAdjustmentTool")]
    public static void Create()
    {
        var gameObject = Resources.Load(AssetPath.PathLevelAdjustment);
        var levelTool = (GameObject)Instantiate(gameObject, Vector3.zero, Quaternion.identity);
        levelTool.tag = "LevelTool";
        levelTool.name = "LevelAdjustmentTool";
    }
}