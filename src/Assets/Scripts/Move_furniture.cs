using UnityEngine;
using System.Collections;

public class Move_furniture : MonoBehaviour {
	bool is_MovingIconLoad = false;
	public string name_SelectedFurniture = null;
	GameObject selected_furniture;
	GameObject[] btn_Moving = new GameObject[4]; 
	// 0->up 1->down 2->left 3->right

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(ContentManager.getInstance().Mode == 2 && name_SelectedFurniture.Contains("furniture")){
			selected_furniture = GameObject.Find(name_SelectedFurniture);
			if(!is_MovingIconLoad){
				// Load Moving Icon 
				MovingIconLoad();
				is_MovingIconLoad = true;
			}
			else{
				if(Input.GetButtonDown("Fire1")){
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray.origin,ray.direction,out hit)){
						if(hit.transform.name.Contains("btn")){
							switch(hit.transform.name){
								case "btn_up": 
								Moves_furniture(0.0f,1.0f);
								break;
								case "btn_down":
								Moves_furniture(0.0f,-1.0f);
								break;
								case "btn_left":
								Moves_furniture(-1.0f,0.0f);
								break;
								case "btn_right":
								Moves_furniture(1.0f,0.0f);
								break;
							}
						}
					}
				}
			}
		}
		else if(is_MovingIconLoad){
			// Delete Moving Icon
			is_MovingIconLoad = false;
		}
	}
	void Moves_furniture(float x,float z){
		float dis = 0.01f;
		Vector3 move = selected_furniture.transform.localPosition;
		move.Set (move.x+x*dis,move.y,move.z+z*dis);
		selected_furniture.transform.localPosition = move;
		int i;
		for(i=0;i<4;i++){
			move = btn_Moving[i].transform.localPosition;
			move.Set (move.x+x*dis,move.y,move.z+z*dis);
			btn_Moving[i].transform.localPosition = move;
		}
	}
	void MovingIconLoad(){
		//		btn_Moving[0] = new GameObject("btn_up");
		int i;
		for(i=0;i<4;i++){
			btn_Moving[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
		}
		btn_Moving[0].transform.name = "btn_up";
		btn_Moving[1].transform.name = "btn_down";
		btn_Moving[2].transform.name = "btn_left";
		btn_Moving[3].transform.name = "btn_right";
		float[] direction_x = {0,0,-1,1};
		float[] direction_z = {1,-1,0,0};
		float distance = 0.05f;
		for(i=0;i<4;i++){
			btn_Moving[i].transform.parent = selected_furniture.transform.parent;
			btn_Moving[i].transform.localPosition = new Vector3(selected_furniture.transform.localPosition.x + direction_x[i]*distance,0.25f,
			                                                    selected_furniture.transform.localPosition.z + direction_z[i]*distance);
			Material mat = (Material)Resources.Load("Materials/Arrow",typeof(Material));
			btn_Moving[i].GetComponent<MeshRenderer>().material = mat;
			btn_Moving[i].transform.localScale = new Vector3(0.03f,0.0f,0.03f);
		}
		btn_Moving[0].transform.localEulerAngles = new Vector3(0,90,0);
		btn_Moving[1].transform.localEulerAngles = new Vector3(0,-90,0);
		btn_Moving[2].transform.localEulerAngles = new Vector3(0,0,0);
		btn_Moving[3].transform.localEulerAngles = new Vector3(0,180,0);
	}
}
