using Game.EnemyWave;
using Game.Player;
using Game.Player.PlayerGUI;
using Game.Result;
using Game.Start;
using Game.Timer;
using UnityEngine;
using Zenject;

namespace Game.GameStates
{
    public class GameStateManager : StateMachine<GameState>
    {
        [Inject] private PlayerController playerController = default;

        [SerializeField] private BlackBeltView blackBeltView = default;

        [SerializeField] private StartView startView = default;

        [SerializeField] private ResultView resultView;

        [SerializeField] private TimerPresenter timerPresenter;

        [SerializeField] private Waves waves;
        
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
            
            startView.ViewStartSign();
        }

        private void OnUpdateSetting()
        {
            if(startView.isFinished == false) return;
                        
            GoToState(GameState.Game);
        }
        #endregion

        #region GameMethod

        private void OnSetUpGame()
        {
            playerController.Initialize();
            
            blackBeltView.FadeBlackBelt();
            
            timerPresenter.OnStartTimer();
            
            waves.OnStartWave();
        }

        private void OnUpdateGame()
        {
            if (waves.IsEndWave())
            {
                 GoToState(GameState.Finish);
            }
        }
        #endregion

        #region FinishMethod
        private void OnSetUpFinish()
        {
            timerPresenter.OnFinishTimer();
            
            resultView.ActivateResult(true);
        }
        private void OnUpdateFinish()
        {
            
        }
        #endregion
    }
}
