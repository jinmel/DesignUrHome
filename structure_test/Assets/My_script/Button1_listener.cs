using UnityEngine;
using System.Collections;

public class Button1_listener : MonoBehaviour {

	public GameObject _light;
	private int counter;
	// Use this for initialization
	void Start () {
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		counter = (counter + 1) % 3;

		switch (counter) {
		case 0:
			_light.light.color = Color.white;
			break;
		case 1:
			_light.light.color = Color.blue;
			break;
		case 2:
			_light.light.color = Color.green;
			break;
				}
	}
	
}
