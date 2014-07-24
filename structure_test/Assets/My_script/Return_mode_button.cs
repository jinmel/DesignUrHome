using UnityEngine;
using System.Collections;

public class Return_mode_button : MonoBehaviour {

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnMouseDown() {
		Application.LoadLevel ("test_scene");
	}
}
