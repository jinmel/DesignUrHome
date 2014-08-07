using UnityEngine;
using System.Collections;

public class FurnitureCollider : MonoBehaviour {

	// Use this for initialization
	Vector3 now_rotation;
	void Start () {
		now_rotation = gameObject.transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		
		Rigidbody rigid = this.GetComponent<Rigidbody>();
		rigid.velocity = Vector3.zero;
		rigid.angularVelocity = Vector3.zero;
		Vector3 rotation = gameObject.transform.localEulerAngles;
		float y = rotation.y;
		int a = (int)(y/10.0f);
		y = a * 10;
		gameObject.transform.localEulerAngles = new Vector3(now_rotation.x,y,now_rotation.z);
		Vector3 position = gameObject.transform.localPosition;
		gameObject.transform.localPosition = new Vector3(position.x,0,position.z);


//		if(ContentManager.getInstance().Mode == ContentManager.MODE.FURNITURE_MODE && 
//		   ContentManager.getInstance().Flag == 1){
//			// When Furniture Moving.
//			string selected_furniture_name = GameObject.Find ("FurnitureMovingPad").GetComponent<FurnitureController> ().selected_furniture;
//			if(this.transform.name == selected_furniture_name){
//				now_position = this.transform.localPosition;
//				now_rotation = this.transform.localRotation;
//			}
//		}
//		if(!isNowMoving){
//			gameObject.transform.localRotation = now_rotation;
//			gameObject.transform.localPosition = now_position;
//		}
	}
	public void moveFurniture(Vector3 direction){
		Debug.Log ("Go");
		gameObject.transform.position += direction;
	}
	public void rotateFurniture(float degree){
		Vector3 rotate = gameObject.transform.localEulerAngles;
		rotate = new Vector3(rotate.x,rotate.y+degree,rotate.z);
		gameObject.transform.localEulerAngles = rotate;
	}
//	void OnCollisionEnter(Collision other){
//		Rigidbody rigid = this.GetComponent<Rigidbody>();
//		rigid.velocity = Vector3.zero;
//		rigid.angularVelocity = Vector3.zero;
//	}
}
