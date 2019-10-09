using UnityEngine;

public class AttackObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var attackable = other.GetComponent<IAttackable>();
        if (attackable != null)
        {
            attackable.Attacked();

            GameEffectManager.Instance.ShakeCamera(0.25f);

            GameEffectManager.Instance.OnGenelateEffect(
                transform.position,
                EffectType.Slash);
        }
    }

}
