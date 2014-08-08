using UnityEngine;
using System.Collections;

public class SetSunPos : MonoBehaviour
{

		public GameObject SunLight;					//Control sun light direction.
		public Camera MainCamera;
		private Vector3 StructureCenterPos;
		private bool MouseDownCheck = false;
	float RevolutionRadius = 100.0f;

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
								GameObject ImgTarget = GameObject.Find ((ContentManager.getInstance ().imageTargetName));
								this.transform.parent = ImgTarget.transform.GetChild (0);
								StructureCenterPos = ImgTarget.transform.GetChild (0).position;
						} else if (Input.GetMouseButtonUp (0)) {
								MouseDownCheck = false;
						}
						
						//Follow finger
						if (MouseDownCheck == true) {
								Vector3 TouchPos = CalculatePlanePos (Input.mousePosition);
								this.transform.position = TouchPos;

								//Change Light Direction
				RotateSunLight ();
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

		private void RotateSunLight(){
			Vector3 TargetDir = StructureCenterPos - this.transform.position;
			// local z-axis <= TargetDir
		SunLight.transform.forward = TargetDir.normalized;
	}
}
