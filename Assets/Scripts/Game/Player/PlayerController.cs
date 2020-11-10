using Game.Interface;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        private PlayerInput _playerInput;

        private PlayerMover _playerMover;

        private PlayerAttacker　_playerAttacker;

        private PlayerAnimator _playerAnimator;

        #region パラメータ
        [SerializeField]private float moveSpeed = 300f;

        //public float slashCount { get; set; } = 0;    //UIの表示で取得したい
    
        [SerializeField]private float jumpPower = 3000f;

        [SerializeField]private int jumpCount = 1;
        #endregion

        private bool isMoveable = true;

        private bool isAttackable = true;
        
        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerMover = new PlayerMover(GetComponent<Rigidbody>(), transform);
            _playerAttacker = GetComponent<PlayerAttacker>();
            _playerAnimator = new PlayerAnimator(transform);
        }
        public void Initialize()
        {
            //入力
            this.UpdateAsObservable()
                .Subscribe(_ => _playerInput.InputKeys());

            //Move
            this.FixedUpdateAsObservable()
                .Where(_ => isMoveable)
                .Subscribe(_ =>
                {
                    _playerMover.Move(_playerInput.MoveDirection() * moveSpeed);
                });
            
        
            //居合攻撃 壁に触れたら攻撃可能に
            this.UpdateAsObservable()
                .Where(_ => isAttackable)
                .Where(_ => _playerInput.IsAttack)
                .Subscribe(_ =>
                {
                    ChangeLayer(true);
                    _playerAttacker.ActiveAttackCollider(true);

                    _playerAttacker.IaiSlash();
                    _playerAnimator.DoMoveAnimation();

                    isAttackable = false;
                });

            this.OnCollisionEnterAsObservable()
                .Select(collision => collision.gameObject.tag)
                .Where(collision => collision == "Ground" || collision == "Wall")
                .Subscribe(_ =>
                {
                    isAttackable = true;
                });

            //ジャンプ
            this.UpdateAsObservable()
                .Where(_ => IsJumpable() && isMoveable)
                .Subscribe(_ =>
                {
                    _playerMover.Jump(_playerInput.JumpDirection() * jumpPower);

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
                    _playerAttacker.ActiveAttackCollider(false);
                });
        }

        private bool IsJumpable()
        {
            return jumpCount > 0 && _playerInput.JumpDirection().magnitude > 0;
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
