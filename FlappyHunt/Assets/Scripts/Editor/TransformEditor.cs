using UnityEditor;
using UnityEngine;


namespace Assets.Scripts.Editor {

    [CustomEditor(typeof (Transform))]
    public class TransformInspector : UnityEditor.Editor {
        private static bool _expand;


        public override void OnInspectorGUI() {
            var t = (Transform) target;

            _expand = EditorGUILayout.ToggleLeft(" Include World Transform", _expand);
            EditorGUILayout.Separator();

            EditorGUIUtility.LookLikeControls();
            EditorGUI.indentLevel = 0;


            t.localPosition = EditorGUILayout.Vector3Field("Local Position", t.localPosition);
            if (_expand) {
                t.position = EditorGUILayout.Vector3Field("World Position", t.position);
                EditorGUILayout.Separator();
            }

            t.localEulerAngles = EditorGUILayout.Vector3Field("Local Rotation", t.localEulerAngles);
            if (_expand) {
                t.eulerAngles = EditorGUILayout.Vector3Field("World Rotation", t.eulerAngles);
                EditorGUILayout.Separator();
            }

            t.localScale = EditorGUILayout.Vector3Field("Local Scale", t.localScale);

            if (!GUI.changed) {
                return;
            }

            Undo.RecordObject(t, "Transform Change");
            t.localPosition = FixIfNaN(t.localPosition);
            t.position = FixIfNaN(t.position);

            t.localEulerAngles = FixIfNaN(t.localEulerAngles);
            t.eulerAngles = FixIfNaN(t.eulerAngles);

            t.localScale = FixIfNaN(t.localScale);
        }


        private Vector3 FixIfNaN(Vector3 v) {
            if (float.IsNaN(v.x)) {
                v.x = 0;
            }
            if (float.IsNaN(v.y)) {
                v.y = 0;
            }
            if (float.IsNaN(v.z)) {
                v.z = 0;
            }
            return v;
        }

    }
}
