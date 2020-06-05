using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    //プライベート変数
    private Slider playerHPSlider;
    private SpriteRenderer renderer;

    //パブリック変数
    //エフェクト
    public GameObject effectPrefab;
    //体力
    public int playerHP;
    //プレイヤー残機の配列
    public GameObject[] playerIcons;
    //プレイヤーがやられた回数のカウンター
    public int deathCount = 0;
    public bool on_damage = false;

    public GameObject startPoint;

    public AudioClip Revival_PotionSE;
    public AudioClip HP_PotionSE;
    public AudioSource audioSource;



    private void Start()
    {
        playerHPSlider = GameObject.Find("PlayerHPSlider").GetComponent<Slider>();
        playerHPSlider.maxValue = playerHP;
        playerHPSlider.value = playerHP;
        //点滅処理のために呼ぶ
        renderer = gameObject.GetComponent<SpriteRenderer>();

        //AudioSource関数にアクセスして実体化
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Revival_PotionSE;
        audioSource.clip = HP_PotionSE;
    }

    void Update()
    {
        //ダメージフラグがtrueだったら点滅
        if(on_damage)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            renderer.color = new Color(1f, 1f, 1f, level);
        }

        if (playerHP <= 0)
        {
            //HPが0になったらカウント+1
            deathCount += 1;
            if (deathCount > 3)
            {
                SceneManager.LoadScene("GameOver");
            }
            UpdatePlayerIcons();

            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 1.0f);
            //プレイヤーを非アクティブにする
            this.gameObject.SetActive(false);
            //リトライの命令ブロックを1秒後に呼び出す  
            Invoke("Retry", 1.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //敵とぶつかったかつ、ダメージフラグがfalseだったら
        if (!on_damage && collision.gameObject.CompareTag("Enemy"))
        {
            playerHP -= 1;

            playerHPSlider.value = playerHP;

            OnDamageEffect();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //弾に当たったらｈｐが1減る
        if (collider.gameObject.CompareTag("EnemyBullet"))
        {
            playerHP -= 1;

            playerHPSlider.value = playerHP;

            OnDamageEffect();
        }

        //入手したらHP回復
        if(collider.gameObject.CompareTag("HP_Potion"))
        {
            //SEを鳴らす
            audioSource.PlayOneShot(HP_PotionSE);
            playerHP += 3;

            playerHPSlider.value += 3;

            if(playerHP > 5)
            {
                playerHP = 5;
            }
        }

        if (collider.gameObject.CompareTag("Revival_Potion"))
        {
            //SEを鳴らす
            audioSource.PlayOneShot(Revival_PotionSE);
            deathCount -= 1;
            UpdatePlayerIcons();
            if (deathCount < 0)
            {
                deathCount = 0;
            }
        }
    }

    //ダメージを受けた際の動き
    void OnDamageEffect()
    {
        //ダメージフラグをON
        on_damage = true;
        
        //プレイヤーの位置を後ろに飛ばす
        float s = 10f * Time.deltaTime;
        transform.Translate(Vector3.up * s);

        //プレイヤーのlocalScaleでどちらを向いているのかを判定
        if(transform.localScale.x >= 0)
        {
            transform.Translate(Vector3.left * s);
        }
        else
        {
            transform.Translate(Vector3.right * s);
        }

        // コルーチン開始
        StartCoroutine("WaitForIt");
    }

    IEnumerator WaitForIt()
    {
        // 1秒間処理を止める
        yield return new WaitForSeconds(1);

        // １秒後ダメージフラグをfalseにして点滅を戻す
        on_damage = false;
        renderer.color = new Color(1f, 1f, 1f, 1f);
    }

    //プレイヤーの残機数を表示する命令ブロック
    void UpdatePlayerIcons()
    {
        for(int i = 0;i < playerIcons.Length;i++)
        {
            if (deathCount <= i)
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
        on_damage = false;
        renderer.color = new Color(1f, 1f, 1f, 1f);

        gameObject.transform.position = startPoint.transform.position;

        //// 現在のシーンを再読み込み
        //int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(sceneIndex);
    }
}
