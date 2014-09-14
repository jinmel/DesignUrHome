using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour
{
    
    //MainScene Object
    public Camera ARCamera;                     // vuforia camera
    public Camera CAM;                          // external Camera (using button3)
    public GameObject Light;                    // main Light in Camera
    public GameObject GamePad;                  // GamePad. in button3
    public GameObject ImageTarget;              // ImageTarget in vuforia
    public GameObject Character;                // Charector model
    public GameObject Sun;                      // mode 1 - SUN
    public GameObject PlanePrefab;
    
    //...
    private GameObject tFloor;
    private GameObject t_ObjList;               // Button3 - mode3. 가구 및 구조물 저장;
    private Object tLight;                      // Button3 - mode3. 1인칭 시점에서 사용
    private bool model_render_check;            // Button4 - 
    private int mode_checker;
    private int SunMode = -1;
    private string prev_Target_name;
    private GameObject tPlane;

    public GUIStyle container_style;
	public Texture button1_image;
	public Texture button2_image;
	public Texture button3_image;
	public Texture button4_image;

	private Texture button_on_image;
	private Texture button_off_image;
    public GUIStyle tDebug;

    private ContentManager contentManager;
    
    // Use this for initialization
    void Start()
    {   
        mode_checker = 0;
        prev_Target_name = null;

        //UI Size store
        contentManager = ContentManager.getInstance();
        contentManager.UI_Domain = new Rect(50, 40, 80, 350);

		button_on_image = Resources.Load ("button_on") as Texture;
		button_off_image = Resources.Load ("button_off") as Texture;
    }
    
    // Update is called once per frame
    void Update()
    {
		Screen.orientation = ScreenOrientation.LandscapeRight;
    }

    void OnGUI()
    {
        // Handle Light Orientation 
        if (GUI.Button(new Rect(50, 40, 80, 80),button1_image))
        {
            contentManager.Mode = ContentManager.MODE.LIGHT_MODE;

            SunMode *= -1;

            //Create SUN
            if (SunMode == 1)
            {
                Light.SetActive(false);
                Sun.SetActive(true);
                                
                //Make plane
                //tPlane = (GameObject)Instantiate (PlanePrefab);
                //GameObject tImgTarget = GameObject.Find (ContentManager.getInstance ().imageTargetName);
                //tPlane.transform.parent = tImgTarget.transform.GetChild (0);
                //tPlane.transform.position = tImgTarget.transform.position;
            }
                        //Delete SUN
                        else
            {
                Light.SetActive(true);
                Sun.SetActive(false);

                //Destroy Plane
                //DestroyObject (tPlane);
            }
        }
        if (GUI.Button(new Rect(50, 130, 80, 80), button2_image))
        {
			if(contentManager.Mode != ContentManager.MODE.FURNITURE_MODE){
	            contentManager.Mode = ContentManager.MODE.FURNITURE_MODE;
	            contentManager.Flag = 0;
			}
			else if(contentManager.Flag == 0){
				contentManager.Mode = ContentManager.MODE.DEFAULT_MODE;
			}
			else{
				contentManager.Flag = 0;
			}
        }

        if (GUI.Button(new Rect(50, 220, 80, 80), button3_image))
        {
            //view mode
            //////////////////////////////////////////////
            // mode 0 : default
            // mode 1 : bird-eyes view
            // mode 2 : target following view mode
            //////////////////////////////////////////////
            
            contentManager.Mode = ContentManager.MODE.CHARACTER_MODE;
            mode_checker = (mode_checker + 1) % 3;
            
            GameObject tImageTarget;                    //SceneManger -> ImageTarget find
            string t_name = contentManager.imageTargetName;
            
            if (t_name != null && !t_name.Equals(prev_Target_name))
            {
                Mode3Initialize();
                mode_checker = 1;
            }
            
            switch (mode_checker)
            {
                case 0:
                                //destroy all human model
                    Mode3Initialize();
                
                    break;
                case 1:
                                //Tracking nothing - action nothing
                    if (t_name == null)
                    {
                        mode_checker = 0;
                        break;
                    }
                
                                //Create human model & not change camera
                                //Create main model & attach model controller
                    GamePad.SetActive(true);
                
                    tImageTarget = GameObject.Find(t_name);
                
                    Character.transform.position = tImageTarget.transform.position;
                    Character.transform.parent = tImageTarget.transform.GetChild(0).gameObject.transform;
                    Character.SetActive(true);
                    break;
                case 2:
                                //disable ARcamera & change camera view
                                //when changeing view, camera pos & rotation => ARcamera pos to main model
                                //AR_Camera.gameObject.SetActive(false);
                    GamePad.SetActive(false);
                
                                //Main Light Copy
                    tLight = Instantiate(Light, Light.transform.position, Light.transform.rotation);
                
                    CAM.transform.position = ARCamera.transform.position;
                    CAM.transform.rotation = ARCamera.transform.rotation;
                    CAM.gameObject.SetActive(true);
                
                                //Camera moving start;
                    CAM.gameObject.transform.GetComponent<SetCamPos>().Cam_posSet();
                
                                //Create Structure
                    tImageTarget = GameObject.Find(t_name);
                    GameObject t_GameObj = tImageTarget.transform.GetChild(0).gameObject;
                    t_ObjList = (GameObject)Instantiate(t_GameObj, t_GameObj.transform.position, t_GameObj.transform.rotation);
                                
                                //Set structure scale
                    Vector3 t_scale = new Vector3(t_GameObj.transform.localScale.x * tImageTarget.transform.localScale.x
                                               , t_GameObj.transform.localScale.y * tImageTarget.transform.localScale.y
                                               , t_GameObj.transform.localScale.z * tImageTarget.transform.localScale.z);
                    t_ObjList.transform.localScale = t_scale;
                
                                //Create Floor
                                //...               
                
                                //ARCamera shut down
                    ARCamera.gameObject.SetActive(false);
                    ImageTarget.SetActive(false);
                
                    break;
            }
            
            //Store prev target name
            prev_Target_name = t_name;
        }

        if (GUI.Button(new Rect(50, 310, 80, 80), button4_image))
        {
            contentManager.Mode = ContentManager.MODE.RENDER_ONOFF_MODE;

            if (model_render_check == true)
            {
				button4_image = button_off_image;
                model_render_check = false;
                ImageTarget.SetActive(false);
            } else
            {
				button4_image = button_on_image;
                model_render_check = true;
                ImageTarget.SetActive(true);
            }
        }
                
        //GUI.Label(new Rect(200, 5, 30, 30), contentManager.imageTargetName, tDebug);

        if (contentManager.Mode == ContentManager.MODE.FURNITURE_MODE && 
            contentManager.Flag == 1)
        {
            Texture Delete_Button = new Texture();
            Delete_Button = (Texture)Resources.Load("Delete", typeof(Texture));

            if (GUI.Button(new Rect(Screen.width - 130, 40, 80, 80), Delete_Button))
            {
                contentManager.Mode = ContentManager.MODE.FURNITURE_MODE;
                contentManager.Flag = 0;
                string selected_furniture_name = GameObject.Find("FurnitureMovingPad").GetComponent<FurnitureController>().selected_furniture;
                GameObject.Destroy(GameObject.Find(selected_furniture_name));
            }
        }   
    }

    private void Mode3Initialize()
    {
        //Destroy temp Light
        DestroyObject(tLight);
        CAM.gameObject.SetActive(false);
        
        //kill character model
        Character.SetActive(false);
        
        //Set Active Cam
        ARCamera.gameObject.SetActive(true);
        ImageTarget.SetActive(true);
        
        //killllllll Gamepad
        GamePad.SetActive(false);

        //List clear
        DestroyObject(t_ObjList);
    }
}