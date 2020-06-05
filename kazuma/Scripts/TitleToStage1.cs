using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleToStage1 : MonoBehaviour
{
    public AudioClip SE;
    public AudioSource audioSource;

    uint wait = 0;
    bool SceneMove = false;
    void Start()
    {
        //AudioSource関数にアクセスして実体化
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = SE;
    }
    void Update()
    {
        if (SceneMove)
        {
            if (wait > 0)
            {
                Debug.Log(wait.ToString());
                --wait;
            }
            else
            {
                wait = 0;
                Debug.Log(wait.ToString());
                SceneManager.LoadScene("Stage1");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SEを鳴らす
            audioSource.PlayOneShot(SE);
            SceneMove = true;
            wait = 15;
        }
    }
}
