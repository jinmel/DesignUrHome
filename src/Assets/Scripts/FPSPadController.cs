using UnityEngine;
using System.Collections;

public class FPSPadController : MonoBehaviour {
	
	public GUIStyle textStyle;
	public GameObject Move_key;
	public GameObject Move_board;
	public GameObject Character;
	public Camera main_cam;
	private bool touch_check;
	
	//Move Key pad
	private Vector3 Start_point;		//Initial Start point
	private float Button_Dist;
	private Vector3 Screen_Start;
	
	//Local position
	private Vector3 Local_start;
	private Vector3 Local_target;
	
	//UI Domain
	private Rect UI_size;
	
	//rotation parameter
	float rot_threshold = 45.0f;
	float rot_const = 1.0f;

    private ContentManager contentManager;

	private float velocity = 1.5f;

	// Use this for initialization
	void Start ()
	{
		touch_check = false;
		Button_Dist = 0.7f;
        contentManager = ContentManager.getInstance();

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (contentManager.Mode == ContentManager.MODE.CHARACTER_MODE) {
			if (Input.GetMouseButtonDown (0)) {
				if (!InRectCheck (contentManager.UI_Domain)) {
					touch_check = true;
					Start_point = CalculateButtonPos (10);
					Screen_Start = Input.mousePosition;
					Move_key.transform.position = Start_point;
					Local_start = Move_key.transform.localPosition;
					Move_board.transform.position = CalculateButtonPos (15);
					Move_key.SetActive (true);
					Move_board.SetActive (true);
				}
			} else if (Input.GetMouseButtonUp (0)) {
				Move_key.SetActive (false);
				Move_board.SetActive (false);
				touch_check = false;
			}
			
			if (touch_check == true) {
				//버튼 따라다니게 하기
				Vector3 target_pos = CalculateButtonPos (10);
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
				
				//Rotate Charecter
				Vector3 Char_dir = GetModelDirection ();
				Vector3 Screen_vec = Input.mousePosition - Screen_Start;
				float t_angle = Vector3.Angle (Vector3.up, Screen_vec);
				
				//Move Charecter
				if(t_angle <= rot_threshold || (180 - rot_threshold) <= t_angle){
					Character.transform.position += velocity * Char_dir;
				}
				else{
					Vector3 t_cross_result = Vector3.Cross (Vector3.up, Screen_vec);
					Vector3 Present_angle = Character.transform.eulerAngles;
					if (t_cross_result.z < 0){
						Present_angle.y += rot_const;
					}else{
						Present_angle.y -= rot_const;
					}
					Character.transform.rotation = Quaternion.Euler(Present_angle);
				}
			}
			else{
				//Character Idle animation
				RuntimeAnimatorController idle_anim = (RuntimeAnimatorController)Resources.Load ("Idle",typeof(RuntimeAnimatorController));
				Character.GetComponent<Animator>().runtimeAnimatorController = idle_anim;
			}
		}
	}
	
	Vector3 CalculateButtonPos (int dist)
	{
		Vector3 screen_pos = Input.mousePosition;
		Ray touch_ray = main_cam.ScreenPointToRay (screen_pos);
		Vector3 Origin_ray = touch_ray.origin;
		Vector3 Cam_ray_vec = Origin_ray - main_cam.gameObject.transform.position;
		float cam_ray_dist = Cam_ray_vec.magnitude;				//distance - cam, ray_origin
		float sub_dist = dist - cam_ray_dist;
		Vector3 dst_pos = touch_ray.GetPoint (sub_dist);
		
		return dst_pos;
	}
	
	//Get Model move direction
	//return normalized vector.
	private Vector3 GetModelDirection ()
	{
		Vector3 Start_floor_pos = GetFloorPos (Move_board.transform.position);
		Vector3 GameKey_floor_pos = GetFloorPos (Move_key.transform.position);
		
		Vector3 Dir_vec = GameKey_floor_pos - Start_floor_pos;
		return Dir_vec.normalized;
	}
	
	//Gamepad_pos + Ray_vec*t = floor_pos.
	//This fuction calculate t.
	private float GetScreentoFloorConst (Vector3 p)
	{
		Vector3 t_Ray = p - main_cam.transform.position;
		
		return p.y / (-t_Ray.y);
	}
	
	private Vector3 GetFloorPos (Vector3 p)
	{
		Vector3 t_Ray = p - main_cam.transform.position;
		float t_const = GetScreentoFloorConst (p);	
		
		return p + t_const * t_Ray;
	}
	
	private bool InRectCheck (Rect UI)
	{
		Vector3 Mouse_pos = Input.mousePosition;
		Mouse_pos.y = Screen.height - Mouse_pos.y;
		if (UI.x <= Mouse_pos.x && Mouse_pos.x <= UI.x + UI.width) {
			if (UI.y <= Mouse_pos.y && Mouse_pos.y <= UI.y + UI.height)
				return true;
		}
		return false;
	}
	
}
