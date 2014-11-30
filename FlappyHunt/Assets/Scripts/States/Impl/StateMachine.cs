using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using Assets.Scripts.PropertyAttributes;
using Assets.Scripts.Utils;
using UnityEngine;


namespace Assets.Scripts.States {
    public class StateMachine : MonoBehaviour {

        [SerializeField] private Enum _startingState;
        public Enum StartingState { get { return _startingState; } set { _startingState = value; } }


        [SerializeField] private bool _running;
        public bool Running { get { return _running; } private set { _running = value; } }


        [SerializeField] private bool _changingState;
        [SerializeField] public bool ChangingState { get { return _changingState; } private set { _changingState = value; } }

        private readonly Dictionary<Enum, StateInfo> _stateInfos = new Dictionary<Enum, StateInfo>();


        [Readonly, SerializeField] private State _previousState;
        public State PreviousState { get { return _previousState; } private set { _previousState = value; } }

        [Readonly, SerializeField] private State _currentState;
        public State CurrentState { get { return _currentState; } private set { _currentState = value; } }


        #region event handlers
        private Func<Enum, IEnumerator> _entered;
        private Func<IEnumerator> _exited;

        private Action _updated;
        private Action _fixedUpdated;
        private Action _lateUpdated;

        private Action<Collision2D> _collisionEntered;
        private Action<Collision2D> _collisionStayed;
        private Action<Collision2D> _collisionExited;

        private Action<Collider2D> _triggerEntered;
        private Action<Collider2D> _triggerStayed;
        private Action<Collider2D> _triggerExited;

        private Action _renderedGUI;
        #endregion


        #region event handler name binding constants
        private const string OnCreateCallbackName = "OnCreate";
        private const string OnDestroyCallbackName = "OnDestroy";

        private const string OnEnterCallbackName = "OnEnter";
        private const string OnExitCallbackName = "OnExit";
        
        private const string OnUpdateCallbackName = "OnUpdate";
        private const string OnFixedUpdateCallbackName = "OnFixedUpdate";
        private const string OnLateUpdateCallbackName = "OnLateUpdate";

        private const string OnCollisionEnterCallbackName = "OnCollisionEnter";
        private const string OnCollisionStayCallbackName = "OnCollisionStay";
        private const string OnCollisionExitCallbackName = "OnCollisionExit";

        private const string OnTriggerEnterCallbackName = "OnTriggerEnter";
        private const string OnTriggerStayCallbackName = "OnTriggerStay";
        private const string OnTriggerExitCallbackName = "OnTriggerExit";

        private const string OnRenderGUICallbackName = "OnRenderGUI";
        #endregion


        #region relayed callbacks
        private void OnCreateState(StateInfo stateInfo) {
            if (stateInfo.OnCreateCallback != null) {
                stateInfo.OnCreateCallback.Invoke();
            }
        }


        private void OnDestroyState(StateInfo stateInfo) {
            if (stateInfo.OnDestroyCallback != null) {
                stateInfo.OnDestroyCallback.Invoke();
            }
            UnwireEvents(stateInfo);
        }


        //MonoBehvaiour Callback
        private void OnDestroy() {
            foreach (var pair in _stateInfos) {
                var stateInfo = pair.Value;
                OnDestroyState(stateInfo);
            }
        }


        private IEnumerator OnEnter(Enum previousStateName) {
            //Debug.Log("StateMachine::OnEnter()!");
            //Debug.Log("_entered: " + _entered);
            if (_entered != null) {
                //Debug.Log("_entered isn't null! Yay!");
                yield return StartCoroutine(_entered(previousStateName));
            }
        }


        private IEnumerator OnExit() {
            //Debug.Log("StateMachine::OnExit()!");
            //Debug.Log("_exited: " + _exited);
            if (_exited != null) {
                //Debug.Log("_exited isn't null! Yay!");
                yield return StartCoroutine(_exited());
            }
        }


        private void Update() {
            if (_updated != null) {
                _updated();
            }
        }


        private void FixedUpdate() {
            if (_fixedUpdated != null) {
                _fixedUpdated();
            }
        }


        private void LateUpdate() {
            if (_lateUpdated != null) {
                _lateUpdated();
            }
        }


        //TODO: Allow ability to get these callbacks from another GameObject
        private void OnCollisionEnter(Collision2D collision) {
            if (_collisionEntered != null) {
                _collisionEntered(collision);
            }
        }


