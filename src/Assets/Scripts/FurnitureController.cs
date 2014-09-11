using UnityEngine;
using System.Collections;

public class FurnitureController : MonoBehaviour
{
    public GUIStyle textStyle;
    public GameObject Move_key;
    public GameObject Move_key2;
    public GameObject Move_board;
    public Camera main_cam;
    public string selected_furniture;
    private float _time;
    private int touch_check;
    // touch_check == 1 -> Input.touchCount == 1 
    // touch_check == 2 -> Input.touchCount == 2

    //Move Key pad
    private Vector3 Start_point;
    private float Button_Dist_Max;
    private float Button_Dist_Min;
    //Rotate Key pad
    private float Start_angle;
    //Local position
    private Vector3 Local_start;
    private Vector3 Local_target;
    private ContentManager contentManager;

    // Use this for initialization
    void Start()
    {
        _time = Time.timeSinceLevelLoad;
        touch_check = 0;
        Button_Dist_Max = 0.7f;
        Button_Dist_Min = 0.1f;
        contentManager = ContentManager.getInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            Move_key.SetActive(false);
            Move_key2.SetActive(false);
            Move_board.SetActive(false);
            touch_check = 0;
        }
        if (contentManager.Mode == ContentManager.MODE.FURNITURE_MODE &&
            contentManager.Flag == 1)
        {
            if (Time.timeSinceLevelLoad - _time > 0.1f)
            {
                if (Input.touchCount == 1)
                { 
                    if (touch_check == 1)
                    {
                        //버튼 따라다니게 하기
                        Vector3 target_pos = calculateButtonPos(10, Input.mousePosition);
                        Move_key.transform.position = target_pos;
                        Local_target = Move_key.transform.localPosition;
                        float dist = Vector3.Distance(Local_target, Local_start);
                        
                        //일정 범위 이상 안벗어 나게...
                        if (dist > Button_Dist_Max)
                        {
                            Vector3 Key_dir = Vector3.Normalize(Local_target - Local_start);
                            Key_dir = Key_dir * Button_Dist_Max;
                            Vector3 dst_pos = Local_start + Key_dir;
                            Move_key.transform.localPosition = dst_pos;
                        }
                        
                        if (dist > Button_Dist_Min)
                        {
                            float t_angle;
                            Vector3 z_axis = new Vector3(0, 0, 1);
                            Vector3 Char_dir = getModelDirection();
                            Vector3 t_cross_result = Vector3.Cross(z_axis, Char_dir);
                            t_angle = Vector3.Angle(z_axis, Char_dir);
                            
                            if (t_cross_result.y < 0)
                                t_angle *= -1.0f;

                            if (Mathf.Abs(Char_dir.x) > Mathf.Abs(Char_dir.z))
                            {
                                Char_dir.z = 0;
                                GameObject.Find(selected_furniture).GetComponent<FurnitureCollider>().moveFurniture(Char_dir);
                            } else
                            {
                                Char_dir.x = 0;
                                GameObject.Find(selected_furniture).GetComponent<FurnitureCollider>().moveFurniture(Char_dir);
                            }
                        }
                    } else
                    {
                        touch_check = 1;
                        Start_point = calculateButtonPos(10, Input.mousePosition);
                        Move_key.transform.position = Start_point;
                        Local_start = Move_key.transform.localPosition;
                        Move_board.transform.position = calculateButtonPos(15, Input.mousePosition);
                        Move_key.SetActive(true);
                        Move_key2.SetActive(false);
                        Move_board.SetActive(true);
                        _time = Time.timeSinceLevelLoad;    
                    }
                } else if (Input.touchCount == 2)
                {
                    if (touch_check == 2)
                    {
                        //버튼 따라다니게 하기
                        Vector3[] target_pos = new Vector3[2];
                        target_pos [0] = calculateButtonPos(10, Input.GetTouch(0).position);
                        target_pos [1] = calculateButtonPos(10, Input.GetTouch(1).position);

                        Move_key.transform.position = target_pos [0];
                        Move_key2.transform.position = target_pos [1];


                        float now_angle = setAngle(target_pos [0], target_pos [1]);

                        float diff_angle = now_angle - Start_angle;
                        if (diff_angle > 360)
                            diff_angle -= 360;
                        if (diff_angle < -360)
                            diff_angle += 360;
                        if (Mathf.Abs(diff_angle) > 1.0f)
                        {
                            if (diff_angle > 0)
                                diff_angle = -1.0f;
                            else
                                diff_angle = 1.0f;
                            GameObject.Find(selected_furniture).GetComponent<FurnitureCollider>().rotateFurniture(diff_angle * 10.0f);
                            Start_angle = now_angle;
                        }
                    } else
                    {
                        touch_check = 2;
                        Move_key.transform.position = calculateButtonPos(10, Input.GetTouch(0).position);
                        Move_key2.transform.position = calculateButtonPos(10, Input.GetTouch(1).position);

                        Start_angle = setAngle(Move_key.transform.position, Move_key2.transform.position);
                        Move_key.SetActive(true);
                        Move_key2.SetActive(true);
                        Move_board.SetActive(false);
                        _time = Time.timeSinceLevelLoad;    
                    }
                }
            }
        } else
        {
            _time = Time.timeSinceLevelLoad + 0.2f;
        }
    }

    float setAngle(Vector3 first, Vector3 second)
    {
        first.y = 0;
        second.y = 0;
        Vector3 sub = first - second;
        return Mathf.Atan2(sub.z, sub.x) * Mathf.Rad2Deg;
    }

    Vector3 calculateButtonPos(int dist, Vector3 input)
    {
        Vector3 screen_pos = input;
        Ray touch_ray = main_cam.ScreenPointToRay(screen_pos);
        Vector3 Origin_ray = touch_ray.origin;
        Vector3 Cam_ray_vec = Origin_ray - main_cam.gameObject.transform.position;
        float cam_ray_dist = Cam_ray_vec.magnitude;             //distance - cam, ray_origin
        float sub_dist = dist - cam_ray_dist;
        Vector3 dst_pos = touch_ray.GetPoint(sub_dist);

        return dst_pos;
    }

    private Vector3 getModelDirection()
    {
        Vector3 Start_floor_pos = getFloorPos(Move_board.transform.position);
        Vector3 GameKey_floor_pos = getFloorPos(Move_key.transform.position);
        
        Vector3 Dir_vec = GameKey_floor_pos - Start_floor_pos;
        return Dir_vec.normalized;
    }

    //Gamepad_pos + Ray_vec*t = floor_pos.
    //This fuction calculate t.
    private float getScreentoFloorConst(Vector3 p)
    {
        Vector3 t_Ray = p - main_cam.transform.position;
        
        return p.y / (-t_Ray.y);
    }

    private Vector3 getFloorPos(Vector3 p)
    {
        Vector3 t_Ray = p - main_cam.transform.position;
        float t_const = getScreentoFloorConst(p);   

        return p + t_const * t_Ray;
    }

    void OnGUI()
    {
        Vector3 Cam_pos = main_cam.transform.position;
//          this.textStyle.fontSize = 30;
//          GUI.Label (new Rect (300, 300, 60, 60), diff_angle.ToString(), this.textStyle);
    }
}

