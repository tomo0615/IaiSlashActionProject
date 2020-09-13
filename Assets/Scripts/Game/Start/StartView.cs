using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Start
{
    public class StartView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI startText = default;

        public bool isFinished = false;
        
        public void ViewStartSign()
        {
            startText.enabled = true;
            
            startText.transform.DOScale(Vector3.one * 5, 1.0f)
                .OnComplete(() =>
                {
                    isFinished = true;

                    startText.enabled = false;
                });
        }
    }
}
