using DG.Tweening;
using UnityEngine;

namespace Game.Player
{
    public class PlayerAnimator
    {
        private readonly Transform _transform;

        public PlayerAnimator(Transform transform)
        {
            _transform = transform;
        }

        public void DoMoveAnimation()
        {
            var localScale = _transform.localScale;
            var horizontalLongScale = new Vector3(localScale.x, localScale.y * 0.2f);

            _transform.DOScale(horizontalLongScale, 0.2f)
                .OnComplete(() =>
                {
                    _transform.DOScale(1f, 0.1f);
                });
        }
    }
}
