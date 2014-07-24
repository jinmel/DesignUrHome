using UnityEngine;
using System.Collections;

public class Pad_Controller : MonoBehaviour
{

		public GameObject Move_key;
		public Camera main_cam;
		private float _time;
		private bool touch_check;

		// Use this for initialization
		void Start ()
		{
				_time = Time.timeSinceLevelLoad;
				touch_check = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
				if (Time.timeSinceLevelLoad - _time > 0.5) {
						if (Input.GetMouseButtonDown (0)) {
								Move_key.SetActive (true);
								//Vector3 pos = Input.mousePosition;
								//Debug.Log ("MousePos : " + pos.x + " " + pos.y + " " + pos.z);
								Debug.Log ("touch_check = true");
								touch_check = true;
						} else if (Input.GetMouseButtonUp (0)) {
								Move_key.SetActive (false);
								touch_check = false;
						}
						
						if (touch_check == true) {
								Vector3 screen_pos = Input.mousePosition;
								Ray touch_ray = main_cam.ScreenPointToRay (screen_pos);
						}
				}
		}
}
