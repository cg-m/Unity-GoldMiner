using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockScript : MonoBehaviour {

    public Text textPrice;
    public GameObject buttonBuy, buttonNBuy;
    int price;
    // Use this for initialization
    void Start()
    {
        //Vector3 vector3 = transform.position;
        //Debug.Log("Toa do cua Items X=" + vector3.x + " Y=" + vector3.y);
        SetPrice();  
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("So lan nhan: " + press);
    }

    void SetPrice()
    {
        price = Random.RandomRange(10, 70);
        textPrice.text = "$" + price;
    }
    public void Buy()
    {
        int dollar = PlayerPrefs.GetInt("MaxDollar");
        PlayerPrefs.SetInt("MaxDollar", dollar - price);
        ShopController.instance.SetTextMoney();
        //Debug.Log("Da vao day");
        CGameManager.instance.clock = true;            
            //Destroy(gameObject);
        buttonNBuy.SetActive(true);
        buttonBuy.SetActive(false);
    }

}