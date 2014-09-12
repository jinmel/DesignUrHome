using UnityEngine;
using System.Collections;

public class PadController : MonoBehaviour
{
    public GUIStyle textStyle;
    public GameObject Move_key;
    public GameObject Move_board;
    public GameObject Character;
    public Camera main_cam;
    private bool touch_check;
    
    //Move Key pad
    private Vector3 Start_point;        //Initial Start point
    private float Button_Dist;
    
    //Local position
    private Vector3 Local_start;
    private Vector3 Local_target;

    //UI Domain
    private Rect UI_size;
    private ContentManager contentManager;
    
    // Use this for initialization
    void Start()
    {
        touch_check = false;
        Button_Dist = 0.7f;
        contentManager = ContentManager.getInstance();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (contentManager.Mode == ContentManager.MODE.CHARACTER_MODE)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!InRectCheck(ContentManager.getInstance().UI_Domain))
                {
                    touch_check = true;
                    Start_point = Calculate_button_pos(10);
                    Move_key.transform.position = Start_point;
                    Local_start = Move_key.transform.localPosition;
                    Move_board.transform.position = Calculate_button_pos(15);
                    Move_key.SetActive(true);
                    Move_board.SetActive(true);

                    //Character Moving animation
                    RuntimeAnimatorController walk_anim = (RuntimeAnimatorController)Resources.Load("Walk", typeof(RuntimeAnimatorController));
                    Character.GetComponent<Animator>().runtimeAnimatorController = walk_anim;
                }
            } else if (Input.GetMouseButtonUp(0))
            {
                Move_key.SetActive(false);
                Move_board.SetActive(false);
                touch_check = false;
                                
                //Character Idle animation
                RuntimeAnimatorController idle_anim = (RuntimeAnimatorController)Resources.Load("Idle", typeof(RuntimeAnimatorController));
                Character.GetComponent<Animator>().runtimeAnimatorController = idle_anim;
            }
            
            if (touch_check == true)
            {
                //버튼 따라다니게 하기
                Vector3 target_pos = Calculate_button_pos(10);
                Move_key.transform.position = target_pos;
                Local_target = Move_key.transform.localPosition;
                float dist = Vector3.Distance(Local_target, Local_start);
                
                //일정 범위 이상 안벗어 나게...
                if (dist > Button_Dist)
                {
                    Vector3 Key_dir = Vector3.Normalize(Local_target - Local_start);
                    Key_dir = Key_dir * Button_Dist;
                    Vector3 dst_pos = Local_start + Key_dir;
                    Move_key.transform.localPosition = dst_pos;
                }
                
                //Rotate Charecter
                //axis-z is main direction
                float t_angle;
                Vector3 z_axis = new Vector3(0, 0, 1);
                Vector3 Char_dir = GetModel_Direction();
                Vector3 t_cross_result = Vector3.Cross(z_axis, Char_dir);
                t_angle = Vector3.Angle(z_axis, Char_dir);
                
                if (t_cross_result.y < 0)
                    t_angle *= -1.0f;
                
                Character.transform.rotation = Quaternion.AngleAxis(t_angle, Vector3.up);
                
                //Move Charecter
                Character.transform.position += Char_dir;
            }
        }
    }
    
    Vector3 Calculate_button_pos(int dist)
    {
        Vector3 screen_pos = Input.mousePosition;
        Ray touch_ray = main_cam.ScreenPointToRay(screen_pos);
        Vector3 Origin_ray = touch_ray.origin;
        Vector3 Cam_ray_vec = Origin_ray - main_cam.gameObject.transform.position;
        float cam_ray_dist = Cam_ray_vec.magnitude;             //distance - cam, ray_origin
        float sub_dist = dist - cam_ray_dist;
        Vector3 dst_pos = touch_ray.GetPoint(sub_dist);
        
        return dst_pos;
    }
    
    //Get Model move direction
    //return normalized vector.
    private Vector3 GetModel_Direction()
    {
        Vector3 Start_floor_pos = GetFloor_pos(Move_board.transform.position);
        Vector3 GameKey_floor_pos = GetFloor_pos(Move_key.transform.position);
        
        Vector3 Dir_vec = GameKey_floor_pos - Start_floor_pos;
        return Dir_vec.normalized;
    }
    
    //Gamepad_pos + Ray_vec*t = floor_pos.
    //This fuction calculate t.
    private float GetScreentoFloor_Const(Vector3 p)
    {
        Vector3 t_Ray = p - main_cam.transform.position;
        
        return p.y / (-t_Ray.y);
    }
    
    private Vector3 GetFloor_pos(Vector3 p)
    {
        Vector3 t_Ray = p - main_cam.transform.position;
        float t_const = GetScreentoFloor_Const(p);  
        
        return p + t_const * t_Ray;
    }

    private bool InRectCheck(Rect UI)
    {
        Vector3 Mouse_pos = Input.mousePosition;
        Mouse_pos.y = Screen.height - Mouse_pos.y;
        if (UI.x <= Mouse_pos.x && Mouse_pos.x <= UI.x + UI.width)
        {
            if (UI.y <= Mouse_pos.y && Mouse_pos.y <= UI.y + UI.height)
                return true;
        }
        return false;
    }
    
    void OnGUI()
    {
        //Vector3 Cam_pos = main_cam.transform.position;
        //GUI.Label(new Rect(300, 300, 60, 60), "x:" + Cam_pos.x + " y:" + Cam_pos.y + " z:" + Cam_pos.z, this.textStyle);
    }
}

