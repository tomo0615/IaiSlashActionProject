using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region パラメータ
    [SerializeField]private int hitPoint = 1;

    [SerializeField] private float moveSpeed = 10;
    #endregion

    private Rigidbody _rigidbody;
    /*
    private BoxCollider _boxCollider;
    [SerializeField]private ParticleSystem destroyEffect;
    */
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Move()
    {
        //moveAnimation実装
    }

    private void Attack()
    {
        //当たり判定のある何かを出す　or アニメーションを動かす
    }

    private void Death()
    {
        if (hitPoint > 0) return;

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            hitPoint--;

            //GameEffectManager.Instance.ShakeCamera(0.5f);
            GameEffectManager.Instance.OnHitAttack(other.transform.position);
            Death();
        }
    }
}
