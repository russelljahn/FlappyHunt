using System;
using System.Collections;
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Utils
{
    public class TweenUtils : MonoBehaviour
    {
        #region Singleton nonsense
        private static TweenUtils _instance;

        private static TweenUtils Instance
        {
            get
            {
                if (_instance.IsNull())
                {
                    _instance = FindObjectOfType<TweenUtils>();
                }
                if (_instance.IsNull())
                {
                    var go = new GameObject("TweenUtils");
                    _instance = go.AddComponent<TweenUtils>();
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }


        protected TweenUtils() {}
        #endregion


        #region Interpolation handlers (Lerp functions)
        private delegate T InterpolationHandler<T>(T from, T to, float t);


        private static float LerpFloat(float from, float to, float t)
        {
            return Mathf.Lerp(from, to, t);
        }


        private static Vector2 LerpVector2(Vector2 from, Vector2 to, float t)
        {
            return Vector2.Lerp(from, to, t);
        }


        private static Vector3 LerpVector3(Vector3 from, Vector3 to, float t)
        {
            return Vector3.Lerp(from, to, t);
        }


        private static Vector4 LerpVector4(Vector4 from, Vector4 to, float t)
        {
            return Vector4.Lerp(from, to, t);
        }


        private static Color LerpColor(Color from, Color to, float t)
        {
            return Color.Lerp(from, to, t);
        }


        private static Color32 LerpColor32(Color32 from, Color32 to, float t)
        {
            return Color32.Lerp(from, to, t);
        }


        private static Quaternion LerpQuaternion(Quaternion from, Quaternion to, float t)
        {
            return Quaternion.Lerp(from, to, t);
        }
        #endregion


        #region Tweening core
        private static void Tween<TValue>(
            Pointer<TValue> valuePtr,
            TValue endValue,
            float time,
            InterpolationHandler<TValue> interpolationFunction,
            AnimationCurve easing = null,
            params Action [] onComplete
            )
        {
            var endValuePtr = new Pointer<TValue>(() => endValue, dummySetter => {});
            Instance.StartCoroutine(Co_TweenInternal(valuePtr, endValuePtr, time, interpolationFunction, easing, onComplete));
        }


        private static void Tween<TValue, TCallbackArg>(
            Pointer<TValue> valuePtr,
            TValue endValue,
            float time,
            InterpolationHandler<TValue> interpolationFunction,
            AnimationCurve easing = null,
            Action<TCallbackArg> onComplete = null,
            TCallbackArg onCompleteArgument = default(TCallbackArg)
            )
        {
            var endValuePtr = new Pointer<TValue>(() => endValue, dummySetter => {});
            Instance.StartCoroutine(Co_TweenInternal(valuePtr, endValuePtr, time, interpolationFunction, easing, onComplete, onCompleteArgument));
        }


        private static void Tween<TValue>(
            Pointer<TValue> valuePtr,
            Pointer<TValue> endValue,
            float time,
            InterpolationHandler<TValue> interpolationFunction,
            AnimationCurve easing = null,
            params Action [] onComplete
            )
        {
            Instance.StartCoroutine(Co_TweenInternal(valuePtr, endValue, time, interpolationFunction, easing, onComplete));
        }


        private static void Tween<TValue, TCallbackArg>(
            Pointer<TValue> valuePtr,
            Pointer<TValue> endValue,
            float time,
            InterpolationHandler<TValue> interpolationFunction,
            AnimationCurve easing = null,
            Action<TCallbackArg> onComplete = null,
            TCallbackArg onCompleteArgument = default(TCallbackArg)
            )
        {
            Instance.StartCoroutine(Co_TweenInternal(valuePtr, endValue, time, interpolationFunction, easing, onComplete, onCompleteArgument));
        }


        private static IEnumerator Co_TweenInternal<TValue>(
            Pointer<TValue> valuePtr,
            Pointer<TValue> endValue,
            float time,
            InterpolationHandler<TValue> interpolationFunction,
            AnimationCurve easing = null,
            params Action [] onComplete
            )
        {
            var t = 0f;
            var initialValue = valuePtr.Dereference;
            easing = easing ?? AnimationCurveUtils.GetLinearCurve();

            while (t < time)
            {
                var lerp = easing.Evaluate(t/time);
                valuePtr.Dereference = interpolationFunction(initialValue, endValue.Dereference, lerp);

                yield return null;
                t += Time.deltaTime;
            }

            valuePtr.Dereference = endValue.Dereference;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < onComplete.Length; ++i) {
                var callback = onComplete[i];
                if (callback.IsNotNull()) {
                    callback.Invoke();
                }
            }
        }


        private static IEnumerator Co_TweenInternal<TValue, TCallbackArg>(
            Pointer<TValue> valuePtr,
            Pointer<TValue> endValue,
            float time,
            InterpolationHandler<TValue> interpolationFunction,
            AnimationCurve easing = null,
            Action<TCallbackArg> onComplete = null,
            TCallbackArg onCompleteArgument = default(TCallbackArg)
            )
        {
            var t = 0f;
            var initialValue = valuePtr.Dereference;
            easing = easing ?? AnimationCurveUtils.GetLinearCurve();

            while (t < time)
            {
                var lerp = easing.Evaluate(t/time);
                valuePtr.Dereference = interpolationFunction(initialValue, endValue.Dereference, lerp);

                yield return null;
                t += Time.deltaTime;
            }

            valuePtr.Dereference = endValue.Dereference;
            if (onComplete != null)
            {
                onComplete.Invoke(onCompleteArgument);
            }
        }
        #endregion


        #region Tweening types
        public static void TweenFloat(
            Pointer<float> valuePtr,
            float endValue,
            float time,
            AnimationCurve easing = null,
            params Action [] onComplete
            )
        {
            Tween(valuePtr, endValue, time, LerpFloat, easing, onComplete);
        }


        public static void TweenFloat<T>(
            Pointer<float> valuePtr,
            float endValue,
            float time,
            AnimationCurve easing = null,
            Action<T> onComplete = null,
            T onCompleteArgument = default(T)
            )
        {
            Tween(valuePtr, endValue, time, LerpFloat, easing, onComplete, onCompleteArgument);
        }


        public static void TweenVector2(
            Pointer<Vector2> valuePtr,
            Vector2 endValue,
            float time,
            AnimationCurve easing = null,
            params Action [] onComplete
            )
        {
            Tween(valuePtr, endValue, time, LerpVector2, easing, onComplete);
        }


        public static void TweenVector2<T>(
            Pointer<Vector2> valuePtr,
            Vector2 endValue,
            float time,
            AnimationCurve easing = null,
            Action<T> onComplete = null,
            T onCompleteArgument = default(T)
            )
        {
            Tween(valuePtr, endValue, time, LerpVector2, easing, onComplete, onCompleteArgument);
        }


        public static void TweenVector3(
            Pointer<Vector3> valuePtr,
            Vector3 endValue,
            float time,
            AnimationCurve easing = null,
            params Action [] onComplete
            )
        {
            Tween(valuePtr, endValue, time, LerpVector3, easing, onComplete);
        }


        public static void TweenVector3<T>(
            Pointer<Vector3> valuePtr,
            Vector3 endValue,
            float time,
            AnimationCurve easing = null,
            Action<T> onComplete = null,
            T onCompleteArgument = default(T)
            )
        {
            Tween(valuePtr, endValue, time, LerpVector3, easing, onComplete, onCompleteArgument);
        }


        public static void TweenVector4(
            Pointer<Vector4> valuePtr,
            Vector4 endValue,
            float time,
            AnimationCurve easing = null,
            params Action [] onComplete
            )
        {
            Tween(valuePtr, endValue, time, LerpVector4, easing, onComplete);
        }


        public static void TweenVector4<T>(
            Pointer<Vector4> valuePtr,
            Vector4 endValue,
            float time,
            AnimationCurve easing = null,
            Action<T> onComplete = null,
            T onCompleteArgument = default(T)
            )
        {
            Tween(valuePtr, endValue, time, LerpVector4, easing, onComplete, onCompleteArgument);
        }


        public static void TweenColor(
            Pointer<Color> valuePtr,
            Color endValue,
            float time,
            AnimationCurve easing = null,
            params Action [] onComplete
            )
        {
            Tween(valuePtr, endValue, time, LerpColor, easing, onComplete);
        }


        public static void TweenColor<T>(
            Pointer<Color> valuePtr,
            Color endValue,
            float time,
            AnimationCurve easing = null,
            Action<T> onComplete = null,
            T onCompleteArgument = default(T)
            )
        {
            Tween(valuePtr, endValue, time, LerpColor, easing, onComplete, onCompleteArgument);
        }


        public static void TweenColor32(
            Pointer<Color32> valuePtr,
            Color32 endValue,
            float time,
            AnimationCurve easing = null,
            params Action [] onComplete
            )
        {
            Tween(valuePtr, endValue, time, LerpColor32, easing, onComplete);
        }


        public static void TweenColor32<T>(
            Pointer<Color32> valuePtr,
            Color32 endValue,
            float time,
            AnimationCurve easing = null,
            Action<T> onComplete = null,
            T onCompleteArgument = default(T)
            )
        {
            Tween(valuePtr, endValue, time, LerpColor32, easing, onComplete, onCompleteArgument);
        }


        public static void TweenQuaternion(
            Pointer<Quaternion> valuePtr,
            Quaternion endValue,
            float time,
            AnimationCurve easing = null,
            params Action [] onComplete
            )
        {
            Tween(valuePtr, endValue, time, LerpQuaternion, easing, onComplete);
        }


        public static void TweenQuaternion<T>(
            Pointer<Quaternion> valuePtr,
            Quaternion endValue,
            float time,
            AnimationCurve easing = null,
            Action<T> onComplete = null,
            T onCompleteArgument = default(T)
            )
        {
            Tween(valuePtr, endValue, time, LerpQuaternion, easing, onComplete, onCompleteArgument);
        }
        #endregion


        #region Specific tweens
        public static void TweenWorldPosition(Transform transform, Vector3 endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var ptr = new Pointer<Vector3>(() => transform.position, value => transform.position = value);
            TweenVector3(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenWorldPosition(Transform transform, Transform endValue, float time, AnimationCurve easing = null, params Action [] onComplete) 
        {
            var ptr = new Pointer<Vector3>(() => transform.position, value => transform.position = value);
            TweenVector3(ptr, endValue.position, time, easing, onComplete);
        }


        public static void TweenLocalPosition(Transform transform, Vector3 endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var ptr = new Pointer<Vector3>(() => transform.localPosition, value => transform.localPosition = value);
            TweenVector3(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenLocalPosition(Transform transform, Transform endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            TweenWorldPosition(transform, endValue, time, easing, onComplete);
        }


        public static void TweenWorldRotation(Transform transform, Vector3 endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var ptr = new Pointer<Vector3>(() => transform.rotation.eulerAngles, value => transform.rotation = value.ToQuaternion());
            TweenVector3(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenLocalRotation(Transform transform, Vector3 endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var ptr = new Pointer<Vector3>(() => transform.localRotation.eulerAngles, value => transform.localRotation = value.ToQuaternion());
            TweenVector3(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenWorldRotation(Transform transform, Quaternion endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var ptr = new Pointer<Quaternion>(() => transform.rotation, value => transform.rotation = value);
            TweenQuaternion(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenLocalRotation(Transform transform, Quaternion endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var ptr = new Pointer<Quaternion>(() => transform.localRotation, value => transform.localRotation = value);
            TweenQuaternion(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenLocalScale(Transform transform, Vector3 endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var ptr = new Pointer<Vector3>(() => transform.localScale, value => transform.localScale = value);
            TweenVector3(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenSize(RectTransform rectTransform, Vector2 endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var ptr = new Pointer<Vector2>(() => rectTransform.sizeDelta, value => rectTransform.sizeDelta = value);
            TweenVector2(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenSize(Text text, int endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var ptr = new Pointer<float>(() => text.fontSize, value => text.fontSize = Mathf.FloorToInt(value));
            TweenFloat(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenColor(Color color, Color endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var ptr = new Pointer<Color>(() => color, value => color = value);
            TweenColor(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenColor(Graphic graphic, Color endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var ptr = new Pointer<Color>(() => graphic.color, value => graphic.color = value);
            TweenColor(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenAlpha(Color color, float endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var ptr = new Pointer<float>(() => color.a, value => color.a = value);
            TweenFloat(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenAlpha(CanvasGroup group, float endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var ptr = new Pointer<float>(() => group.alpha, value => group.alpha = value);
            TweenFloat(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenAlpha<T>(CanvasGroup group, float endValue, float time, AnimationCurve easing = null,  Action<T> onComplete = null, T onCompleteArg = default(T)) 
        {
            var ptr = new Pointer<float>(() => group.alpha, value => group.alpha = value);
            TweenFloat(ptr, endValue, time, easing, onComplete);
        }


        public static void TweenAlpha(Graphic graphic, float endValue, float time, AnimationCurve easing = null, params Action [] onComplete)
        {
            var color = graphic.color;
            var ptr = new Pointer<float>(() => color.a, value => { color.a = value; graphic.color = color; });
            TweenFloat(ptr, endValue, time, easing, onComplete);
        }
        #endregion



    }
}
