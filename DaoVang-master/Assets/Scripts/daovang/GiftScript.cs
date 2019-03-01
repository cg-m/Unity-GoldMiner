using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftScript : MonoBehaviour {

    int randomGift;

    public bool isMoveFollow = false;
    public float maxY;
    public float speed;
    // Use this for initialization
    void Start () {
        isMoveFollow = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        moveFllowTarget(GameObject.Find("luoiCau").transform);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "luoiCau"
            && GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction != TypeAction.KeoCau)
        {
            LuoiCauScript.instance.cameraOut = false;
            isMoveFollow = true;
            GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction = TypeAction.KeoCau;
            GameObject.Find("luoiCau").GetComponent<LuoiCauScript>().velocity = -GameObject.Find("luoiCau").GetComponent<LuoiCauScript>().velocity;
            GameObject.Find("luoiCau").GetComponent<LuoiCauScript>().speed -= this.speed;
            if (GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.KeoCau)
            {
                //this.gameObject.transform.rotation = Quaternion.Euler(0, 0, DayCauScript.instance.rotationDay * 70);
            }
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
                                             target.position.y - gameObject.GetComponent<Collider2D>().bounds.size.y / 2,
                                             transform.position.z);
            if (GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.Nghi)
            {
                if (CGameManager.instance.clover)
                {
                    randomGift = Random.RandomRange(1, 5);
                    switch (randomGift)
                    {
                        case 1:
                            GamePlayScript.instance.Power();
                            break;
                        case 2:
                            int score = Random.RandomRange(300, 400);
                            GameObject.Find("GamePlay").GetComponent<GamePlayScript>().score += score;
                            GameObject.Find("GamePlay").GetComponent<GamePlayScript>().CreateScoreFly(score);
                            break;
                        case 3:
                            GamePlayScript.instance.CreateBoomFly();
                            break;
                        case 4:
                            GamePlayScript.instance.Power();
                            break;
                    }
                }
                else
                {
                    randomGift = Random.RandomRange(1, 8);
                    //randomGift = 2;
                    switch (randomGift)
                    {
                        case 1:
                            int score = Random.RandomRange(50, 150);
                            GameObject.Find("GamePlay").GetComponent<GamePlayScript>().score += score;
                            GameObject.Find("GamePlay").GetComponent<GamePlayScript>().CreateScoreFly(score);
                            break;
                        case 2:
                            GamePlayScript.instance.CreateBoomFly();
                            break;
                        case 4:
                            GamePlayScript.instance.Power();
                            break;
                        case 5:
                            GamePlayScript.instance.CreateBoomFly();
                            break;
                        case 6:
                            int score6 = Random.RandomRange(50, 150);
                            GameObject.Find("GamePlay").GetComponent<GamePlayScript>().score += score6;
                            GameObject.Find("GamePlay").GetComponent<GamePlayScript>().CreateScoreFly(score6);
                            break;
                        case 7:
                            int score7 = Random.RandomRange(50, 150);
                            GameObject.Find("GamePlay").GetComponent<GamePlayScript>().score += score7;
                            GameObject.Find("GamePlay").GetComponent<GamePlayScript>().CreateScoreFly(score7);
                            break;

                    }
                }
                
                
                Destroy(gameObject);
            }
        }
    }


}
