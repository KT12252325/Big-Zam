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
    //頭をぶつけているかどうか
    private bool isHead = false;
    //ジャンプしているかいないか
    private bool jump = false;
    //ジャンプした場所を記録
    private float jumpPos = 0.0f;
    //マウス用位置座標
    private Vector3 mpos;
    //スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;

    //パブリック変数
    //速度
    public float speed;
    //接地
    public GroundCheck ground;
    //頭をぶつけた判定
    public GroundCheck head;
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

    void FixedUpdate()
    {
        //接地判定を得る
        isGround = ground.IsGround();
        isHead = head.IsGround();

        //キー入力されたら行動する
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

        if (Input.GetMouseButtonDown(0))
        {
            //撃つアニメーション
            anim.SetBool("shot", true);
        }
        else
        {
            anim.SetBool("shot", false);
        }

        if (!Input.GetKey(KeyCode.S))
        {
            if(Input.GetKey(KeyCode.D))
            {
                //transform.localScale = new Vector3(1, 1, 1);
                anim.SetBool("run", true);
                xSpeed = speed;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                //transform.localScale = new Vector3(-1, 1, 1);
                anim.SetBool("run", true);
                xSpeed = -speed;
            }
            else
            {
                anim.SetBool("run", false);
                xSpeed = 0.0f;
            }
        }

        //Vector3でマウス位置座標を取得する
        mpos = Input.mousePosition;
        //Z軸修正
        mpos.z = 10f;
        //マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(mpos);

        //動く方向にキャラが向くように画像を反転させる処理
        if (transform.position.x < screenToWorldPointPosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x > screenToWorldPointPosition.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //ジャンプ制御
        if (isGround)
        {
            if (verticalKey > 0 )
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
                anim.SetBool("jumpAtk", false);
            }
        }
        else if(jump)
        {
            //Wキーを押されている。かつ、現在の高さがジャンプした位置から自分の決めた位置より下ならジャンプを継続する
            if(verticalKey > 0 && jumpPos + jumpHeight > transform.position.y)
            {
                ySpeed = jumpSpeed;
                if(isHead == true)
                {
                    jump = false;
                }
            }
            else
            {
                jump = false;
            }

        }
        if(!isGround)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetBool("jumpAtk", true);
            }
        }
        
        rb2d.velocity = new Vector2(xSpeed, ySpeed);
    }
}