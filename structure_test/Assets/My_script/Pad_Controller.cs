using UnityEngine;
using System.Collections;

public class Pad_Controller : MonoBehaviour
{
		public GUIStyle textStyle;
		public GameObject Move_key;
		public GameObject Move_board;
		public Camera main_cam;
		private float _time;
		private bool touch_check;

		//Move Key pad
		private Vector3 Start_point;
		private float Button_Dist;

		//Local position
		private Vector3 Local_start;
		private Vector3 Local_target;

		// Use this for initialization
		void Start ()
		{
				_time = Time.timeSinceLevelLoad;
				touch_check = false;
				Button_Dist = 0.7f;

				Color _t_gray = new Color (0.2f, 0.2f, 0.2f, 1.0f);
				Move_board.renderer.material.SetColor ("_Color", _t_gray);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
				if (Time.timeSinceLevelLoad - _time > 0.5) {
						if (Input.GetMouseButtonDown (0)) {
								touch_check = true;
								Start_point = Calculate_button_pos (10);
								Move_key.transform.position = Start_point;
								Local_start = Move_key.transform.localPosition;
								Move_board.transform.position = Calculate_button_pos (15);
								Move_key.SetActive (true);
								Move_board.SetActive (true);
						} else if (Input.GetMouseButtonUp (0)) {
								Move_key.SetActive (false);
								Move_board.SetActive (false);
								touch_check = false;
						}
						
						if (touch_check == true) {
								//버튼 따라다니게 하기
								Vector3 target_pos = Calculate_button_pos (10);
								Move_key.transform.position = target_pos;
								Local_target = Move_key.transform.localPosition;
								float dist = Vector3.Distance (Local_target, Local_start);
								
								//일정 범위 이상 안벗어 나게...
								if (dist > Button_Dist) {
										Vector3 Key_dir = Vector3.Normalize (Local_target - Local_start);
										Key_dir = Key_dir * Button_Dist;
										Vector3 dst_pos = Local_start + Key_dir;
										Move_key.transform.localPosition = dst_pos;
								}
						}
				}
		}

		Vector3 Calculate_button_pos (int dist)
		{
				Vector3 screen_pos = Input.mousePosition;
				Ray touch_ray = main_cam.ScreenPointToRay (screen_pos);
				Vector3 Origin_ray = touch_ray.origin;
				Vector3 Cam_ray_vec = Origin_ray - main_cam.gameObject.transform.position;
				float cam_ray_dist = Mathf.Sqrt (Cam_ray_vec.magnitude);				//distance - cam, ray_origin
				float sub_dist = dist - cam_ray_dist;
				Vector3 dst_pos = touch_ray.GetPoint (sub_dist);

				return dst_pos;
		}

		void OnGUI ()
		{
				Vector3 Cam_pos = main_cam.transform.position;
				GUI.Label (new Rect (300, 300, 60, 60), "x:" + Cam_pos.x + " y:" + Cam_pos.y + " z:" + Cam_pos.z, this.textStyle);
		}
}

