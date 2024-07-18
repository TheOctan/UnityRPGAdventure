using System.Linq;
using OctanGames.Logic;
using OctanGames.Logic.SpawnMarker;
using OctanGames.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OctanGames.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string INITIAL_POINT_TAG = "InitialPoint";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners =
                    FindObjectsOfType<SpawnMarker>()
                        .Select(e =>
                            new EnemySpawnerData(e.GetComponent<UniqueId>().Id, e.MonsterTypeId, e.transform.position))
                        .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
                levelData.InitialHeroPosition = GameObject.FindWithTag(INITIAL_POINT_TAG).transform.position;
            }

            EditorUtility.SetDirty(target);
        }
    }
}