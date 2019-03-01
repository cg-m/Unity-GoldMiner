using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuotScript : MonoBehaviour {

    private float speedRun = 2f;
    private int quayDau = -1;
    private int quayLai = 1;

    public bool isMoveFollow = false;
    public float maxY;
    public int score;
    public float speed;
    // Use this for initialization
    void Start () {
        isMoveFollow = false;
    }
	
	// Update is called once per frame
	void Update () {
        ChuotChay();
	}
    void FixedUpdate()
    {
        moveFllowTarget(GameObject.Find("luoiCau").transform);
        //Debug.Log("Do xoay cua day cau: "+DayCauScript.instance.rotationDay*10);


    }
    void ChuotChay()
    {
        Vector3 temp = transform.position;
        temp.x += quayDau * speedRun * UnityEngine.Time.deltaTime;
        transform.position = temp;

        Vector3 scale = transform.localScale;
        scale.x = quayLai;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "QuayTrai")
        {
            //Debug.Log("Do Trig");
            quayDau = -1;
            quayLai = 1;
        }
        else if (collision.gameObject.name == "QuayPhai")
        {
            //Debug.Log("Do Trig");
            quayDau = 1;
            quayLai = -1;
        }
        else if(collision.gameObject.name == "luoiCau" && 
            GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction != TypeAction.KeoCau)
        {
            LuoiCauScript.instance.cameraOut = false;
            isMoveFollow = true;
            GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction = TypeAction.KeoCau;
            GameObject.Find("luoiCau").GetComponent<LuoiCauScript>().velocity = -GameObject.Find("luoiCau").GetComponent<LuoiCauScript>().velocity;
            GameObject.Find("luoiCau").GetComponent<LuoiCauScript>().speed -= this.speed;

            GamePlayScript.instance.itemSeclected = gameObject.name;
        }
    }

    void moveFllowTarget(Transform target)
    {
        if (isMoveFollow)
        {
            Quaternion tg = Quaternion.Euler(target.parent.transform.rotation.x,
                                             target.parent.transform.rotation.y,
                                             90 + target.parent.transform.rotation.z);
            //				transform.rotation = Quaternion.Slerp(transform.rotation, tg, 0.5f);
            transform.position = new Vector3(target.position.x,
                                             target.position.y - gameObject.GetComponent<Collider2D>().GetComponent<Collider2D>().bounds.size.y / 2,
                                             transform.position.z);
            if (GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.Nghi)
            {
                GameObject.Find("GamePlay").GetComponent<GamePlayScript>().score += this.score;
                GameObject.Find("GamePlay").GetComponent<GamePlayScript>().CreateScoreFly(this.score);
                Destroy(gameObject);
            }
        }
    }

}
