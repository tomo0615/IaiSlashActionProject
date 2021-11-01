using Game.Interface;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

namespace Game.Player
{
    public class AttackObject : MonoBehaviour
    {
        public void Initialize()
        {
            this.OnTriggerEnterAsObservable()
                .Select(damageable => damageable.GetComponent<IDamageable>())
                .Where(damageable => damageable != null)
                .Subscribe(damageable =>
                {
                    damageable.ApplyDamage();
                    
                    //GameEffectManager.Instance.ShakeCamera(0.25f);

                    GameEffectManager.Instance.OnGenelateEffect(
                        transform.position,
                        EffectType.Slash);
                });
            
            gameObject.SetActive(false);
        }
    }
}
