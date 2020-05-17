using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    //AnimatorコンポーネントにアクセスするAnimator型変数animを宣言
    private Animator anim;

    public AudioClip atackSE;
    public AudioSource audioSource;

    public GameObject bullet;
    float speed;
    public float animCount = 10;
    bool shot = false;

    void Start()
    {
        //弾速
        speed = 4.0f;
        //Animator関数にアクセスして実体化
        anim = GetComponent<Animator>();

        //AudioSource関数にアクセスして実体化
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = atackSE;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //SEを鳴らす
            audioSource.PlayOneShot(atackSE);
            //撃つアニメーション
            anim.SetBool("shot", true);
            // 弾（ゲームオブジェクト）の生成
            GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);
            // クリックした座標の取得（スクリーン座標からワールド座標に変換）
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 向きの生成（Z成分の除去と正規化）
            Vector3 shotForward = Vector3.Scale((mouseWorldPos - transform.position), new Vector3(1, 1, 0)).normalized;
            // 弾に速度を与える
            clone.GetComponent<Rigidbody2D>().velocity = shotForward * speed;
        }
        else
        {
            anim.SetBool("shot", false);
        }
        
    }
}