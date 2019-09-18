using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputs _playerInputs;

    private PlayerMover _playerMover;

    private PlayerAttacks _playerAttacks;

    #region パラメータ
    [SerializeField]private float moveSpeed = 300f;

    [SerializeField]private float slashSpeed = 50f;

    public float slashCount { get; set; } = 0;    //UIの表示で取得したい
    
    [SerializeField]private float jumpPower = 3000f;

    [SerializeField]private int jumpCount = 1;
    #endregion

    private float dx;

    private protected bool isMoveable = true;

    [SerializeField]private GameObject attackObject = null;

    private void Awake()
    {
        _playerInputs = new PlayerInputs();
        _playerMover = new PlayerMover(GetComponent<Rigidbody>(), transform);
        _playerAttacks = GetComponent<PlayerAttacks>();
    }

    void Update()
    {
        dx = Input.GetAxisRaw("Horizontal");

        _playerInputs.InputKeys();

        if (_playerInputs.IsAttack)
        {
            if ((int)slashCount <= 0) return;

            attackObject.SetActive(true);
            ChangeLayer();

            isMoveable = true;
            slashCount--;

            _playerAttacks.IaiSlash(slashSpeed);
        }
        else if(_playerInputs.IsCharge)
        {
            slashCount += _playerAttacks.ChargeSlashCount();

            isMoveable = false;
        }

        if (IsJumpable())
        {
            _playerMover.Jump(_playerInputs.JumpDirection() * jumpPower);
            jumpCount--;
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

    #region 当たり判定
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 1; //TODO:JumpCountを戻す処理にする
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
