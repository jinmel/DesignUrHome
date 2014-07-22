using UnityEngine;
using System.Collections;

public class Button3_listener : MonoBehaviour {
	//view mode
	//////////////////////////////////////////////
	// mode 0 : default
	// mode 1 : bird-eyes view
	// mode 2 : target following view mode
	//////////////////////////////////////////////

	public GameObject main_model;
	private int mode_checker;
	private GameObject _Character;

	// Use this for initialization
	void Start () {
		mode_checker = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnMouseDown() {
		mode_checker = (mode_checker + 1) % 3;

		switch (mode_checker) {
		case 0:
			//destroy all human model
			Destroy(_Character);

			break;
		case 1:
			//Create human model & not change camera
			//Create main model & attach model controller

			//_Character = Instantiate(main_model, , main_model.transform.rotation);

			break;
		case 2:
			//disable ARcamera & change camera view
			//when changeing view, camera pos & rotation => ARcamera pos to main model

			break;
				}
	}
}
