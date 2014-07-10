using UnityEngine;
using System.Collections;

public class Button3_listener : MonoBehaviour {

	public GameObject _light;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown() {
		_light.light.color = Color.white;
	}
}
