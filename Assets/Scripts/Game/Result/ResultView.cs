using Game.Timer;
using UnityEngine;

namespace Game.Result
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField] private TimerView timerView;

        public void ActivateResult(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
