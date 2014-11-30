using UnityEngine;


namespace Assets.Scripts.Utils {
    public static class AnimationCurveUtils {

        public static AnimationCurve GetLinearCurve() {
            return AnimationCurve.Linear(0f, 0f, 1f, 1f);
        }
    }
}
