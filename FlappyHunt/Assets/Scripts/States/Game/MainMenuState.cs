using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets.Scripts.States.Game {
    public class MainMenuState : State {
        private IEnumerator OnEnter(Enum previousState) {
            Debug.Log("Starting Game!");
            //TransitionState(GameLoopStates.StartRound);
            yield break;
        }


        private void OnStart() {
            Debug.Log("MainMenuState::OnStart()!");
        }


        private void OnUpdate()
        {
            Debug.Log("MainMenuState::OnUpdate()!");
        }
    }
}
