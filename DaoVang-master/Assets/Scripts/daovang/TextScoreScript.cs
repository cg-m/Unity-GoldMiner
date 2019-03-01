using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScoreScript : MonoBehaviour {

    //private float xEnd = -500, yEnd = 340;
    Vector3 position, scale;
    public Text txtScore;
    public static int score;
    public GameObject endFly;
	// Use this for initialization
	void Start () {
        position = transform.localPosition;
        scale = transform.localScale;

        //Debug.Log("Toa do luc dau: x=" + position.x + " y=" + position.y);
	}
	
	// Update is called once per frame
	void Update () {
        txtScore.text = "$"+ score.ToString();
        TextMove();
        
        
	}
    void TextMove()
    {
        float x = position.x;
        float y = position.y;

        float xScale = scale.x;
        float yScale = scale.y;
        if (x > -500)
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
        if (y < 450)
        {
            y += y * UnityEngine.Time.deltaTime * 2;
            position.y = y;
        }
        else
        {
            xScale += UnityEngine.Time.deltaTime * 0.4f;
            scale.x = xScale;

            yScale += UnityEngine.Time.deltaTime * 0.4f;
            scale.y = yScale;
        }
        transform.localScale = scale;
        transform.localPosition = position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "scoreObject")
        {
            //Debug.Log("cham vao score object");
            GamePlayScript.instance.ScoreZoomEffect();
            GamePlayScript.instance.SetScoreText();
            Destroy(gameObject, 0.4f);
        }
    }
}
