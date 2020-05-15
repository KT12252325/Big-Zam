using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCheck : MonoBehaviour
{
    private string PlayerTag = "Player";
    public bool isright = false;
    private bool isrightEnter, isrightStay, isrightExit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool Isright()
    {
        if (isrightEnter || isrightStay)
        {
            isright = true;
        }
        else if (isrightExit)
        {
            isright = false;
        }

        isrightEnter = false;
        isrightStay = false;
        isrightExit = false;
        return isright;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag)
        {
            isrightEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag)
        {
            isrightStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag)
        {
            isrightExit = true;
        }
    }
}