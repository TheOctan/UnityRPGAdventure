using OctanGames.Logic;
using OctanGames.Logic.SpawnMarker;
using UnityEditor;
using UnityEngine;

namespace OctanGames.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditor : UnityEditor.Editor
    {
        private const float RADIUS = 0.5f;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawner.transform.position, RADIUS);
        }
    }
}