        //TODO: Allow ability to get these callbacks from another GameObject
        private void OnCollisionStay(Collision2D collision) {
            if (_collisionStayed != null) {
                _collisionStayed(collision);
            }
        }


        //TODO: Allow ability to get these callbacks from another GameObject
        private void OnCollisionExit(Collision2D collision) {
            if (_collisionExited != null) {
                _collisionExited(collision);
            }
        }


        //TODO: Allow ability to get these callbacks from another GameObject
        private void OnTriggerEnter(Collider2D triggeredCollider) {
            if (_triggerEntered != null) {
                _triggerEntered(triggeredCollider);
            }
        }


        //TODO: Allow ability to get these callbacks from another GameObject
        private void OnTriggerStay(Collider2D triggeredCollider) {
            if (_triggerStayed != null) {
                _triggerStayed(triggeredCollider);
            }
        }


        //TODO: Allow ability to get these callbacks from another GameObject
        private void OnTriggerExit(Collider2D triggeredCollider) {
            if (_triggerExited != null) {
                _triggerExited(triggeredCollider);
            }
        }


        private void OnGUI() {
            if (_renderedGUI != null) {
                _renderedGUI();
            }
        }
        #endregion


        private static void CacheStateCallbacks(StateInfo stateInfo) {
            stateInfo.OnCreateCallback = ReflectionUtils.GetMethod<Action>(stateInfo.State, OnCreateCallbackName);
            stateInfo.OnDestroyCallback = ReflectionUtils.GetMethod<Action>(stateInfo.State, OnDestroyCallbackName);

            stateInfo.OnEnterCallback = ReflectionUtils.GetMethod<Func<Enum, IEnumerator>>(stateInfo.State, OnEnterCallbackName);
            stateInfo.OnExitCallback = ReflectionUtils.GetMethod<Func<IEnumerator>>(stateInfo.State, OnExitCallbackName);

            stateInfo.OnUpdateCallback = ReflectionUtils.GetMethod<Action>(stateInfo.State, OnUpdateCallbackName);
            stateInfo.OnUpdateCallback = ReflectionUtils.GetMethod<Action>(stateInfo.State, OnUpdateCallbackName);

            stateInfo.OnUpdateCallback = ReflectionUtils.GetMethod<Action>(stateInfo.State, OnUpdateCallbackName);
            stateInfo.OnFixedUpdateCallback = ReflectionUtils.GetMethod<Action>(stateInfo.State, OnFixedUpdateCallbackName);
            stateInfo.OnLateUpdateCallback = ReflectionUtils.GetMethod<Action>(stateInfo.State, OnLateUpdateCallbackName);

            stateInfo.OnCollisionEnterCallback = ReflectionUtils.GetMethod<Action<Collision2D>>(stateInfo.State, OnCollisionEnterCallbackName);
            stateInfo.OnCollisionStayCallback = ReflectionUtils.GetMethod<Action<Collision2D>>(stateInfo.State, OnCollisionStayCallbackName);
            stateInfo.OnCollisionExitCallback = ReflectionUtils.GetMethod<Action<Collision2D>>(stateInfo.State, OnCollisionExitCallbackName);

            stateInfo.OnTriggerEnterCallback = ReflectionUtils.GetMethod<Action<Collider2D>>(stateInfo.State, OnTriggerEnterCallbackName);
            stateInfo.OnTriggerStayCallback = ReflectionUtils.GetMethod<Action<Collider2D>>(stateInfo.State, OnTriggerStayCallbackName);
            stateInfo.OnTriggerExitCallback = ReflectionUtils.GetMethod<Action<Collider2D>>(stateInfo.State, OnTriggerExitCallbackName);

            stateInfo.OnRenderGUICallback = ReflectionUtils.GetMethod<Action>(stateInfo.State, OnRenderGUICallbackName);
        }


        private void WireEvents(StateInfo stateInfo) {
            _entered += stateInfo.OnEnterCallback;
            _exited += stateInfo.OnExitCallback;

            _updated += stateInfo.OnUpdateCallback;
            _fixedUpdated += stateInfo.OnFixedUpdateCallback;
            _lateUpdated += stateInfo.OnLateUpdateCallback;

            _collisionEntered += stateInfo.OnCollisionEnterCallback;
            _collisionStayed += stateInfo.OnCollisionStayCallback;
            _collisionExited += stateInfo.OnCollisionExitCallback;

            _triggerEntered += stateInfo.OnTriggerEnterCallback;
            _triggerStayed += stateInfo.OnTriggerStayCallback;
            _triggerExited += stateInfo.OnTriggerExitCallback;

            _renderedGUI += stateInfo.OnRenderGUICallback;
            //Debug.Log("Wiring events for: " + stateInfo.State.GetType());
        }


