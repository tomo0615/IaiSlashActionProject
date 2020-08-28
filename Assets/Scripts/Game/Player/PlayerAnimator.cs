using DG.Tweening;
using UnityEngine;

namespace Game.Player
{
    public class PlayerAnimator
    {
        private Transform _transform;

        public PlayerAnimator(Transform transform)
        {
            _transform = transform;
        }

        public void DOMoveAnimation()
        {
            Vector3 horizontalLongScale = new Vector3(_transform.localScale.x, _transform.localScale.y * 0.2f);

            _transform.DOScale(horizontalLongScale, 0.2f)
                .OnComplete(() =>
                {
                    _transform.DOScale(1f, 0.1f);
                });
        }
    }
}
