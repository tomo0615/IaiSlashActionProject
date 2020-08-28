using UnityEngine;

namespace Game.Player
{
    public class PlayerAttacks : MonoBehaviour
    {
        [SerializeField]
        private float slashSpeed = 100f;
        [SerializeField]
        private GameObject attackObject = null;//攻撃時にPlayerの前に出るオブジェクト

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void IaiSlash() //TODO:複数のことをやっているので分ける
        {
            var pos = Camera.main.WorldToScreenPoint(transform.localPosition); //コンストラクタを作るとエラーを吐く

            var lookDirection = (Vector2)(Input.mousePosition - pos);
            lookDirection = lookDirection.normalized;

            var rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
            transform.localRotation = rotation * Quaternion.Euler(0f, 0f, 90f);

            _rigidbody.velocity = Vector2.zero;
            _rigidbody.velocity = lookDirection * slashSpeed;
        }

        public void ActiveAttackCollider(bool attackFlag)
        {
            attackObject.SetActive(attackFlag);
        }

        /*
    public float ChargeSlashCount()
    { 
        return Time.deltaTime; //TODO：指数的に減らす　1~5の間で徐々にチャージスピードを減らす
    }
    */
    }
}
