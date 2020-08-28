using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        private PlayerInputs _playerInputs;

        private PlayerMover _playerMover;

        private PlayerAttacks _playerAttacks;

        private PlayerAnimator _playerAnimator;

        #region パラメータ
        [SerializeField]private float moveSpeed = 300f;

        //public float slashCount { get; set; } = 0;    //UIの表示で取得したい
    
        [SerializeField]private float jumpPower = 3000f;

        [SerializeField]private int jumpCount = 1;
        #endregion

        private bool isMoveable = true;

        private void Awake()
        {
            _playerInputs = new PlayerInputs();
            _playerMover = new PlayerMover(GetComponent<Rigidbody>(), transform);
            _playerAttacks = GetComponent<PlayerAttacks>();
            _playerAnimator = new PlayerAnimator(transform);
        }
        private void Start()
        {
            //入力
            this.UpdateAsObservable()
                .Subscribe(_ => _playerInputs.InputKeys());

            //Move
            this.FixedUpdateAsObservable()
                .Where(_ => isMoveable)
                .Subscribe(_ =>
                {
                    _playerMover.Move(_playerInputs.MoveDirection() * moveSpeed);
                });
            
        
            //居合攻撃
            this.UpdateAsObservable()
                .Where(_ => _playerInputs.IsAttack)
                .Subscribe(_ =>
                {
                    ChangeLayer(true);
                    _playerAttacks.ActiveAttackCollider(true);

                    _playerAttacks.IaiSlash();
                    _playerAnimator.DOMoveAnimation();
                });

            //ジャンプ
            this.UpdateAsObservable()
                .Where(_ => IsJumpable() && isMoveable)
                .Subscribe(_ =>
                {
                    _playerMover.Jump(_playerInputs.JumpDirection() * jumpPower);

                    jumpCount--;
                });

            this.OnCollisionEnterAsObservable()
                .Subscribe(collision =>
                {
                    if (collision.gameObject.CompareTag("Ground"))
                    {
                        jumpCount = 1;
                    }
                    else
                    {
                        _playerMover.Stop();//壁抜け防止
                    }

                    ChangeLayer(false);
                    _playerAttacks.ActiveAttackCollider(false);
                });
        }

        private bool IsJumpable()
        {
            return jumpCount > 0 && _playerInputs.JumpDirection().magnitude > 0;
        }
    
        private void ChangeLayer(bool attackFlag)
        {
            var layerName = attackFlag ? "NoneHitEnemy" : "Player";

            gameObject.layer = LayerMask.NameToLayer(layerName);
        }

        public void ApplyDamage()
        {
            //ダメージを食らう処理
        }
    }
}
