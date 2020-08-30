using UnityEngine;

namespace Game.Player
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField]
        private float slashSpeed = 100f;
        [SerializeField]
        private AttackObject attackObject = null; //攻撃時の当たり判定用

        private Rigidbody _rigidbody;

        private Camera _camera;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            _camera = Camera.main;
            
            attackObject.Initialize();
        }

        public void IaiSlash() //TODO:複数のことをやっているので分ける
        {
            var playerPosition = _camera.WorldToScreenPoint(transform.localPosition);
            
            var lookDirection = (Vector2)(Input.mousePosition - playerPosition).normalized;

            var rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
            transform.localRotation = rotation * Quaternion.Euler(0f, 0f, 90f);
            
            _rigidbody.velocity = lookDirection * slashSpeed;
        }

        public void ActiveAttackCollider(bool attackFlag)
        {
            attackObject.gameObject.SetActive(attackFlag);
        }

        /*
    public float ChargeSlashCount()
    { 
        return Time.deltaTime; //TODO：指数的に減らす　1~5の間で徐々にチャージスピードを減らす
    }
    */
    }
}
