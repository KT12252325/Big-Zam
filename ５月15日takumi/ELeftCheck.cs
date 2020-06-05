using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELeftCheck : MonoBehaviour
{
    private string PlayerTag = "Player";
    public bool isleft = false;
    private bool isleftEnter, isleftStay, isleftExit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool Isleft()
    {
        if (isleftEnter || isleftStay)
        {
            isleft = true;
        }
       
        return isleft;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag)
        {
            isleftEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag)
        {
            isleftStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag)
        {
            isleftExit = true;
        }
    }
}