using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2ToStage3 : MonoBehaviour
{
    uint wait = 0;
    bool SceneMove = false;

    GameObject[] enemyObjects;
    public int enemyNum;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        enemyNum = enemyObjects.Length;

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
                SceneManager.LoadScene("Stage3");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && enemyNum == 0)
        {
            SceneMove = true;
            wait = 15;
        }
    }
}
