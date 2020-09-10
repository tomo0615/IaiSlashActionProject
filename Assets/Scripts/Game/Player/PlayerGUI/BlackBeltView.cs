using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.Player.PlayerGUI
{
    public class BlackBeltView : MonoBehaviour
    {
        [SerializeField] private Image blackBeltUp;

        [SerializeField] private Image blackBeltDawn;

        private readonly Vector3 fadeScale = new Vector3(1, 0, 1);

        public void ViewBlackBelt()
        {
            blackBeltUp.transform.DOScale(Vector3.one, 1.0f);
            blackBeltDawn.transform.DOScale(Vector3.one, 1.0f);
        }

        public void FadeBlackBelt()
        {
            blackBeltUp.transform.DOScale(fadeScale, 1.0f);
            blackBeltDawn.transform.DOScale(fadeScale, 1.0f);
        }
    }
}
