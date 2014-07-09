using UnityEngine;
using System.Collections;

public class Button3_listener : MonoBehaviour {

	public GameObject Target_light;
	// Use this for initialization
	void Start () {
		Debug.Log ("Start Complete");
		Target_light = GameObject.Find ("Camera Light");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetTouch (0).phase == TouchPhase.Began) {
			Debug.Log ("Click!!");
			Target_light.light.color = Color.white;
		}
	}
}
