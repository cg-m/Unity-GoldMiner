using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTouchScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name == "Boom")
        {
            if(GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.KeoCau)
            {
                //Debug.Log("Nhan Vao Boom");
                GamePlayScript.instance.Boom();
            }
            
        }
        
        else
        {
            GameObject.Find("luoiCau").GetComponent<LuoiCauScript>().checkTouchScene();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
