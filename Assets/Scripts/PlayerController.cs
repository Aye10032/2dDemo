using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;

    //翻转人物贴图
    private readonly Vector3 _flippedScale = new Vector3(-1, 1, 1);
    private float _moveX;
    private float _moveY;

    //刚体组件
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Direction();
    }

    private void Movement()
    {
        //获得移动数据
        _moveX = Input.GetAxis("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical"); //竖直方向不需要大小

        //改变人物速度
        _rigidbody2D.velocity = new Vector2(_moveX * moveSpeed, _rigidbody2D.velocity.y);
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
}