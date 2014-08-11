using UnityEngine;
using System.Collections;

public class SetSunPos : MonoBehaviour
{

		public GameObject SunLight;					//Control sun light direction.
		public Camera MainCamera;
		private Vector3 StructureCenterPos;			//Tracking Target Center
		private Vector3 CenterToTouch;
		private bool MouseDownCheck = false;
		private float RevolutionRadius = -1.0f;	//공전 반지름
		private float RevolutionVelocity = 1.0f;		//각 속도
		private float PresentAngle;					//현재 각도
		private float AlphaAngle;
		private GameObject ImgTarget;
		public GUIStyle textStyle;

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

								PresentAngle = 0.0f;
						} else if (Input.GetMouseButtonUp (0)) {
								MouseDownCheck = false;
						}
						
						//Follow finger
						if (MouseDownCheck == true) {
								Vector3 TouchPos = CalculatePlanePos (Input.mousePosition);
								CenterToTouch = TouchPos - ImgTarget.transform.position;
								RevolutionRadius = CenterToTouch.magnitude;

								AlphaAngle = Vector3.Angle (Vector3.right, CenterToTouch);
								if (Vector3.Cross (CenterToTouch, Vector3.right).y < 0)
										AlphaAngle = 360.0f - AlphaAngle;
								//Debug.Log (RevolutionRadius);
								AlphaAngle = Mathf.PI * AlphaAngle / 180.0f;

								this.transform.position = TouchPos;
								//Change Light Direction
								RotateSunLight ();
						} else {
								//Auto movement\
								this.transform.position = CalculateSunPos ();
								RotateSunLight ();
						}

				}
		}

		private Vector3 CalculateSunPos ()
		{
				Vector3 SunPos = new Vector3 ();
				PresentAngle += RevolutionVelocity % 360;

				float R_Angle = Mathf.PI * PresentAngle / 180.0f;

				SunPos.y = RevolutionRadius * Mathf.Sin (R_Angle);
				SunPos.x = RevolutionRadius * Mathf.Cos (R_Angle) * Mathf.Cos (AlphaAngle);
				SunPos.z = RevolutionRadius * Mathf.Cos (R_Angle) * Mathf.Sin (AlphaAngle);

				return SunPos;
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

		void OnGUI ()
		{
				GUI.Label (new Rect (800, 5, 30, 30), ContentManager.getInstance ().imageTargetName, this.textStyle);
		}
}
