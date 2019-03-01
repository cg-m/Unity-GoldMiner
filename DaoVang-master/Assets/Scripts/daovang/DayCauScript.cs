using UnityEngine;
using System.Collections;

public class DayCauScript : MonoBehaviour {
    public static DayCauScript instance; 
	public Transform luoiCau;
	public Vector3 angles;
	private float speed = 3;
	public float angleMax = 70;
	public TypeAction typeAction = TypeAction.Nghi;
	private Vector3 initAngles;
    public float rotationDay;

    // Use this for initialization
    void Start() {
        MakeInstance();
        luoiCau = GameObject.Find("luoiCau").transform;
        initAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }
	void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<LineRenderer>().SetPosition(0, transform.position);
		gameObject.GetComponent<LineRenderer>().SetPosition(1, luoiCau.position);
	}

	void FixedUpdate() {
		if(speed > 0 && typeAction == TypeAction.Nghi)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * speed) * angleMax);
        }
        rotationDay = transform.rotation.z;	
	}

	//void pendulum(float x, float y, float z) {
	// 	transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * speed) * angleMax);
	//}
}
