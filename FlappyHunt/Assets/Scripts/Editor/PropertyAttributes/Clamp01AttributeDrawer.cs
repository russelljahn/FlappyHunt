using Assets.Scripts.PropertyAttributes;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


namespace Assets.Scripts.Editor.PropertyAttributes
{
    [CustomPropertyDrawer(typeof(Clamp01Attribute))]
    public class Clamp01AttributeDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            var newValue = EditorGUI.Slider(position, property.name, property.floatValue, 0f, 1f);
            property.floatValue = Mathf.Clamp01(newValue);
        }
    }
}
#endif
