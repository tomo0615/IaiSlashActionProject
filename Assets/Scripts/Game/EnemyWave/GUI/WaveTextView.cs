using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Game.EnemyWave.GUI
{
    public class WaveTextView : MonoBehaviour
    {
        [SerializeField] private RectTransform moveStartTransform;

        [SerializeField] private RectTransform moveEndTransform;

        private RectTransform myRectTransform;

        private TextMeshProUGUI waveText;
        
        private const float AnimationTime = 0.25f;
        
        private void Awake()
        {
            myRectTransform = GetComponent<RectTransform>();

            waveText = GetComponent<TextMeshProUGUI>();
        }
        
        public void DoMoveAnimation(int waveIndex)
        {
            transform.position = moveStartTransform.position;

            waveText.text = "wave " + waveIndex;
            
            DOTween.Sequence()
                .Append(myRectTransform.DOLocalMoveX(0, AnimationTime))
                .AppendInterval(AnimationTime)
                .Append(myRectTransform.DOMoveX(moveEndTransform.position.x, AnimationTime));
        }
    }
}
