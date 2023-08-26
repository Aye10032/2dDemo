using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpSpeed = 1f;

    private int _moveStatus;

    //翻转人物贴图
    private readonly Vector3 _flippedScale = new Vector3(-1, 1, 1);
    private float _moveX;
    private float _moveY;

    //刚体组件
    private Rigidbody2D _rigidbody2D;

    //动画组件
    private Animator _animator;
    private static readonly int MovementTag = Animator.StringToHash("movement");
    private static readonly int OnGroundTag = Animator.StringToHash("isOnGround");
    private static readonly int JumpTag = Animator.StringToHash("jump");

    //是否在地面
    private bool _isOnGround;

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
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsOnGround(collision, false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        IsOnGround(collision, false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsOnGround(collision, true);
    }

    private void Movement()
    {
        //获得移动数据
        _moveX = Input.GetAxis("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical"); //竖直方向不需要大小

        //改变人物速度
        _rigidbody2D.velocity = new Vector2(_moveX * moveSpeed, _rigidbody2D.velocity.y);

        //改变动画状态
        if (_moveX > 0)
        {
            _moveStatus = 1;
        }
        else if (_moveX < 0)
        {
            _moveStatus = -1;
        }
        else
        {
            _moveStatus = 0;
        }

        _animator.SetInteger(MovementTag, _moveStatus);
    }

    //改变人物朝向
    private void Direction()
    {
        //向右
        if (_moveX > 0)
        {
            transform.localScale = _flippedScale;
        }
        else if (_moveX < 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    //处理人物跳跃
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            //给予持续的向上的力
            _rigidbody2D.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Force);

            //触发跳跃动画
            _animator.SetTrigger(JumpTag);
        }
    }

    private void IsOnGround(Collision2D collision, bool exit)
    {
        //离开时
        if (exit)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            {
                Debug.Log("leave");
                _isOnGround = false;
            }
        }
        else
        {
            Debug.Log("enter");
            if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain") && !_isOnGround)
            {
                if (collision.contacts[0].normal == Vector2.up) //从上方进入碰撞体
                {
                    _isOnGround = true;
                    CancelJump();
                }
                else if (collision.contacts[0].normal == Vector2.down)
                {
                    CancelJump();
                }
            }
        }
        
        _animator.SetBool(OnGroundTag, _isOnGround);
    }

    //取消跳跃状态
    private void CancelJump()
    {
        _animator.ResetTrigger(JumpTag);
    }
}