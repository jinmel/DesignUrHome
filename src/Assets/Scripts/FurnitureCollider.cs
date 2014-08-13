using UnityEngine;
using System.Collections;

public class FurnitureCollider : MonoBehaviour {

	// Use this for initialization
	public Vector3 now_rotation;
	public Vector3 now_position;
	public bool isMoving = false;
	void Start () {
		now_rotation = gameObject.transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoving){
			Rigidbody rigid = this.GetComponent<Rigidbody>();
			rigid.velocity = Vector3.zero;
			rigid.angularVelocity = Vector3.zero;
			Vector3 rotation = gameObject.transform.localEulerAngles;
			float y = rotation.y;
			int a = (int)(y/10.0f);
			y = a * 10;
			gameObject.transform.localEulerAngles = new Vector3(now_rotation.x,now_rotation.y,now_rotation.z);
			Vector3 position = gameObject.transform.localPosition;
			gameObject.transform.localPosition = new Vector3(position.x,0,position.z);
			now_position = gameObject.transform.localPosition;
		}
		else{
			gameObject.transform.localEulerAngles = now_rotation;
			gameObject.transform.localPosition = now_position;
		}
	}
	public void moveFurniture(Vector3 direction){
		gameObject.transform.position += direction;
	}
	public void rotateFurniture(float degree){
		Vector3 rotate = gameObject.transform.localEulerAngles;
		rotate = new Vector3(rotate.x,rotate.y+degree,rotate.z);
		gameObject.transform.localEulerAngles = rotate;
	}
}
