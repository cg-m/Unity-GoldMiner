using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombScript : MonoBehaviour {
    public GameObject buttonBuy, buttonNBuy;
    public Text textPrice;
    int bomb, price;
	// Use this for initialization
	void Start () {
        SetPrice();
        bomb = PlayerPrefs.GetInt("Bomb");
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("So lan nhan: " + press);
	}

    void SetPrice()
    {
        price = Random.RandomRange(50, 300);
        textPrice.text = "$" + price;
    }
    public void PressButton()
    {
        //Debug.Log("Da vao day");
        bomb++;
        PlayerPrefs.SetInt("Bomb", bomb);
        //Destroy(gameObject);
        int dollar = PlayerPrefs.GetInt("MaxDollar");
        PlayerPrefs.SetInt("MaxDollar", dollar - price);
        ShopController.instance.SetTextMoney();
        buttonNBuy.SetActive(true);
        buttonBuy.SetActive(false);
    }

}
