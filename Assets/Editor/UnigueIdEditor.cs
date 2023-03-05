/*using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(UniqueId))]
public class UnigueIdEditor : Editor
{
    private void OnEnable()
    {
        var unigueId = (UniqueId)target;

        if (string.IsNullOrEmpty(unigueId.Id))
            Generate(unigueId);
        else
        {
            UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();

            if (uniqueIds.Any(other => other != unigueId && other.Id == unigueId.Id))
                Generate(unigueId);
        }
    }

    private void Generate(UniqueId unigueId)
    {
        unigueId.Id = $"{unigueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

        if (Application.isPlaying)
        {
            EditorUtility.SetDirty(unigueId);
        }
    }
}*/