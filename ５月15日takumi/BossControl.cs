using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    //Rigidbodey2DコンポーネントにアクセスするRigidbody型変数rb2dを宣言
    Rigidbody2D rb2d;
    //プレイヤーの方を向く
    public ELeftCheck left;
    public ERightCheck right;
    //体力
    public int hp = 50;
    //移動速度
    public float speed = 2.0f;
    //ジャンプしているかいないか
    bool jump = false;
    //ジャンプ力
    float flap = 200f;
    
    //画面に移ったら行動開始
    private SpriteRenderer sr = null;

    private bool isleft = false;
    private bool isright = false;
    // 爆発効果音
    public AudioClip explosionSE;
    //ジャンプ時の音
    public AudioClip sound1;
    AudioSource audio;
    void Start()
    {
        //Rigidbody2D関数にアクセスして実体化
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (sr.isVisible)
        {
            rb2d.velocity = new Vector2(-transform.localScale.x * speed, rb2d.velocity.y);
            if (jump == false)
            {
                rb2d.AddForce(Vector2.up * flap);
                jump = true;
                if(jump==true)
                {
                    audio.Play();
                }
            }
            if (isleft)
            {
                speed = speed * -1;
            }
            if (isright)
            {
                speed = speed * -1;
            }
        }

        //hpが0になった時死ぬ
        if (hp <= 0)
        {
            // オーディオを再生
            AudioSource.PlayClipAtPoint(explosionSE, transform.position);
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
        if (col.gameObject.CompareTag("Ground"))
        {
            jump = false;

        }
       
    }

   
}
