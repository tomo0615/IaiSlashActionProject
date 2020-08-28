using UnityEngine;

namespace Game.Player
{
    public class PlayerInput
    {
        public KeyCode 
            chargeKey = KeyCode.Mouse0,
            attackKey = KeyCode.Mouse0;

        private float _horizontal;

        private float _vertical;

        public void InputKeys()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
        }

        public Vector2 MoveDirection() =>
            (Vector2.right * _horizontal);

        public Vector2 JumpDirection() =>
            (Vector2.up * _vertical);

        public bool IsAttack => Input.GetKeyUp(attackKey);

        public bool IsCharge => Input.GetKey(chargeKey);
    }
}
