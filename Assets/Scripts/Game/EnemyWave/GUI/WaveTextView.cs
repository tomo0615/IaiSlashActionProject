using UnityEngine;
using DG.Tweening;

namespace Game.EnemyWave.GUI
{
    public class WaveTextView : MonoBehaviour
    {
        [SerializeField] private RectTransform moveStartTransform;

        [SerializeField] private RectTransform moveEndTransform;

        private RectTransform myRectTransform;
        
        private void Start()
        {
            myRectTransform = GetComponent<RectTransform>();
            
            DoMoveAnimation();
        }
        
        public void DoMoveAnimation()
        {
            transform.position = moveStartTransform.position;

            DOTween.Sequence()
                .Append(myRectTransform.DOMoveX(moveEndTransform.position.x / 2, 0.25f))
                .AppendInterval(0.25f)
                .Append(myRectTransform.DOMoveX(moveEndTransform.position.x, 0.25f));
        }
    }
}
