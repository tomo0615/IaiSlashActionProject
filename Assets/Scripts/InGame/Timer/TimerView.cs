using TMPro;
using UnityEngine;

namespace Game.Timer
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText = default;

        public void ViewTimeValue(int timeValue)
        {
            timerText.text = ConvertMinuteTime(timeValue);
        }
        
        private string ConvertMinuteTime(int timeValue)
        {
            var minute = timeValue / 60;
            var remainSeconds = timeValue - minute * 60;
            
            return minute.ToString("d2")+ ":" + remainSeconds.ToString("d2");
        }
    }
}
