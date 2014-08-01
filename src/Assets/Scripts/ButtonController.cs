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
		private GameObject tStructure;				// Button3 - mode3. 1인칭 시점에서 구조

		// ... 
		public GUIStyle container_style;
		public GUIStyle button1_style;
		public GUIStyle button2_style;
		public GUIStyle button3_style;
		public GUIStyle button4_style;
	public GUIStyle tDebug;
	
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
				if (GUI.Button (new Rect (50, 40, 80, 80), "Button 1", button1_style)) {
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
				if (GUI.Button (new Rect (50, 130, 80, 80), "Button 2", button2_style)) {
						SceneManager.getInstance ().Mode = 2;
				}

				if (GUI.Button (new Rect (50, 220, 80, 80), "Button 3", button3_style)) {
						//view mode
						//////////////////////////////////////////////
						// mode 0 : default
						// mode 1 : bird-eyes view
						// mode 2 : target following view mode
						//////////////////////////////////////////////

						SceneManager.getInstance ().Mode = 3;
						mode_checker = (mode_checker + 1) % 3;

						Vector3 tttt = new Vector3(-50.88073f, 0.0f, 96.75817f);

						switch (mode_checker) {
						case 0:
       
								//destroy all human model
								//TO-DO

    						    //Destroy temp Light
								DestroyObject (tLight);
								CAM.gameObject.SetActive (false);
								
								//Destroy temp Structure
								DestroyObject(tStructure);

						        //Set Active Cam
								ARCamera.gameObject.SetActive (true);
								ImageTarget.SetActive(true);

								//killllllll Gamepad
								GamePad.SetActive (false);

								break;
						case 1:
								//Tracking nothing - action nothing
								if(SceneManager.getInstance().ImageTarget_name == null){
									mode_checker = 0;
									break;
								}

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

								CAM.transform.position = ARCamera.transform.position;
								CAM.transform.rotation = ARCamera.transform.rotation;
								CAM.gameObject.SetActive (true);
								
								//Create Structure
								GameObject tImageTarget = GameObject.Find(SceneManager.getInstance().ImageTarget_name);
								GameObject target_structure = tImageTarget.transform.GetChild(0).gameObject;
								tStructure = (GameObject)Instantiate (target_structure, target_structure.transform.position , target_structure.transform.rotation);
								
								//Set structure scale
								Vector3 t_scale = new Vector3(target_structure.transform.localScale.x * tImageTarget.transform.localScale.x
				                				              ,	target_structure.transform.localScale.y * tImageTarget.transform.localScale.y
				                              					, target_structure.transform.localScale.z * tImageTarget.transform.localScale.z);
								tStructure.transform.localScale = t_scale;
								//////////////////////////////////////////////////////
								//tStructure.transform.position =	tttt;
								//CAM.transform.position = new Vector3(0.0f,600.0f,0.0f);
								//SCAM.transform.eulerAngles = new Vector3(50.88073f, -600.0f, -96.75817f);
								//tStructure.transform.localScale = new Vector3(0.2179955f, 0.2179955f, 0.2179955f);
								//////////////////////////////////////////////////////

								//ARCamera shut down
								ARCamera.gameObject.SetActive (false);
								ImageTarget.SetActive(false);

								break;
						}
				}


				if (GUI.Button (new Rect (50, 310, 80, 80), "Button 4", button4_style)) {
						SceneManager.getInstance ().Mode = 4;

						if (model_render_check == true) {
								model_render_check = false;
								ImageTarget.SetActive (false);
						} else {
								model_render_check = true;
								ImageTarget.SetActive (true);
						}
				}
		GUI.Label (new Rect (200, 5, 30, 30), SceneManager.getInstance().ImageTarget_name, tDebug);
		}
}