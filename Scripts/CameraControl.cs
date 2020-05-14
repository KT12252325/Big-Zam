using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //ゲームオブジェクト型変数playerを宣言
    public GameObject player;

    void Start()
    {
        //Playerオブジェクトを見つけて代入し実体化
        this.player = GameObject.Find("Player");
    }

    void Update()
    {
        //playerPosに現在のplayerの座標を代入
        Vector3 playerPos = this.player.transform.position;
        //playerの動きに合わせてカメラのx,y座標を更新する。z座標はそのまま。
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);

        //カメラのx軸の下限、上限を設定
        if(transform.position.x < 1)
        {
            transform.position = new Vector3(1, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 6)
        {
            transform.position = new Vector3(6, transform.position.y, transform.position.z);
        }

        //カメラのy軸の下限を設定
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}
