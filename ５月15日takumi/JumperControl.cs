using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperControl : MonoBehaviour
{
    //Rigidbodey2DコンポーネントにアクセスするRigidbody型変数rb2dを宣言
    Rigidbody2D rb2d;
    //AnimatorコンポーネントにアクセスするAnimator型変数animを宣言
    Animator anim;

    //体力
    public int hp = 5;

    //ジャンプ力
    float flap = 150f;
    //ジャンプしているかいないか
    bool jump = false;
    //画面に移ったら行動開始
    private SpriteRenderer sr = null;
    //ジャンプ時の音
    public AudioClip sound1;
    AudioSource audio;
    // 爆発効果音
    public AudioClip explosionSE;


    void Start()
    {
        //Rigidbody2D関数にアクセスして実体化
        rb2d = GetComponent<Rigidbody2D>();
        //Animator関数にアクセスして実体化
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        //Componentを取得
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (sr.isVisible)
        {
            if (jump == false)
            {
                rb2d.AddForce(Vector2.up * flap);
                jump = true;
                 if(jump==true)
            {
                //音(sound1)を鳴らす
                audio.Play();
            }
            }
            else
            {
                anim.SetBool("JumperJump", true);
            }
            //hpが0になった時死ぬ
            if (hp <= 0)
            {
                FindObjectOfType<Score>().AddPoint(100);
                // オーディオを再生
                AudioSource.PlayClipAtPoint(explosionSE, transform.position);
                Destroy(gameObject);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jump = false;
            anim.SetBool("JumperJump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //弾にあたったらhpが1減る
        if (collider.gameObject.CompareTag("Bullet"))
        {
            hp -= 1;
        }
    }
}
