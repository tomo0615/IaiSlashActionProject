using System;
using UnityEngine;

namespace Game.Timer
{
    public class TimerPresenter : MonoBehaviour
    {
        private TimerModel timerModel;

        private TimerView timerView;

        private void Start()
        {
            timerModel = new TimerModel();
            
            
        }
    }
}
