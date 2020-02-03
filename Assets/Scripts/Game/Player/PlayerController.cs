﻿using UnityEngine;
using UniRx;

public class PlayerController : MonoBehaviour, IDamageable
{
    private PlayerInputs _playerInputs;

    private PlayerMover _playerMover;

    private PlayerAttacks _playerAttacks;

    private PlayerAnimator _playerAnimator;

    #region パラメータ
    [SerializeField]private float moveSpeed = 300f;

    [SerializeField]private float slashSpeed = 70f;

    //public float slashCount { get; set; } = 0;    //UIの表示で取得したい
    
    [SerializeField]private float jumpPower = 3000f;

    [SerializeField]private int jumpCount = 1;
    #endregion

    private protected bool isMoveable = true;

    [SerializeField]
    private GameObject attackObject = null;//攻撃時にPlayerノ前に出るオブジェクト

    private void Awake()
    {
        _playerInputs = new PlayerInputs();
        _playerMover = new PlayerMover(GetComponent<Rigidbody>(), transform);
        _playerAttacks = GetComponent<PlayerAttacks>();
        _playerAnimator = new PlayerAnimator(transform);
    }

    void Update()
    {
        _playerInputs.InputKeys();

        if (_playerInputs.IsAttack)
        {
            //if (slashCount < 0) return; 

            attackObject.SetActive(true);
            ChangeLayer();

            //slashCount--;

            _playerAttacks.IaiSlash(slashSpeed);

            _playerAnimator.DOMoveAnimation();
        }
        /*
        else if(_playerInputs.IsCharge)
        {
            slashCount += _playerAttacks.ChargeSlashCount();

            isMoveable = false;
        }*/
        else if (IsJumpable() && isMoveable)
        {
            _playerMover.Jump(_playerInputs.JumpDirection() * jumpPower);
            jumpCount--;
        }
        else
        {
            isMoveable = true;
        }
    }

     private bool IsJumpable()
    {
        return jumpCount > 0 && _playerInputs.JumpDirection().magnitude > 0;
    }

    private void FixedUpdate()
    {
        if (!isMoveable) return;

        _playerMover.Move(_playerInputs.MoveDirection() * moveSpeed);

    }
    private void ChangeLayer()
    {
        var layerName = attackObject.activeSelf ? "NoneHitEnemy" : "Player";

        gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    public void ApplyDamage()
    {
        //ダメージを食らう処理
    }

    #region 当たり判定
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 1; //TODO:JumpCountを戻す処理にする マジックナンバーを消す
        }
        else
        {
            _playerMover.Stop();//壁抜け防止
        }

        attackObject.SetActive(false);
        ChangeLayer();
    }
    #endregion
}
