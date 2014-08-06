using UnityEngine;
using System.Collections;

public class SetCamPos : MonoBehaviour
{

		private static bool Start_set = false;
		private float t_time;
		private const float Moving_time = 2.0f;
		private Vector3 src_pos;
		private Vector3 src_rot;
		private Vector3 dst_pos = new Vector3 (0.0f, 2.0f, 0.0f);		//local coordinate
		private Vector3 dst_rot = new Vector3 (10.0f, 0.0f, 0.0f);
		private Vector3 dir_vec;
		private Vector3 dir_rot;
		// Use this for initialization
		void Start ()
		{
				t_time = 0.0f;
		}
	
		// Update is called once per frame
		void Update ()
		{
				float t = Time.timeSinceLevelLoad - t_time;

				if (Start_set == true) {
						if (t_time == 0.0f) {
								t_time = Time.timeSinceLevelLoad;

								src_pos = this.transform.localPosition;
								src_rot = this.transform.localRotation.eulerAngles;
								
								dir_vec = (dst_pos - this.transform.localPosition) / Moving_time;
								dir_rot = (dst_rot - this.transform.localRotation.eulerAngles) / Moving_time;
						} else if (t > 0.5) {
								t -= 0.5f;
								
								if (t > Moving_time) {
										t_time = 0.0f;
										Start_set = false;

										this.transform.localPosition = dst_pos;
										this.transform.localRotation = Quaternion.Euler (dst_rot);
								} else {
										this.transform.localRotation = Quaternion.Euler (src_rot + t * dir_rot);
										this.transform.localPosition = src_pos + t * dir_vec;
								}
						}
				}
		}

		public void Cam_posSet ()
		{
				Start_set = true;
		}
}
