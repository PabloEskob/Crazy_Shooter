using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelAdjustmentTool))]
[CanEditMultipleObjects]
public class LevelAdjustmentToolEditor : Editor
{
    private LevelAdjustmentTool _levelAdjustmentTool;
    private bool _buttonRemoveAllZone;
    private bool _buttonZone;
    private bool _buttonRemoveZone;
    private bool _buttonCreateSpawner;
    private bool _buttonDeleteSpawner;
    private int _numberZone;

    private void OnEnable() =>
        _levelAdjustmentTool = (LevelAdjustmentTool)target;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        ChangeTypeLevelCategory();

        switch (_levelAdjustmentTool._typeLevelCategory)
        {
            case LevelAdjustmentTool.LevelCategory.Company:
                ViewButtonsZone();
                ViewZone();
                break;
            case LevelAdjustmentTool.LevelCategory.Survival:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ChangeTypeLevelCategory()
    {
        _levelAdjustmentTool._typeLevelCategory = (LevelAdjustmentTool.LevelCategory)
            EditorGUILayout.EnumPopup("Type Level", _levelAdjustmentTool._typeLevelCategory);
    }

    private void CreateButtonZone()
    {
        GUILayout.Space(10);
        EditorGUILayout.BeginVertical("box");
        _buttonZone = GUILayout.Button("Create Zone", GUILayout.Height(50));
        _buttonRemoveZone = GUILayout.Button("Remove Zone", GUILayout.Height(50));
        _buttonRemoveAllZone = GUILayout.Button("RemoveAll", GUILayout.Height(50));
        EditorGUILayout.EndVertical();
    }

    private void ViewButtonsZone()
    {
        if (_buttonZone)
        {
            _levelAdjustmentTool.AddZone();
        }

        if (_buttonRemoveZone)
        {
            _levelAdjustmentTool.DeleteZone();
        }

        if (_buttonRemoveAllZone)
        {
            _levelAdjustmentTool.RemoveAll();
        }
    }

    private void ViewZone()
    {
        foreach (var zone in _levelAdjustmentTool._zones)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.TextField("Name", $"{zone.Name} {zone.Number}");
            EditorGUILayout.IntField("Count Spawners", zone.CountEnemySpawners);
            EditorGUILayout.Space(10);
            
            if (zone.CountEnemySpawners > 0)
            {
                EditorGUILayout.Space(10);
                
                for (int i = 0; i < zone._enemySpawners.Count; i++)
                {
                    EditorGUILayout.ObjectField($"Spawner-{zone._enemySpawners[i].Number}", zone._enemySpawners[i],
                        typeof(GameObject),
                        false);
                    EditorGUILayout.ObjectField($"TurningPoint-{zone._enemySpawners[i].Number}", zone._turningPoints[i],
                        typeof(GameObject),
                        false);
                    EditorGUILayout.Space(10);
                }
            }
            
            _numberZone = zone.Number;
            CreateButtonsSpawners();
            CreateSpawner();
            DeleteSpawner();
            EditorGUILayout.EndVertical();
        }

        CreateButtonZone();
    }

    private void CreateButtonsSpawners()
    {
        EditorGUILayout.BeginHorizontal("box");
        _buttonCreateSpawner = GUILayout.Button("Create Spawner", GUILayout.Width(120), GUILayout.Height(30));
        _buttonDeleteSpawner = GUILayout.Button("Delete Spawner", GUILayout.Width(120), GUILayout.Height(30));
        EditorGUILayout.EndHorizontal();
    }

    private void CreateSpawner()
    {
        if (_buttonCreateSpawner)
        {
            _levelAdjustmentTool.CreateEnemySpawner(_numberZone);
        }
    }

    private void DeleteSpawner()
    {
        if (_buttonDeleteSpawner)
        {
            _levelAdjustmentTool.DeleteEnemySpawner(_numberZone);
        }
    }
}