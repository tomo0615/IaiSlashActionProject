﻿using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.GameStates
{
    public class GameStateManager : StateMachine<GameState>
    {
        [Inject] private PlayerController playerController;
        
        private void Awake()
        {
            InitializeStateMachine();
        }

        private void Start()
        {
            GoToState(GameState.Setting);
        }

        private void InitializeStateMachine()
        {
            //Setting
            {
                var state = new State<global::Game.GameStates.GameState>(GameState.Setting);
                state.SetUpAction = OnSetUpSetting;
                state.UpdateAction = OnUpdateSetting;
                AddState(state);
            }
            //Game
            {
                var state = new State<global::Game.GameStates.GameState>(GameState.Game);
                state.SetUpAction = OnSetUpGame;
                state.UpdateAction = OnUpdateGame;
                AddState(state);
            }
            //Finish
            {
                var state = new State<global::Game.GameStates.GameState>(GameState.Finish);
                state.SetUpAction = OnSetUpFinish;
                state.UpdateAction = OnUpdateFinish;
                AddState(state);
            }
        }
        
        #region SettingMethod
        private void OnSetUpSetting()
        {
            GoToState(GameState.Game);
        }

        private void OnUpdateSetting()
        {
            
        }
        #endregion

        #region GameMethod

        private void OnSetUpGame()
        {
            playerController.Initialize();
        }

        private void OnUpdateGame()
        {
            //Playerを使えるようにする
            //playerController.UpdatePlayerAction();
        }
        #endregion

        #region FinishMethod
        private void OnSetUpFinish()
        {
            
        }
        private void OnUpdateFinish()
        {
            //Debug.Log("Finish");
        }
        #endregion
    }
}
