using UnityEngine;
using System.Collections;

public class FurnitureMaker : MonoBehaviour
{

		public GameObject FurnitureMovingPad;
		private string selected_furniture;

		// Use this for initialization
		int count = 0;

		public void Start ()
		{
			selected_furniture = null;
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (ContentManager.getInstance().Mode == ContentManager.MODE.FURNITURE_MODE &&
				ContentManager.getInstance ().Flag == 0) {
				if (Input.GetButtonDown ("Fire1")) {
					GameObject targets = GameObject.Find ("Targets"); 
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					RaycastHit hit = new RaycastHit ();
					if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
						if (hit.transform.name.Contains ("furniture")) {
							FurnitureMovingPad.SetActive (true);
							FurnitureMovingPad.GetComponent<FurnitureController> ().selected_furniture = hit.transform.name;
							selected_furniture = hit.transform.name;
							GameObject.Find (selected_furniture).GetComponent<FurnitureCollider>().isMoving = true;
							setFurnitureLayer(GameObject.Find (selected_furniture),"FurnitureSelectedLayer");
							ContentManager.getInstance ().Flag = 1;
						}
					}
				}
			}
			if((ContentManager.getInstance().Flag == 0 || 
			   ContentManager.getInstance().Mode != ContentManager.MODE.FURNITURE_MODE) && 
			   selected_furniture != null){
				GameObject.Find (selected_furniture).GetComponent<FurnitureCollider>().isMoving = false;
				setFurnitureLayer(GameObject.Find (selected_furniture),"Default");
				selected_furniture = null;
			}
		}
	void setFurnitureLayer(GameObject n_furniture,string layer){
		foreach(Transform child in n_furniture.transform){
			child.gameObject.layer = LayerMask.NameToLayer(layer);
		}
	}
}
