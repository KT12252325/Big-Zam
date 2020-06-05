using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    // ハイスコアを表示するUIの取得用
    public Text scoreText;

    int resultScore;

    void Start()
    {
        resultScore = Score.getScore();
    }

    void Update()
    {
        scoreText.text = resultScore.ToString();
    }
}
