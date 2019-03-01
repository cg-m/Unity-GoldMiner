using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPanelScript : MonoBehaviour {

    Vector3 scale;
    float timeDelay;
	// Use this for initialization
	void Start () {
        scale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        PanelScale();
	}

    void PanelScale()
    {
        float xScale = scale.x;
        float yScale = scale.y;

        xScale += UnityEngine.Time.deltaTime * 2;
        yScale += UnityEngine.Time.deltaTime * 2;

        if(xScale > 1.5f)
        {
            xScale = 1.5f;
            yScale = 1.5f;
        }
        //timeDelay += UnityEngine.Time.deltaTime * 10;
        //if (timeDelay > 10)
        //{
        //    if(xScale < 1)
        //    {
        //        xScale = 1;
        //        yScale = 1;
        //    }
        //    else
        //    {
        //        xScale -= UnityEngine.Time.deltaTime * 8;
        //        yScale -= UnityEngine.Time.deltaTime * 8;
        //    }
        //}

        scale.x = xScale;
        scale.y = yScale;
        transform.localScale = scale;
    }
}
