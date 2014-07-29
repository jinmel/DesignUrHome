using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{

		//view mode
		//////////////////////////////////////////////
		// mode 0 : default
		// mode 1 : bird-eyes view
		// mode 2 : target following view mode
		//////////////////////////////////////////////

		public GameObject main_model;
		public Camera ARCamera;
		public GameObject GamePad;
		public Camera CAM;
		public GameObject Light;
		private GameObject Character;
		private Object tLight;
		private GameObject BackgroundCam;
		public GameObject light;
		private bool model_render_check;
		public GameObject apartment1;
		public GameObject structure2;
		private int mode_checker;
		private int counter;
	
		// Use this for initialization
		void Start ()
		{
				mode_checker = 0;
				BackgroundCam = GameObject.Find ("BackgroundCamera(Clone)");
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnGUI ()
		{
				GUI.Box (new Rect (10, 10, 160, 200), "Menu");
				// Handle Light Orientation 
				if (GUI.Button (new Rect (50, 40, 80, 20), "Button 1")) {
						SceneManager.getInstance ().Mode = 1;

						counter = (counter + 1) % 3;
						switch (counter) {
						case 0:
								light.light.color = Color.white;
								break;
						case 1:
								light.light.color = Color.blue;
								break;
						case 2:
								light.light.color = Color.green;
								break;
						}
				}

				if (GUI.Button (new Rect (50, 70, 80, 20), "Button 2")) {
						SceneManager.getInstance ().Mode = 2;
				}

				if (GUI.Button (new Rect (50, 100, 80, 20), "Button 3")) {
						SceneManager.getInstance ().Mode = 3;
						mode_checker = (mode_checker + 1) % 3;

						switch (mode_checker) {
						case 0:
        //destroy all human model

        //Destroy temp Light
								DestroyObject (tLight);
								CAM.gameObject.SetActive (false);

        //Set Active Cam
								ARCamera.gameObject.SetActive (true);

								break;
						case 1:
        //Create human model & not change camera
        //Create main model & attach model controller
								GamePad.SetActive (true);
								break;
						case 2:
        //disable ARcamera & change camera view
        //when changeing view, camera pos & rotation => ARcamera pos to main model
        //AR_Camera.gameObject.SetActive(false);
								GamePad.SetActive (false);

        //Main Light Copy
								tLight = Instantiate (Light, Light.transform.position, Light.transform.rotation);
								ARCamera.gameObject.SetActive (false);

								CAM.transform.position = ARCamera.transform.position;
								CAM.transform.rotation = ARCamera.transform.rotation;
								CAM.gameObject.SetActive (true);

								break;
						}
				}


				if (GUI.Button (new Rect (50, 130, 80, 20), "Button 4")) {
						SceneManager.getInstance ().Mode = 4;

						if (model_render_check == true) {
								model_render_check = false;
								apartment1.SetActive (false);
								structure2.SetActive (false);
						} else {
								model_render_check = true;
								apartment1.SetActive (true);
								structure2.SetActive (true);
						}
				}
		}
}