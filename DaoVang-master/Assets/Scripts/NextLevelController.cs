using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelController : MonoBehaviour {

    public Text textTarget, textLevel;
    float timeDelay;
	// Use this for initialization
	void Start () {
       
        textTarget.text = "$" + CGameManager.instance.GetScoreTarget(CGameManager.instance.levelCurrent).ToString();
        textLevel.text = "LEVEL " + CGameManager.instance.levelCurrent;
	}
	
	// Update is called once per frame
	void Update () {
        timeDelay += UnityEngine.Time.deltaTime * 10;
        if(timeDelay > 25)
        {
            //Application.LoadLevel("NghiaDemo");
            int level = CGameManager.instance.levelCurrent;
            if(level <= 10)
            {
                Application.LoadLevel("Level" + level);
            }
            else
            {
                int ran = Random.RandomRange(1, 10);
                Application.LoadLevel("Level" + ran);

            }
            //else if(level > 10 && level <= 20)
            //{
            //    level -= 10;
            //    Application.LoadLevel("Level" + level);
            //}
            //else if (level > 20 && level <= 30)
            //{
            //    level -= 20;
            //    Application.LoadLevel("Level" + level);
            //}
            //else if (level > 30 && level <= 40)
            //{
            //    level -= 30;
            //    Application.LoadLevel("Level" + level);
            //}
            //else if (level > 40 && level <= 50)
            //{
            //    level -= 40;
            //    Application.LoadLevel("Level" + level);
            //}
            
        }
	}
}
