﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus1Controller : MonoBehaviour
{
    //プレイヤーのオブジェクト
    public GameObject player;
    //弾のプレハブオブジェクト
    public EBullet enemyBullet;
    //プレイヤーの方を向く
    public ELeftCheck left;
    public ERightCheck right;
    // 弾の移動の速さ
    public float EshotSpeed;
    // 複数の弾を発射する時の角度
    public float EshotAngleRange;
    // 弾の発射タイミングを管理するタイマー
    public float EshotTimer;
    // 弾の発射数
    public int EshotCount;
    // 弾の発射間隔（秒）
    public float EshotInterval;

    public float angle = 0;

    private bool isleft = false;
    private bool isright = false;

    //体力
    public int hp = 3;
    //画面に移ったら行動開始
    private SpriteRenderer sr = null;
    // 爆発効果音
    public AudioClip explosionSE;
    //エフェクト
    public GameObject effectPrefab;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        isleft = left.Isleft();
        isright = right.Isright();
        if (sr.isVisible)
        {

            // 弾の発射タイミングを管理するタイマーを更新する
            EshotTimer += Time.deltaTime;

            // まだ弾の発射タイミングではない場合は、ここで処理を終える
            if (EshotTimer < EshotInterval) return;

            // 弾の発射タイミングを管理するタイマーをリセットする
            EshotTimer = 0;

            if (isleft)
            {
                //EnemyのlocalScaleを変数に格納
                Vector2 temp = gameObject.transform.localScale;
                //localScale.xに1をかける
                temp.x *= 1;
                //結果を戻す
                gameObject.transform.localScale = temp;
                // 弾を発射する
                ShootNWay(angle, EshotAngleRange, EshotSpeed, EshotCount);
            }
            if (isright)
            {
                //EnemyのlocalScaleを変数に格納
                Vector2 temp = gameObject.transform.localScale;
                //localScale.xに-1をかける
                temp.x *= -1;
                //結果を戻す
                gameObject.transform.localScale = temp;
                // 弾を発射する
                ShootNWay(angle-90, EshotAngleRange, EshotSpeed, EshotCount);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //弾に当たったらhpが1減る
        if (collider.gameObject.CompareTag("Bullet"))
        {
            hp -= 1;
            //hpが0になった時死ぬ
            if (hp <= 0)
            {
                FindObjectOfType<Score>().AddPoint(100);
                GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 1.0f);
                // オーディオを再生
                AudioSource.PlayClipAtPoint(explosionSE, transform.position);
                Destroy(gameObject);
            }
        }
    }
    // 弾を発射する関数
    private void ShootNWay(
        float angleBase, float angleRange, float speed, int count)
    {
        var pos = transform.localPosition; // プレイヤーの位置
        var rot = transform.localRotation; // プレイヤーの向き

        // pos.y = -1;
        //pos.x = -1/9;

        // 弾を複数発射する場合
        if (1 < count)
        {
            // 発射する回数分ループする
            for (int i = 0; i < count; ++i)
            {
                // 弾の発射角度を計算する
                var angle = angleBase +
                    angleRange * ((float)i / (count - 1) - 0.5f);

                // 発射する弾を生成する
                var ebullet = Instantiate(enemyBullet, pos, rot);

                // 弾を発射する方向と速さを設定する
                ebullet.Init(angle, speed);
            }
        }
        // 弾を 1 つだけ発射する場合
        else if (count == 1)
        {
            // 発射する弾を生成する
            var ebullet = Instantiate(enemyBullet, pos, rot);

            // 弾を発射する方向と速さを設定する
            ebullet.Init(angleBase, speed);
        }


    }
}
