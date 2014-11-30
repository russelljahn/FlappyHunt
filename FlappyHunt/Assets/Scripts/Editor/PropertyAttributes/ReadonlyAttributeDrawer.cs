#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Assets.Scripts.PropertyAttributes;

namespace Assets.Scripts.Editor.PropertyAttributes
{
    [CustomPropertyDrawer(typeof(ReadonlyAttribute))]
    public class ReadonlyAttributePropertyDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) 
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}
#endif
