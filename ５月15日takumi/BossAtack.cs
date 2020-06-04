using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtack : MonoBehaviour
{
    //プレイヤーのオブジェクト
    public GameObject player;
    //弾のプレハブオブジェクト
    public EBullet enemyBullet;
   
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

 
  
    //画面に移ったら行動開始
    private SpriteRenderer sr = null;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
       
        if (sr.isVisible)
        {
            // 弾の発射タイミングを管理するタイマーを更新する
            EshotTimer += Time.deltaTime;

            // まだ弾の発射タイミングではない場合は、ここで処理を終える
            if (EshotTimer < EshotInterval) return;

            // 弾の発射タイミングを管理するタイマーをリセットする
            EshotTimer = 0;

           
        }
    }

  
    
    // 弾を発射する関数
    private void ShootNWay(
        float angleBase, float angleRange, float speed, int count)
    {
        var pos = transform.localPosition; // プレイヤーの位置
        var rot = transform.localRotation; // プレイヤーの向き

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
