using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //プライベート変数
    //Rigidbodey2DコンポーネントにアクセスするRigidbody型変数rb2dを宣言
    private Rigidbody2D rb2d;
    //AnimatorコンポーネントにアクセスするAnimator型変数animを宣言
    private Animator anim;
    //接地判定
    private bool isGround = false;
    //ジャンプしているかいないか
    private bool jump = false;
    //ジャンプした場所を記録
    private float jumpPos = 0.0f;

    //パブリック変数
    //速度
    public float speed;
    //接地
    public GroundCheck ground;
    //重力
    public float gravity;
    //ジャンプ力
    public float jumpSpeed;
    //ジャンプした場所の高さ
    public float jumpHeight;

    void Start()
    {
        //Rigidbody2D関数にアクセスして実体化
        rb2d = GetComponent<Rigidbody2D>();
        //Animator関数にアクセスして実体化
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //接地判定を得る
        isGround = ground.IsGround();

        //キー入力されたら行動する
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        float ySpeed = -gravity;
        float verticalKey = Input.GetAxis("Vertical");

        //Sを押したらしゃがむアニメーション
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("squat", true);
        }
        else
        {
            anim.SetBool("squat", false);
        }

        if (!Input.GetKey(KeyCode.S))
        {
            if (horizontalKey > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                anim.SetBool("run", true);
                xSpeed = speed;
            }
            else if (horizontalKey < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                anim.SetBool("run", true);
                xSpeed = -speed;
            }
            else
            {
                anim.SetBool("run", false);
                xSpeed = 0.0f;
            }
        }

        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //動く方向にキャラが向くように画像を反転させる処理
        //ジャンプ制御
        if (isGround)
        {
            if (verticalKey > 0)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;
                jump = true;
                anim.SetBool("jump", true);
            }
            else
            {
                jump = false;
                anim.SetBool("jump", false);
            }
        }
        else if (jump)
        {
            //Wキーを押されている。かつ、現在の高さがジャンプした位置から自分の決めた位置より下ならジャンプを継続する
            if (verticalKey > 0 && jumpPos + jumpHeight > transform.position.y)
            {
                ySpeed = jumpSpeed;
            }
            else
            {
                jump = false;
            }
        }
        rb2d.velocity = new Vector2(xSpeed, ySpeed);
    }
}
