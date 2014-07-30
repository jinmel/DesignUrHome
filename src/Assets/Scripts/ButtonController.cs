using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
	
		//MainScene Object
		public Camera ARCamera;						// vuforia camera
		public GameObject GamePad;					// GamePad. in button3
		public Camera CAM;							// external Camera (using button3)
		public GameObject Light;					// main Light in Camera
		public GameObject ImageTarget;				// ImageTarget in vuforia

		//...
		private GameObject Character;				// Charector model
		private Object tLight;						// Button3 - mode3. 1인칭 시점에서 사용
		private bool model_render_check;			// Button4 - 
		private int mode_checker;
		private int counter;

		// ... 
		public GUIStyle container_style;
		public GUIStyle button1_style;
		public GUIStyle button2_style;
		public GUIStyle button3_style;
		public GUIStyle button4_style;
	
		// Use this for initialization
		void Start ()
		{
				mode_checker = 0;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnGUI ()
		{
				GUI.Box (new Rect (10, 10, 160, 200), "Menu", container_style);
				// Handle Light Orientation 
				if (GUI.Button (new Rect (50, 40, 80, 20), "Button 1", button1_style)) {
						SceneManager.getInstance ().Mode = 1;

						counter = (counter + 1) % 3;
						switch (counter) {
						case 0:
								Light.light.color = Color.white;
								break;
						case 1:
								Light.light.color = Color.blue;
								break;
						case 2:
								Light.light.color = Color.green;
								break;
						}
				}

				if (GUI.Button (new Rect (50, 70, 80, 20), "Button 2", button2_style)) {
						SceneManager.getInstance ().Mode = 2;
				}

				if (GUI.Button (new Rect (50, 100, 80, 20), "Button 3", button3_style)) {
						//view mode
						//////////////////////////////////////////////
						// mode 0 : default
						// mode 1 : bird-eyes view
						// mode 2 : target following view mode
						//////////////////////////////////////////////

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


				if (GUI.Button (new Rect (50, 130, 80, 20), "Button 4", button4_style)) {
						SceneManager.getInstance ().Mode = 4;

						if (model_render_check == true) {
								model_render_check = false;
								ImageTarget.SetActive (false);
						} else {
								model_render_check = true;
								ImageTarget.SetActive (true);
						}
				}
		}
}