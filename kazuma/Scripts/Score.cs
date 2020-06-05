using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // スコアを表示するUIの取得用
    public Text scoreText;

    // スコアのカウント用
    public static int score;

    void Start()
    {
        //Initialize();
    }

    void Update()
    {
        // 現在のスコアを画面に表示する
        scoreText.text = score.ToString();
    }

    // ゲーム開始前の状態に戻す
    public static int resetScore()
    {
        // スコアを0に戻す
        return score = 0;
    }

    // ポイントの追加。修飾子をpublicにしているので外部より参照できるメソッドになっている
    public void AddPoint(int point)　　　　　　//　外部より受け取ったint型の引数をpointとして受け取る
    {
        score += point;
    }

    public static int getScore()
    {
        return score;
    }
}
