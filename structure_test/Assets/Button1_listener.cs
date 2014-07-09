using UnityEngine;
using System.Collections;

public class Button1_listener : MonoBehaviour {

	public GameObject Target_light;
	// Use this for initialization
	void Start () {
		Debug.Log ("Start Complete");
		Target_light = GameObject.Find ("Camera Light");
	}
	
	// Update is called once per frame
	void Update () {

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				//터치 ray tracing
				Vector3 ray = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				
				Debug.Log ("Click!!");
				Target_light.light.color = Color.blue;
			}
		} else {
			if (Input.GetMouseButtonDown (0)) {
				//터치 ray tracing
				Vector3 ray = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				
				Debug.Log ("Click!!");
				Target_light.light.color = Color.blue;
			}
		}
	}
}
