using UnityEngine;
using System.Collections;

public class Make_furniture : MonoBehaviour
{

		public GameObject FurnitureMovingPad;


		// Use this for initialization
		int count = 0;

		public
	void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (ContentManager.getInstance ().Mode == ContentManager.FURNITURE_MODE &&
						ContentManager.getInstance ().Flag == 0) {
						if (Input.GetButtonDown ("Fire1")) {
								GameObject targets = GameObject.Find ("Targets"); 
								Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
								RaycastHit hit = new RaycastHit ();
				
								//			if(Physics.Raycast(ray, out hit)) {
								if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
										//				Debug.Log(hit.transform.ToString()+" but \n"+this.transform.ToString());

										for (int i=0; i<targets.transform.childCount; i++) {
												if (hit.transform == targets.transform.GetChild (i)) {
														Vector3 crush = hit.point;
														crush -= targets.transform.GetChild (i).position;
							
														Vector3 rot = targets.transform.GetChild (i).localEulerAngles;
														rot.y *= -1;
														crush = Quaternion.Euler (rot) * crush;
														Debug.Log (targets.transform.GetChild (i).name + " is Clicked");

														MakeObject (crush, targets.transform.GetChild (i));
												}
										}
										if (hit.transform.name.Contains ("furniture")) {
//						this.GetComponent<Move_furniture>().name_SelectedFurniture = hit.transform.name;
												//GameObject.Find ("FurnitureMovingPad").SetActive(true);
												FurnitureMovingPad.SetActive (true);
												FurnitureMovingPad.GetComponent<Furniture_Moving_Controller> ().selected_furniture = hit.transform.name;
												ContentManager.getInstance ().Flag = 1;
										}
								}
						}
				}
		}

		void MakeObject (Vector3 position, Transform parent)
		{
				Debug.Log ("Make Object!");
				GameObject Rock; 
				position /= parent.transform.localScale.x;
				position.y = 0.02f;
				Rock = (GameObject)Instantiate (Resources.Load ("teapot"));
				Rock.name = "furniture_" + count.ToString ();
				Rock.transform.parent = parent;
				Rock.transform.localPosition = position;
				Rock.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
				Rigidbody rigid = Rock.AddComponent<Rigidbody> ();
				rigid.useGravity = false;
				count ++;
		}
}
