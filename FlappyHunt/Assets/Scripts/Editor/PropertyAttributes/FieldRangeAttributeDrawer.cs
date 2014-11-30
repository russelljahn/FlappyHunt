#if UNITY_EDITOR


//TODO: This is broken and may possibly be due to the Unity version 4.6b21 w/ only minimum testing.

//using Assets.SultansLover.Scripts.Extensions;
//using Assets.SultansLover.Scripts.PropertyAttributes;
//using UnityEditor;
//using UnityEngine;


//namespace Assets.Scripts.Editor.PropertyAttributes
//{
//    [CustomPropertyDrawer(typeof(FieldRangeAttribute))]
//    public class ReflectedRangeAttributeAttributeDrawer : PropertyDrawer 
//    {
//        private FieldRangeAttribute _attributeValue;
//        private FieldRangeAttribute AttributeValue {
//            get { return _attributeValue.IsNotNull() ? _attributeValue : (_attributeValue = (FieldRangeAttribute)attribute); }
//        }

//        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//        {
//            if (property.propertyType == SerializedPropertyType.Float) {
//                EditorGUI.Slider(position, property, property.Value<float>(AttributeValue.MinPropertyNeme), property.Value<float>(AttributeValue.MaxPropertyNeme), label);
//            }
//            else if (property.propertyType == SerializedPropertyType.Integer) {
//                EditorGUI.Slider(position, property, property.Value<int>(AttributeValue.MinPropertyNeme), property.Value<int>(AttributeValue.MaxPropertyNeme), label);
//            }
//            else {
//                EditorGUI.HelpBox(position, "FieldRangeAttribute can only be used on float and int types.", MessageType.Error);
//            }
//        }
//    }
//}
#endif
