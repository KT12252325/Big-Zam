﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClabControl : MonoBehaviour
{
    //Rigidbodey2DコンポーネントにアクセスするRigidbody型変数rb2dを宣言
    Rigidbody2D rb2d;
    //AnimatorコンポーネントにアクセスするAnimator型変数animを宣言
    private Animator anim;
    //プレイヤーの方を向く
    public ELeftCheck left;
    public ERightCheck right;
    //体力
    public int hp = 7;
    //移動速度
    public float speed = 2.0f;
    //画面に移ったら行動開始
    private SpriteRenderer sr = null;

    private bool isleft = false;
    private bool isright = false;

    void Start()
    {
        //Rigidbody2D関数にアクセスして実体化
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //Animator関数にアクセスして実体化
        anim = GetComponent<Animator>();
    }

    void Update()
    {
       
        if (sr.isVisible)
        {
            rb2d.velocity = new Vector2(-transform.localScale.x * speed, rb2d.velocity.y);
            
        }
       
        //hpが0になった時死ぬ
        if (hp <= 0)
        {
            FindObjectOfType<Score>().AddPoint(100);
            Destroy(gameObject);
        }
       
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Wallタグのついたオブジェクトと衝突したら
        if (col.gameObject.tag == "Wall")
        {
            //EnemyのlocalScaleを変数に格納
            Vector2 temp = gameObject.transform.localScale;
            //localScale.xに-1をかける
            temp.x *= -1;
            //結果を戻す
            gameObject.transform.localScale = temp;
        }
        //Wallタグのついたオブジェクトと衝突したら
        if (col.gameObject.tag == "Enemy")
        {
            //EnemyのlocalScaleを変数に格納
            Vector2 temp = gameObject.transform.localScale;
            //localScale.xに-1をかける
            temp.x *= -1;
            //結果を戻す
            gameObject.transform.localScale = temp;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //弾に当たったらｈｐが1減る
        if (collider.gameObject.CompareTag("Bullet"))
        {
            hp -= 1;
        }
    }
}

