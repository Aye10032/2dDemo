using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly Vector3 _flippedScale = new(-1, 1, 1);
    private float _moveX;
    private float _moveY;
    private int _moveChangeAnim;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    public float moveSpeed = 10f;
    private static readonly int MovementId = Animator.StringToHash("movement");

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Direction();
    }

    private void Movement()
    {
        _moveX = Input.GetAxis("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical");

        _rigidbody2D.velocity = new Vector2(_moveX * moveSpeed, _rigidbody2D.velocity.y);

        // 就是switch
        _moveChangeAnim = _moveX switch
        {
            > 0 => 1,
            < 0 => -1,
            _ => 0
        };

        _animator.SetInteger(MovementId, _moveChangeAnim);
    }

    private void Direction()
    {
        if (_moveX > 0)
        {
            transform.localScale = _flippedScale;
        }
        else if (_moveX < 0)
        {
            transform.localScale = Vector3.one;
        }
    }
}