using GameEnd;
using Player;
using UnityEngine;
using Zenject;

namespace GameState
{
    public class GameStateManager : StateMachine<GameState>
    {
        [Inject] private PlayerController _playerController;

        [SerializeField]
        private GameEndPresenter gameEndPresenter = default;
        
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
                var state = new State<GameState>(GameState.Setting);
                state.SetUpAction = OnSetUpSetting;
                state.UpdateAction = OnUpdateSetting;
                AddState(state);
            }
            //Game
            {
                var state = new State<GameState>(GameState.Game);
                state.SetUpAction = OnSetUpGame;
                state.UpdateAction = OnUpdateGame;
                AddState(state);
            }
            //Finish
            {
                var state = new State<GameState>(GameState.Finish);
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
            GoToState(GameState.Game);
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

            if (gameEndPresenter.IsGameEnd)
            {
                GoToState(GameState.Finish);
            }
        }
        #endregion

        #region FinishMethod
        private void OnSetUpFinish()
        {
            gameEndPresenter.OnResult();
        }
        private void OnUpdateFinish()
        {
            //Debug.Log("Finish");
        }
        #endregion
    }
}
