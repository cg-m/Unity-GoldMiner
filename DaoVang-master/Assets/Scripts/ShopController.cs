using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

    public static ShopController instance;
    public Text textLevel, textDollar;
    public Transform panelShop;
    public GameObject[] instanceItems = new GameObject[3];
    int[] randomCreateItem = new int[3];

    public AudioSource audioSound;
    public AudioClip pressButton;
    //GameObject[] item = new GameObject[3];
	// Use this for initialization
	void Start () {
        CGameManager.instance.DisableItems();
        UnityEngine.Time.timeScale = 1;
        MakeInstance();
        RandomItem();
        DisplayItems(1, 1);
        textLevel.text = "LEVEL " + PlayerPrefs.GetInt("MaxLevel");
        SetTextMoney();
	}
	
    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel("MainMenu");
        }
	}
    
    public void SetTextMoney()
    {
        audioSound.PlayOneShot(pressButton);
        textDollar.text = "$" + PlayerPrefs.GetInt("MaxDollar");
    }
    void RandomItem()
    {
        for(int i = 0; i < 3; i++)
        {
            int count = 0;
            while(count < 3)
            {
                int ran = Random.RandomRange(1, 7);
                if (!CheckSame(ran))
                {
                    randomCreateItem[i] = ran;
                    count++;
                }
            }
            
            
        }
    }

    bool CheckSame(int m)
    {
        for(int i = 0; i < 3; i++)
        {
            if (randomCreateItem[i] == m)
            {
                return true;
            }
        }
        return false;
    }
    void DisplayItems(int position, int item)
    {
        //switch (position)
        //{
        //    case 1:
        //        Vector3 vector1 = new Vector3(-130, -260, 0);
        //        Instantiate(Resources.Load("" + item), vector1, Quaternion.identity);
        //        break;
        //    case 2:
        //        Vector3 vector2 = new Vector3(-290, -260, 0);
        //        Instantiate(Resources.Load("" + item), vector2, Quaternion.identity);
        //        break;
        //    case 3:
        //        Vector3 vector3 = new Vector3(-450, -260, 0);
        //        Instantiate(Resources.Load("" + item), vector3, Quaternion.identity);
        //        break;
        //    case 4:
        //        Vector3 vector4 = new Vector3(-610, -260, 0);
        //        Instantiate(Resources.Load("" + item), vector4, Quaternion.identity);
        //        break;
        //    case 5:
        //        Vector3 vector5 = new Vector3(-770, -260, 0);
        //        Instantiate(Resources.Load("" + item), vector5, Quaternion.identity);
        //        break;
        //}
        for(int i = 0; i < 3; i++)
        {
            Vector3 vector3 = new Vector3(0, 120 - 225*i, 0);
            string nameLoad = "Item" + randomCreateItem[i];
            instanceItems[i]= Instantiate(Resources.Load(nameLoad), vector3, Quaternion.identity) as GameObject;
            instanceItems[i].transform.SetParent(panelShop, false);
            //item[i] = Instantiate(Resources.Load("Panel 1"), vector3, Quaternion.identity) as GameObject;
        }
    }
    public void PlayLevel()
    {
        audioSound.PlayOneShot(pressButton);
        Application.LoadLevel("NextLevel");
    }
}
