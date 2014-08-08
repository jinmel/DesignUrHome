using UnityEngine;
using System.Collections;

public class SetSunPos : MonoBehaviour
{

		public GameObject SunLight;					//Control sun light direction.
		public Camera MainCamera;
		private Vector3 StructureCenterPos;
		private bool MouseDownCheck = false;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (ContentManager.getInstance ().Mode == ContentManager.MODE.LIGHT_MODE) {

						if (Input.GetMouseButtonDown (0)) {
								MouseDownCheck = true;
								StructureCenterPos = GameObject.Find ((ContentManager.getInstance ().imageTargetName)).transform.GetChild (0).position;
						} else if (Input.GetMouseButtonUp (0)) {
								MouseDownCheck = false;
						}
						
						//Follow finger
						if (MouseDownCheck == true) {
								Vector3 TouchPos = CalculatePlanePos (Input.mousePosition);
								Debug.Log (TouchPos);
								this.transform.position = TouchPos;
						} else {
								//Auto movement
						}

				}
		}

		private Vector3 CalculatePlanePos (Vector3 MousePos)
		{
				Ray MouseRay = MainCamera.ScreenPointToRay (MousePos);
				float t = -MouseRay.origin.y / MouseRay.direction.y;
				return MouseRay.origin + t * MouseRay.direction;
		}

		//private Vector3 CalculateSunPos(Vector3 PlanePos){
		//}
}
