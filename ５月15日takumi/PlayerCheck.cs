using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{ 
    private string PlayerTag = "Player";
    public bool isreft = false;
    private bool isreftEnter, isreftStay, isreftExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool Isreft()
    {
        if (isreftEnter || isreftStay)
        {
            isreft = true;
        }
        else if (isreftExit)
        {
            isreft = false;
        }

        isreftEnter = false;
        isreftStay = false;
        isreftExit = false;
        return isreft;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag)
        {
            isreftEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag)
        {
            isreftStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag)
        {
            isreftExit = true;
        }
    }
}
