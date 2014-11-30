using System;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts.States {
    public class StateInfo {
        public State State;

        #region cached callbacks
        public Action OnCreateCallback;
        public Action OnDestroyCallback;

        public Func<Enum, IEnumerator> OnEnterCallback;
        public Func<IEnumerator> OnExitCallback;

        public Action OnUpdateCallback;
        public Action OnFixedUpdateCallback;
        public Action OnLateUpdateCallback;

        public Action<Collision2D> OnCollisionEnterCallback;
        public Action<Collision2D> OnCollisionStayCallback;
        public Action<Collision2D> OnCollisionExitCallback;

        public Action<Collider2D> OnTriggerEnterCallback;
        public Action<Collider2D> OnTriggerStayCallback;
        public Action<Collider2D> OnTriggerExitCallback;

        public Action OnRenderGUICallback;
        #endregion
    }
}
