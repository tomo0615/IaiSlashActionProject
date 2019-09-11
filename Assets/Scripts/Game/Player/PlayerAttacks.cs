
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private PlayerController _playerCtrl;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerCtrl = GetComponent<PlayerController>();
    }

    public void IaiSlash()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.localPosition);

        var lookDirection = (Vector2)(Input.mousePosition - pos);
        lookDirection = lookDirection.normalized;

        var rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
        transform.localRotation = rotation * Quaternion.Euler(0f, 0f, 90f);

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.velocity = lookDirection * _playerCtrl.SlashSpeed;
    }

    public void ChargeSlashCount()
    { 
        _playerCtrl.slashCount += Time.deltaTime; //TODO：指数的に増やす
    }
}
