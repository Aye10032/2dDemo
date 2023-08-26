using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //翻转人物贴图
    private Vector3 _flippedScale = new Vector3(-1, 1, 1);

    private float _moveX;

    // Start is called before the first frame update
    void Start()
    {
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