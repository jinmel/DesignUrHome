using UnityEngine;
using System.Collections;

public class Button2_listener : MonoBehaviour {

	private GameObject _block;
	private GameObject _cam;

	// Use this for initialization
	void Start () {
		_block = GameObject.Find ("test_block");
		_cam = GameObject.Find ("ARCamera");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		Instantiate (_block, _cam.transform.position, transform.rotation); 
	}
}
