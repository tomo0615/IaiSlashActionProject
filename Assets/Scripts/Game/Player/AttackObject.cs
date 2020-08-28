using Game.Interface;
using UnityEngine;

namespace Game.Player
{
    public class AttackObject : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable == null) return;
        
            damageable.ApplyDamage();

            //GameEffectManager.Instance.ShakeCamera(0.25f);

            GameEffectManager.Instance.OnGenelateEffect(
                transform.position,
                EffectType.Slash);
        }
    }
}
