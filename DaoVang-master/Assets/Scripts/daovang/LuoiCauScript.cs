using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LuoiCauScript : MonoBehaviour
{
    public static LuoiCauScript instance;
	public float speed;
	public float speedMin;
	public float speedBegin;
	public Vector2 velocity;
	public float maxX;
	public float minX;
	public float minY;
	public float maxY;
	public Transform target;
	Vector3 prePosition;

	public int type;

	public bool isUpSpeed;
	public float timeUpSpeed;

    public GameObject hook, halfHook;
    private Vector3 positionHalfHook, scaleHalfHook;

    public bool cameraOut;
    // Use this for initialization
    void Start () {
        MakeInstance();
		isUpSpeed = false;
		prePosition = transform.localPosition;

//		this.StartCoroutine("TimeUpSpeed");
	}
    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
	public IEnumerator TimeUpSpeed ()
	{
		while(true){
			if(isUpSpeed)
			{
				timeUpSpeed = timeUpSpeed - 1;
				if(timeUpSpeed <= 0)
					isUpSpeed = false;
			}
			yield return new WaitForSeconds (1);
		}
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Toc do keo: " + speed);
		checkKeoCauXong();
        //Debug.Log("CameraOut " + cameraOut);
//		if(CGameManager.Instance.gameState == EnumStateGame.Play) 
		{
			//checkTouchScene();

			checkMoveOutCameraView();
		}
        if (CGameManager.instance.power || CGameManager.instance.powerCurrent)
        {
            speed = 4;
        }
        
        positionHalfHook = halfHook.gameObject.transform.position;
        //Debug.Log("Toa do cua cai luoi cau: x=" + positionHalfHook.x + " y=" + positionHalfHook.y);
        scaleHalfHook = halfHook.gameObject.transform.localScale;
        if (GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.KeoCau && !cameraOut)
        {
            GamePlayScript.instance.PlaySound(5);
            //Debug.Log("Dang keo");
            hook.SetActive(false);
            halfHook.SetActive(true);
            if (positionHalfHook.x > 0)
            {
                scaleHalfHook.x = -0.2f;
            }
            else
            {
                scaleHalfHook.x = 0.2f;
            }
            halfHook.transform.localScale = scaleHalfHook;
            cameraOut = false;
        }
        else if (GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.Nghi)
        {
            hook.SetActive(true);
            halfHook.SetActive(false);
        }
    }
	void FixedUpdate() {
//		if(CGameManager.Instance.gameState == EnumStateGame.Play) 
		{
			if(GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.ThaCau)
            {
                GetComponent<Rigidbody2D>().velocity = velocity * speed;
            }
				
            else if(GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.KeoCau)
            {
                if (cameraOut)
                {
                    GetComponent<Rigidbody2D>().velocity = velocity * speed * 3;
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity = velocity * speed;
                }
            }
		}
	}

//	void OnTriggerEnter2D(Collider2D other) {
//		//		Debug.Log("enter");
//		if(other.gameObject.name.CompareTo("dau") == 0) {
//			GameObject fish = other.gameObject.transform.parent.gameObject;
//			fish.GetComponent<CFishScript>().fishAction = EnumFishAction.CanCau;
//			if(!isUpSpeed) {
//				if(speed > fish.GetComponent<CFishScript>().reduceSpeed) {
//					speed = speed - fish.GetComponent<CFishScript>().reduceSpeed;
//					if(speed < speedMin) 
//						speed = speedMin;
//				}
//			}
//
//			if(GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.ThaCau) {
//				GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction = TypeAction.KeoCau;
//				velocity = -velocity;
//			}
//		}
//
//	}
	
	void OnTriggerExit2D(Collider2D other) {
//		Debug.Log("exit");
//		if(other.gameObject.name == "luoiCau") {
//			isBorder = false;
//		}
	}

	bool checkPositionOutBound() {
		return  gameObject.GetComponent<Renderer>().isVisible ;
	}

	public void checkTouchScene() { 	
		if(GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.Nghi)
		{
			GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction = TypeAction.ThaCau;
			velocity = new Vector2(transform.position.x - target.position.x, 
			                       transform.position.y - target.position.y);
			velocity.Normalize();
			speed = speedBegin;
		}
	}
	//kiem tra khi luoi cau ra ngoai tam nhin cua camera
	void checkMoveOutCameraView() {
		if(GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.ThaCau) {
			if(!checkPositionOutBound()) {
                cameraOut = true;
				GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction = TypeAction.KeoCau;
				velocity = -velocity;
			}
		}
	}

	//kiem tra khi luoi ca keo len mat nuoc
	void checkKeoCauXong() {
		if(transform.localPosition.y > maxY && GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.KeoCau) {
			//Debug.Log("keo cau xong");
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction = TypeAction.Nghi;
			transform.localPosition = prePosition;
		}
	}

    
}
