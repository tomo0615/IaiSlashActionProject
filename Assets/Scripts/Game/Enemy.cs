using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private int hitCount = 1;
    private BoxCollider _boxCollider;
    public ParticleSystem destroyEffect;

    private void Death()
    {
        if (hitCount > 0) return;

        Destroy(gameObject);
        //
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            hitCount--;
            GameEffectController.Instance.ShakeCamera(0.5f);

            Death();
        }
    }
}
