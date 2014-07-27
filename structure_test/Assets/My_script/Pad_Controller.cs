using UnityEngine;
using System.Collections;

public class Pad_Controller : MonoBehaviour
{

		public GameObject Move_key;
		public GameObject Move_board;
		public Camera main_cam;
		private float _time;
		private bool touch_check;

		//Move Key pad
		private Vector3 Start_point;
		private float Button_Dist;

		// Use this for initialization
		void Start ()
		{
				_time = Time.timeSinceLevelLoad;
				touch_check = false;
				Button_Dist = 0.7f;

		Move_board.gameObject.renderer.material.color = new Color (0.3f, 0.3f, 0.3f, 0.9f);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
				if (Time.timeSinceLevelLoad - _time > 0.5) {
						if (Input.GetMouseButtonDown (0)) {
								Move_key.SetActive (true);
				Move_board.SetActive(true);
								touch_check = true;
								Start_point = Calculate_button_pos ();
				Move_board.transform.position = Start_point;
						} else if (Input.GetMouseButtonUp (0)) {
								Move_key.SetActive (false);
				Move_board.SetActive(false);
								touch_check = false;
						}
						
						if (touch_check == true) {
								//버튼 따라다니게 하기
								Vector3 target_pos = Calculate_button_pos ();
								float dist = Vector3.Distance (target_pos, Start_point);
								
								//일정 범위 이상 안벗어 나게...
								if (dist < Button_Dist) {
										Move_key.transform.position = target_pos;
								} else {
										Vector3 Key_dir = Vector3.Normalize (target_pos - Start_point);
										Key_dir = Key_dir * Button_Dist;
										Vector3 dst_pos = Start_point + Key_dir;
										Move_key.transform.position = dst_pos;
								}
						}
				}
		}

		Vector3 Calculate_button_pos ()
		{
				Vector3 screen_pos = Input.mousePosition;
				Ray touch_ray = main_cam.ScreenPointToRay (screen_pos);
				Vector3 Origin_ray = touch_ray.origin;
				Vector3 Cam_ray_vec = Origin_ray - main_cam.gameObject.transform.position;
				float cam_ray_dist = Mathf.Sqrt (Cam_ray_vec.magnitude);				//distance - cam, ray_origin
				float sub_dist = 10 - cam_ray_dist;
				Vector3 dst_pos = touch_ray.GetPoint (sub_dist);

				return dst_pos;
		}
}

