using UnityEngine;
using Zenject;

namespace Game.GameStates
{
    public class GameStateManager : StateMachine<GameState>
    {
        private void Awake()
        {
            InitializeStateMachine();
        }

        private void Start()
        {
            GoToState(global::Game.GameStates.GameState.Setting);
        }

        private void InitializeStateMachine()
        {
            //Setting
            {
                var state = new State<global::Game.GameStates.GameState>(global::Game.GameStates.GameState.Setting);
                state.SetUpAction = OnSetUpSetting;
                state.UpdateAction = OnUpdateSetting;
                AddState(state);
            }
            //Game
            {
                var state = new State<global::Game.GameStates.GameState>(global::Game.GameStates.GameState.Game);
                state.SetUpAction = OnSetUpGame;
                state.UpdateAction = OnUpdateGame;
                AddState(state);
            }
            //Finish
            {
                var state = new State<global::Game.GameStates.GameState>(global::Game.GameStates.GameState.Finish);
                state.SetUpAction = OnSetUpFinish;
                state.UpdateAction = OnUpdateFinish;
                AddState(state);
            }
        }
        
        #region SettingMethod
        private void OnSetUpSetting()
        {

        }

        private void OnUpdateSetting()
        {
            GoToState(global::Game.GameStates.GameState.Game);
        }
        #endregion

        #region GameMethod

        private void OnSetUpGame()
        {
 
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
