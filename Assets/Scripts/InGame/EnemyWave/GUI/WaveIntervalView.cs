using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.EnemyWave.GUI
{
    public class WaveIntervalView : MonoBehaviour
    {
        [SerializeField] private Image intervalFrame = default;
        
        public void FillIntervalFrame(float intervalValue)
        {
            intervalFrame.DOFillAmount(1f,intervalValue);
        }

        public void ResetFillAmount()
        {
            intervalFrame.fillAmount = 0f;
        }
    }
}
