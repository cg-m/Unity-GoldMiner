using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OngGiaScript : MonoBehaviour {
    public static OngGiaScript instance;
    private Animator anim;

    private GameObject halo;
    Vector3 position;
	// Use this for initialization
	void Start () {
        MakeInstance();
        anim = GetComponent<Animator>();
        position = transform.position;
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
        KeoCau();
	}
    void KeoCau()
    {
        if(GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.KeoCau)
        {
            //anim.SetBool("DropBomb", false);
            if((LuoiCauScript.instance.speed < 1.5f) && !CGameManager.instance.power)
            {
                anim.SetBool("PullHeavy", true);
            }
            else
            {
                anim.SetBool("Pull", true);
            }
            
        }
        else if(GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.Nghi)
        {
            anim.SetBool("Pull", false);
            anim.SetBool("PullHeavy", false);
        }
    }

    public void DropBomb()
    {
        anim.SetBool("DropBomb", true);
        StartCoroutine(StopDeopBomb());
    }
    IEnumerator StopDeopBomb ()
    {
        yield return new WaitForSeconds(0.7f);
        anim.SetBool("DropBomb", false);
    }

    public void Happy()
    {
        anim.SetBool("Happy", true);
        StartCoroutine(StopHappy());
        halo = Instantiate(Resources.Load("Halo"), position, Quaternion.identity) as GameObject;
    }

    IEnumerator StopHappy()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Happy", false);
        Destroy(halo);
    }

    public void Angry()
    {
        anim.SetBool("Angry", true);
        StartCoroutine(StopAngry());
    }
    
    IEnumerator StopAngry()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Angry", false);
    }
}
