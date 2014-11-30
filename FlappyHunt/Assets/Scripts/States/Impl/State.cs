using System;
using UnityEngine;


namespace Assets.Scripts.States {
    public class State : ScriptableObject {

        protected State() {}

        public StateMachine StateMachine { get; set; }

        public Enum StateName { get; set; }

        public float TimeStartedState { get; set; }
        public float TimeElapsedInState { get { return Time.time - TimeStartedState; } }

        public void ChangeState(Enum nextStateName) {
            StateMachine.ChangeState(this, nextStateName);
        }


        public override string ToString() {
            return string.Format("[State: {0}]", StateName);
        }
    }
}
