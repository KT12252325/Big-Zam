using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour
{
    private Vector3 velocity; // 速度

    // 毎フレーム呼び出される関数
    private void Update()
    {
        // 移動する
        transform.localPosition -= velocity;

    }

    // 弾を発射する時に初期化するための関数
    public void Init(float angle, float speed)
    {
        // 弾の発射角度をベクトルに変換する
        var direction = GetDirection(angle);

        // 発射角度と速さから速度を求める
        velocity = direction * speed;

        // 弾が進行方向を向くようにする
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;

        // 3 秒後に削除する
        Destroy(gameObject, 3);
    }
    // 指定された角度（ 0 ～ 360 ）をベクトルに変換して返す
    public static Vector3 GetDirection(float angle)
    {
        return new Vector3
        (
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad),
            0
        );
    }

}
