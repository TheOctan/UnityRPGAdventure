using System;
using System.Linq;
using OctanGames.Logic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace OctanGames.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = target as UniqueId;

            if (uniqueId == null) return;

            if (string.IsNullOrEmpty(uniqueId.Id))
            {
                Generate(uniqueId);
            }
            else
            {
                UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();
                if (uniqueIds.Any( e => e != uniqueId && e.Id == uniqueId.Id))
                {
                    Generate(uniqueId);
                }
            }
        }

        private static void Generate(UniqueId uniqueId)
        {
            uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

            if (Application.isPlaying) return;

            EditorUtility.SetDirty(uniqueId);
            EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
        }
    }
}