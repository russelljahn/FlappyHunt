using System;
using Assets.Scripts.Extensions;
using UnityEngine;


namespace Assets.Scripts.Utils {

    [ExecuteInEditMode]
    public class ScreenUtils : MonoBehaviour {

         #region Singleton nonsense
        private static ScreenUtils _instance;

        private static ScreenUtils Instance {
            get {
                if (_instance.IsNull()) {
                    _instance = FindObjectOfType<ScreenUtils>();
                }
                if (_instance.IsNull()) {
                    var go = new GameObject("ScreenUtils");
                    _instance = go.AddComponent<ScreenUtils>();
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }

        protected ScreenUtils() {}
        #endregion


        public delegate void ResolutionChangedEventHandler (object sender, ResolutionChangedEventArgs args);
        public static ResolutionChangedEventHandler ResolutionChanged = delegate {};

        public class ResolutionChangedEventArgs : EventArgs {
            public int PreviousWidth;
            public int PreviousHeight;
            public int CurrentWidth;
            public int CurrentHeight; 
        }


        private Rect _screenExtents;
        private int _previousWidth;
        private int _previousHeight;


        private void OnEnable() {
            UpdateTrackedValues();
        }


        private void Update() {
            if (_previousWidth != Screen.width || _previousHeight != Screen.height) {
                OnResolutionChanged();
            }
        }


        private void UpdateTrackedValues() {
            _previousWidth = Screen.width;
            _previousHeight = Screen.height;
            _screenExtents = new Rect(0f, 0f, Screen.width, Screen.height);
        }


        private void OnResolutionChanged() {

            var args = new ResolutionChangedEventArgs() {
                PreviousWidth = _previousWidth,
                PreviousHeight = _previousHeight,
                CurrentWidth = Screen.width,
                CurrentHeight = Screen.height
            };

            UpdateTrackedValues();
            ResolutionChanged.Invoke(this, args);
        }


        public static bool IsOnScreen(Vector2 screenPoint) {
            return Instance._screenExtents.Contains(screenPoint);
        }


        public static bool IsOnScreen(Vector3 screenPoint) {
            return Instance._screenExtents.Contains(screenPoint);
        }


        public static bool IsMouseOnScreen() {
            var mousePosition = Application.isPlaying ? Input.mousePosition : (Vector3)Event.current.mousePosition;
            return IsOnScreen(mousePosition);
        }
    }
}
