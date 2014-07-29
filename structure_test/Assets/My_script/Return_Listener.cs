using UnityEngine;
using System.Collections;

public class Return_Listener : MonoBehaviour {

	public Button3_listener _Button3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// return default mode
	void OnMouseDown() {
		_Button3.OnMouseDown ();
	}
}
