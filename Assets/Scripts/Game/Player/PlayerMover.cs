using UnityEngine;

public class PlayerMover
{
    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;
    private float dx;

    public PlayerMover(Rigidbody rigidbody, Transform transform)
    {
        _rigidbody = rigidbody;
        _transform = transform;
    }

    public void Move(Vector3 moveDirection)
    {
        _rigidbody.MovePosition(_transform.position + moveDirection * Time.deltaTime);


        //TODO:アニメーションで何とかする
        //向き変更
        if (moveDirection.x > 0)
        {
            _transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if(moveDirection.x < 0)
        {
            _transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    public void Jump(Vector2 jumpDirection)
    {
        _rigidbody.AddForce(jumpDirection);
    }
}
