using UnityEngine;


namespace Assets.Scripts.Utils {
    public static class MathfUtils {

        public static bool IsZero(float value) {
            return Mathf.Approximately(value, 0f);
        }


        public static bool IsNonzero(float value) {
            return !IsZero(value);
        }


        public static bool IsLessThanOrEqualTo(float lhs, float rhs) {
            return lhs < rhs || Mathf.Approximately(lhs, rhs);
        }


        public static bool IsGreaterThanOrEqualTo(float lhs, float rhs) {
            return lhs > rhs || Mathf.Approximately(lhs, rhs);
        }


        public static bool IsLessThanOrEqualToZero(float value) {
            return value < 0 || IsZero(value);
        }


        public static bool IsGreaterThanOrEqualToZero(float value) {
            return value > 0 || IsZero(value);
        }
    }
}
