using System;
using OctanGames.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

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
        }

        private void Generate(UniqueId uniqueId)
        {
            uniqueId.Id = Guid.NewGuid().ToString();

            if (Application.isPlaying) return;

            EditorUtility.SetDirty(uniqueId);
            EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            
        }
    }
}