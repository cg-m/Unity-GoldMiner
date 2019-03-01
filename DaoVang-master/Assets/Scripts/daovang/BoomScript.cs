using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomScript : MonoBehaviour {

    //private float xEnd = -500, yEnd = 340;
    Vector3 position, scale;
    int bomb;
    // Use this for initialization
    void Start()
    {
        position = transform.localPosition;
        scale = transform.localScale;

        //Debug.Log("Toa do luc dau: x=" + position.x + " y=" + position.y);
    }

    // Update is called once per frame
    void Update()
    {
        BoomMove();
        

    }
    void BoomMove()
    {
        float x = position.x;
        float y = position.y;

        float xScale = scale.x;
        float yScale = scale.y;
        if (x < 212)
        {
            x += x * UnityEngine.Time.deltaTime * 2;
            position.x = x;
        }
        else
        {
            xScale -= UnityEngine.Time.deltaTime * 4;
            scale.x = xScale;

            yScale -= UnityEngine.Time.deltaTime * 4;
            scale.y = yScale;
        }
        if (y < 338)
        {
            y += y * UnityEngine.Time.deltaTime * 2;
            position.y = y;
        }
        else
        {
            xScale += UnityEngine.Time.deltaTime * 2;
            scale.x = xScale;

            yScale += UnityEngine.Time.deltaTime * 2;
            scale.y = yScale;
        }
        transform.localScale = scale;
        transform.localPosition = position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "boomObject")
        {
            //Debug.Log("cham vao boom object");
            bomb = PlayerPrefs.GetInt("Bomb");
            bomb++;
            PlayerPrefs.SetInt("Bomb", bomb);
            GamePlayScript.instance.SetNumberBoom();
            Destroy(gameObject, 0.5f);
        }
    }
}
