using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemSButtonScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if(gameObject.name == "Button Power")
        {

        }
        else if(gameObject.name == "Button Clover")
        {
            
        }
        else if(gameObject.name == "Button Book")
        {

        }
        else if(gameObject.name == "Button Bomb")
        {

        }
        else
        {

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
