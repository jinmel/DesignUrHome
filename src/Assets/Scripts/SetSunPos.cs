using UnityEngine;
using System.Collections;

public class SetSunPos : MonoBehaviour
{

		public GameObject SunLight;					//Control sun light direction.
		public Camera MainCamera;
		private Vector3 StructureCenterPos;			//Tracking Target Center
		private Vector3 CenterToTouch;
		private bool MouseDownCheck = false;
		private float RevolutionRadius = 500.0f;	//공전 반지름
		private float RevolutionVelocity = 1.0f;		//각 속도
		private float PresentAngle;					//터치 때는 시점의 각도
		private float AlphaAngle;
		private GameObject ImgTarget;

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
								ImgTarget = GameObject.Find ((ContentManager.getInstance ().imageTargetName));
								this.transform.parent = ImgTarget.transform.GetChild (0);
								StructureCenterPos = ImgTarget.transform.GetChild (0).position;
						} else if (Input.GetMouseButtonUp (0)) {
								MouseDownCheck = false;
						}
						
						//Follow finger
						if (MouseDownCheck == true) {
								Vector3 TouchPos = CalculatePlanePos (Input.mousePosition);
								CenterToTouch = TouchPos - ImgTarget.transform.position;
								if (CenterToTouch.magnitude / RevolutionRadius > 1.0f)
										PresentAngle = 0.0f;
								else
										PresentAngle = Mathf.Acos (CenterToTouch.magnitude / RevolutionRadius);
								PresentAngle = 180 * PresentAngle / Mathf.PI;

								AlphaAngle = Vector3.Angle (CenterToTouch, Vector3.left);

								this.transform.position = CalculateSunPos (PresentAngle, CenterToTouch);
								//Change Light Direction
								RotateSunLight ();
						} else {
								//Auto movement
								//this.transform.position = CalculateSunPos ();
								RotateSunLight ();
						}

				}
		}

		private Vector3 CalculateSunPos (float angle, Vector3 Direction)
		{
				Vector3 t_SunPos = new Vector3 ();
				t_SunPos.y = RevolutionRadius * Mathf.Sin (angle);

				float temp = RevolutionRadius * Mathf.Cos (angle);
				t_SunPos.x = temp * Mathf.Cos (AlphaAngle);
				t_SunPos.x = temp * Mathf.Sin (AlphaAngle);

				return t_SunPos;
		}

		private Vector3 CalculatePlanePos (Vector3 MousePos)
		{
				Ray MouseRay = MainCamera.ScreenPointToRay (MousePos);
				float t = -MouseRay.origin.y / MouseRay.direction.y;
				return MouseRay.origin + t * MouseRay.direction;
		}

		private void RotateSunLight ()
		{
				Vector3 TargetDir = StructureCenterPos - this.transform.position;
				// local z-axis <= TargetDir
				SunLight.transform.forward = TargetDir.normalized;
		}
}
