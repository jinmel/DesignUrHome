using UnityEngine;
using System.Collections;

public class FurnitureMaker : MonoBehaviour
{

		public GameObject FurnitureMovingPad;


		// Use this for initialization
		int count = 0;

		public void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (ContentManager.getInstance ().Mode == ContentManager.MODE.FURNITURE_MODE &&
						ContentManager.getInstance ().Flag == 0) {
						if (Input.GetButtonDown ("Fire1")) {
								GameObject targets = GameObject.Find ("Targets"); 
								Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
								RaycastHit hit = new RaycastHit ();
								if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
										for (int i=0; i<targets.transform.childCount; i++) {
												if (hit.transform == targets.transform.GetChild (i)) {
														Vector3 crush = hit.point;
														crush -= targets.transform.GetChild (i).position;
							
														Vector3 rot = targets.transform.GetChild (i).localEulerAngles;
														rot.y *= -1;
														crush = Quaternion.Euler (rot) * crush;
														Debug.Log (targets.transform.GetChild (i).name + " is Clicked");

														makeObject (crush, targets.transform.GetChild (i));
												}
										}
										if (hit.transform.name.Contains ("furniture")) {
												FurnitureMovingPad.SetActive (true);
												FurnitureMovingPad.GetComponent<FurnitureController> ().selected_furniture = hit.transform.name;
												ContentManager.getInstance ().Flag = 1;
										}
								}
						}
				}
		}

		void makeObject (Vector3 position, Transform parent)
		{
				Debug.Log ("Make Object!");
				GameObject newFurniture; 
				position /= parent.transform.localScale.x;
				position.y = 0.02f;
				newFurniture = (GameObject)Instantiate (Resources.Load ("teapot"));
				newFurniture.name = "furniture_" + count.ToString ();
				newFurniture.transform.parent = parent;
				newFurniture.transform.localPosition = position;
				newFurniture.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
				newFurniture.GetComponent<MeshCollider>().convex = true;
				Rigidbody rigid = newFurniture.AddComponent<Rigidbody> ();
				newFurniture.AddComponent<FurnitureCollider>();
				rigid.useGravity = false;
				count ++;
		}
}
