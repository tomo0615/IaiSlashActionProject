using UniRx;
using UnityEngine;

namespace Game.Timer
{
    public class TimerPresenter : MonoBehaviour
    {
        private TimerModel timerModel;

        [SerializeField] private TimerView timerView = default;

        private void Start()
        {
            timerModel = new TimerModel();
        }
        
        public void OnStartTimer()
        {
            var observable = timerModel.CreateTimerObservable();

            //Timer購読終了後に値を反映
            observable
                .Subscribe(value => { timerModel.SetTimeValue((int)value); },
                    () =>
                    {
                        timerView.ViewTimeValue(timerModel.TimerValue.Value);
                    });
        }

        //Game終了時呼び出し
        public void OnFinishTimer()
        {
            timerModel.ActivateTimer(false);
        }
    }
}