        private void UnwireEvents(StateInfo stateInfo) {
            _entered -= stateInfo.OnEnterCallback;
            _exited -= stateInfo.OnExitCallback;

            _updated -= stateInfo.OnUpdateCallback;
            _fixedUpdated -= stateInfo.OnFixedUpdateCallback;
            _lateUpdated -= stateInfo.OnLateUpdateCallback;

            _collisionEntered -= stateInfo.OnCollisionEnterCallback;
            _collisionStayed -= stateInfo.OnCollisionStayCallback;
            _collisionExited -= stateInfo.OnCollisionExitCallback;

            _triggerEntered -= stateInfo.OnTriggerEnterCallback;
            _triggerStayed -= stateInfo.OnTriggerStayCallback;
            _triggerExited -= stateInfo.OnTriggerExitCallback;

            _renderedGUI -= stateInfo.OnRenderGUICallback;
            //Debug.Log("Unwiring events for: " + stateInfo.State.GetType());
        }


        public void StartMachine() {
            if (Running) {
                Debug.LogWarning(gameObject.name + " is already runnning!");
            }
            if (StartingState.IsNull()) {
                if (_stateInfos.Count == 0) {
                    Debug.LogError(string.Format("Starting machine '{0}' with no states!", name));
                    return;
                }
                else {
                    PreviousState = CurrentState = _stateInfos.First().Value.State;
                    Debug.LogWarning(string.Format("Starting machine with no CurrentState; Starting machine with state: '{0}'!", PreviousState.StateName));
                }
            }
            else {
                PreviousState = CurrentState = _stateInfos[StartingState].State;
            }
            ChangeState(PreviousState, PreviousState.StateName);
            Running = true;
        }


        public void StopMachine() {
            if (!Running) {
                Debug.LogWarning(gameObject.name + " is already stopped!");
            }
            Running = false;
        }


        public void AddState<T>(Enum stateName) where T : State, new() {
            if (_stateInfos.ContainsKey(stateName)) {
                Debug.LogError(string.Format("There's already a state with the name: '{0}'!", stateName));
                return;
            }
            var state = ScriptableObject.CreateInstance<T>();
            state.StateName = stateName;
            state.StateMachine = this;
            var stateInfo = new StateInfo { State = state };

            CacheStateCallbacks(stateInfo);
            _stateInfos [stateName] = stateInfo;
            OnCreateState(stateInfo);
            //Debug.Log("StateInfo.state: " + stateInfo.State);
        }


        public void RemoveState(Enum stateName) {
            if (!_stateInfos.ContainsKey(stateName)) {
                Debug.LogError(string.Format("There's no state with the name: '{0}'!", stateName));
                return;
            }
            if (Equals(stateName, CurrentState.StateName)) {
                Debug.LogError("Removing current state; unpredictable behaviour may result!");
                var stateInfo = _stateInfos [stateName];
                OnDestroyState(stateInfo);
            }
            _stateInfos.Remove(stateName);
        }


        public void ChangeState(State previousState, Enum nextStateName) {
            if (!ChangingState) {
                ChangingState = true;
                var previousStateInfo = _stateInfos [previousState.StateName];

                this.InvokeAfterCoroutine(OnExit(), OnCurrentStateFinished, previousState, nextStateName);
                UnwireEvents(previousStateInfo);
            }
            else {
                Debug.LogWarning("Already changing state!");
            }
        }


        private void OnCurrentStateFinished(State previousState, Enum nextStateName) {
            //Debug.Log("OnCurrentStateFinished()!");
            var nextStateInfo = _stateInfos[nextStateName];
            PreviousState = previousState;
            CurrentState = nextStateInfo.State;

            _entered += nextStateInfo.OnEnterCallback;
            this.InvokeAfterCoroutine(OnEnter(PreviousState.StateName), () => {
                WireEvents(nextStateInfo);
                ChangingState = false;
            });
            _entered -= nextStateInfo.OnEnterCallback;
        }


    }

}
