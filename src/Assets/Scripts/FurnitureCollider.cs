using UnityEngine;
using System.Collections;

public class FurnitureCollider : MonoBehaviour {

	// Use this for initialization
	Quaternion now_rotation;
	Vector3 now_position;
	void Start () {
		now_rotation = this.transform.localRotation;
		now_position = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if(ContentManager.getInstance().Mode == ContentManager.MODE.FURNITURE_MODE && 
		   ContentManager.getInstance().Flag == 1){
			// When Furniture Moving.
			string selected_furniture_name = GameObject.Find ("FurnitureMovingPad").GetComponent<FurnitureController> ().selected_furniture;
			if(this.transform.name == selected_furniture_name){
				now_position = this.transform.localPosition;
				now_rotation = this.transform.localRotation;
			}
		}
		this.transform.localRotation = now_rotation;
		this.transform.localPosition = now_position;
	}
}
