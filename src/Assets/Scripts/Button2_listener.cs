using UnityEngine;
using System.Collections;

public class Button2_listener : MonoBehaviour {

	public GameObject DropIt;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		SceneManager.getInstance().Mode = 2;
	}
}
