using Game.Player;
using Game.Player.PlayerGUI;
using Game.Start;
using UnityEngine;
using Zenject;

namespace Game.GameStates
{
    public class GameStateManager : StateMachine<GameState>
    {
        [Inject] private PlayerController playerController = default;

        [SerializeField] private BlackBeltView blackBeltView = default;

        [SerializeField] private StartView startView = default;

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
            blackBeltView.ViewBlackBelt();
            
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
            
            blackBeltView.FadeBlackBelt();
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
