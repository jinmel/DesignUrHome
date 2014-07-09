using UnityEngine;
using System.Collections;

public class Button1_listener : MonoBehaviour {

	private GameObject Target_light;
	// Use this for initialization
	void Start () {
		Target_light = GameObject.Find ("Camera Light");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		Target_light.light.color = Color.blue;
	}
	
}
