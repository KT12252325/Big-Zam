using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusControl : MonoBehaviour
{
    //プレイヤーのオブジェクト
    public GameObject player;
    //弾のプレハブオブジェクト
    public GameObject enemyBullet;

    //3秒ごとに弾を発射するためのもの
    private float targetTime =1.0f;
    private float currentTime = 0;

    //体力
    public int hp = 3;
    //画面に移ったら行動開始
    private SpriteRenderer sr = null;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(sr.isVisible)
        {
            //3秒たつごとに弾を発射する
            currentTime += Time.deltaTime;
            if (targetTime < currentTime)
            {
                currentTime = 0;
                //敵の座標を変数posに保存
                var pos = this.gameObject.transform.position;

                //弾のプレハブを作成
                var b = Instantiate(enemyBullet) as GameObject;

                //弾のプレハブの位置を敵の位置にする
                b.transform.position = pos;

                //敵からプレイヤーに向かうベクトルを作る
                //プレイヤーの位置から敵の位置(弾の位置)を引く
                Vector2 vec = player.transform.position - pos;

                //弾のRigidbody2Dコンポーネントのvelocityに先ほど求めたベクトルを入れて力を加える
                b.GetComponent<Rigidbody2D>().velocity = vec;

                Destroy(b, 3.0f);
            }
        }
        


        //hpが0になった時死ぬ
        if (hp == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //弾に当たったらhpが1減る
        if (collider.gameObject.CompareTag("Bullet"))
        {
            hp -= 1;
        }
    }
}
