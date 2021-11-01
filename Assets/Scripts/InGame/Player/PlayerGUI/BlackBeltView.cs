using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.Player.PlayerGUI
{
    public class BlackBeltView : MonoBehaviour
    {
        [SerializeField] private Image blackBeltUp = default;

        [SerializeField] private Image blackBeltDawn = default;

        private readonly Vector3 fadeScale = new Vector3(1, 0, 1);

        public void ViewBlackBelt()
        {
            blackBeltUp.transform.DOScale(Vector3.one, 0.5f);
            blackBeltDawn.transform.DOScale(Vector3.one, 0.5f);
        }

        public void FadeBlackBelt()
        {
            blackBeltUp.transform.DOScale(fadeScale, 0.5f);
            blackBeltDawn.transform.DOScale(fadeScale, 0.5f);
        }
    }
}
