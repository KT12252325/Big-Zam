using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //プライベート変数
    private Slider playerHPSlider;

　　//パブリック変数
    //エフェクト
    public GameObject effectPrefab;
    //体力
    public int playerHP;
    //プレイヤー残機の配列
    public GameObject[] playerIcons;
    //プレイヤーがやられた回数のカウンター
    public int deathCount = 0;

    private void Start()
    {
        playerHPSlider = GameObject.Find("PlayerHPSlider").GetComponent<Slider>();
        playerHPSlider.maxValue = playerHP;
        playerHPSlider.value = playerHP;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHP -= 1;

            playerHPSlider.value = playerHP;

            if (playerHP == 0)
            {
                //HPが0になったらカウント+1
                deathCount += 1;

                UpdatePlayerIcons();

                GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 1.0f);
                //プレイヤーを非アクティブにする
                this.gameObject.SetActive(false);

                //リトライの命令ブロックを1秒後に呼び出す  
                Invoke("Retry", 1.0f);
            }
        }
    }

    //プレイヤーの残機数を表示する命令ブロック
    void UpdatePlayerIcons()
    {
        for(int i = 0;i < playerIcons.Length;i++)
        {
            if(deathCount <= i)
            {
                playerIcons[i].SetActive(true);
            }
            else
            {
                playerIcons[i].SetActive(false);
            }
        }
    }

    //リトライ
    void Retry()
    {
        this.gameObject.SetActive(true);
        playerHP = 5;
        playerHPSlider.value = playerHP;
    }
}
