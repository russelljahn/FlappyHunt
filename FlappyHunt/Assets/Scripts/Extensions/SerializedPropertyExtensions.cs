#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;


namespace Assets.Scripts.Extensions {
    /// @usage: Use as you would a native SerializedPropertyValueExtension method;
    /// e.g. `Debug.Log(mySerializedProperty.Value<Color>());`
    /// @usage: Lives best within `Assets/Plugins/`.
    public static class SerializedPropertyExtensions {

        public static TValue Value<TValue>(this SerializedProperty property, string valueName) {
            var nestedProperty = property.serializedObject.FindProperty(valueName);
            if (nestedProperty.IsNull()) {
                Debug.LogError(string.Format("No property found named '{0}'!", valueName));
                return default(TValue);
            }
            return nestedProperty.Value<TValue>();
        }


        /// @note: switch/case derived from the decompilation of SerializedProperty's internal SetToValueOfTarget() method.
        public static TValue Value<TValue>(this SerializedProperty property) {
            var typeOfValue = typeof(TValue);

            // First, do special Type checks
            if (typeOfValue.IsEnum) {
                return (TValue)Enum.ToObject(typeOfValue, property.enumValueIndex);
            }

            // Next, check for literal UnityEngine struct-types
            // @note: ->object->ValueT double-casts because C# is too dumb to realize that that the ValueT in each situation is the exact type needed.
            // e.g. `return property.colorValue` spits _error CS0029: Cannot implicitly convert type `UnityEngine.Color' to `ValueT'_
            // and `return (ValueT)property.colorValue;` spits _error CS0030: Cannot convert type `UnityEngine.Color' to `ValueT'_
            if (typeof(Color).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.colorValue;
            }
            else if (typeof(LayerMask).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.intValue;
            }
            else if (typeof(Vector2).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.vector2Value;
            }
            else if (typeof(Vector3).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.vector3Value;
            }
            else if (typeof(Rect).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.rectValue;
            }
            else if (typeof(AnimationCurve).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.animationCurveValue;
            }
            else if (typeof(Bounds).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.boundsValue;
            }
            else if (typeof(Gradient).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)SafeGradientValue(property);
            }
            else if (typeof(Quaternion).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.quaternionValue;
            }

            // Next, check if derived from UnityEngine.Object base class
            else if (typeof(UnityEngine.Object).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.objectReferenceValue;
            }

            // Next, check for native type-families
            else if (typeof(int).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.intValue;
            }
            else if (typeof(bool).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.boolValue;
            }
            else if (typeof(float).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.floatValue;
            }
            else if (typeof(string).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.stringValue;
            }
            else if (typeof(char).IsAssignableFrom(typeOfValue)) {
                return (TValue)(object)property.intValue;
            }

            // And if all fails, throw an exception.
            else {
                throw new NotImplementedException("Unimplemented propertyType " + property.propertyType + ".");
            }
        }


        /// Access to SerializedProperty's internal gradientValue property getter, in a manner that'll only soft break (returning null) if the property changes or disappears in future Unity revs.
        private static Gradient SafeGradientValue(SerializedProperty property) {
            const BindingFlags instanceAnyPrivacyBindingFlags = BindingFlags.Public | BindingFlags.NonPublic |
                                                          BindingFlags.Instance;
            var propertyInfo = typeof(SerializedProperty).GetProperty(
                "gradientValue",
                instanceAnyPrivacyBindingFlags,
                null,
                typeof(Gradient),
                new Type [0],
                null
            );
            if (propertyInfo == null) {
                return null;
            }
            var gradientValue = propertyInfo.GetValue(property, null) as Gradient;
            return gradientValue;
        }
    }
}
#endif
