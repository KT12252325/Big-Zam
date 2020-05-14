using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialToTitle : MonoBehaviour
{
    uint wait = 0;
    bool SceneMove = false;
    void Start()
    {

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
                SceneManager.LoadScene("Title");
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneMove = true;
            wait = 15;
        }
    }
}
