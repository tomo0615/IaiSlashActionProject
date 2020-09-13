using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Start
{
    public class StartView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI startText = default;

        [HideInInspector]public bool isFinished = false;
        
        private const int MovedEndPosition = 630;
        
        public void ViewStartSign()
        {
            startText.enabled = true;

            DOTween.Sequence()
                .Append(startText.rectTransform.DOLocalMove(Vector3.zero, 0.5f))
                .Append(startText.rectTransform.DOLocalMoveX(MovedEndPosition, 0.5f))
                .OnComplete(() =>
                {
                    isFinished = true;

                    // startText.enabled = false;
                });
        }
    }
}
