using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    public GUIStyle itemBoxStyle;
    public GUIStyle inventoryBoxStyle;

	//items for Inventory
    public Texture2D[] images;
    public GameObject[] models;

    //privat variables
    private GameObject curModel;
    private List<GameObject> objectInstanceList; // keep the list of instance of inventory items created by user. 
    private bool itemSelected; //status variable for if user is holding the item. 
    private Camera ar_camera;
    private int making_furniture_count = 1;
	private float listMoving = 0;
	private ContentManager contentManager;
	private int itemCount;
	private Rect[] buttonBox;
	private Vector3[] itemScales;
	// Click Event
	private bool Click_Mouse_down;
	private int Click_Box_Num;
	private Vector3 Click_StartPosition;
    // Use this for initialization
    void Start()
    {
        contentManager = ContentManager.getInstance();
        ar_camera = Camera.main;
        objectInstanceList = new List<GameObject>();
        itemSelected = false;
		itemCount = images.Length;
		Click_Mouse_down = false;
		buttonBox = new Rect[itemCount];
		itemScales = new Vector3[itemCount];
    }
    
    // Update is called once per frame
    void Update()
	{
		if (contentManager.Mode == ContentManager.MODE.FURNITURE_MODE &&
		     contentManager.Flag == 0)
		{
	        if (itemSelected)
	        {
	            //follow cursor 
	            if (Input.GetMouseButton(0))
	            {
	                Ray screenray = Camera.main.ScreenPointToRay(Input.mousePosition);
	                curModel.transform.position = screenray.origin + 440 * screenray.direction;
	            } else
	            { //release
	                Vector3 curPos = curModel.transform.position;
	                curModel.transform.position = CalculatePlanePos();
	                if (contentManager.imageTargetName != null)
	                {
	                    GameObject parent = GameObject.Find(contentManager.imageTargetName);
	                    curModel.transform.parent = parent.transform.FindChild("ObjectList");
	                    curModel.transform.name = "furniture_" + making_furniture_count;
	                    making_furniture_count ++;
	                    FurnitureCollider FC = curModel.AddComponent<FurnitureCollider>();
	                    FC.now_position = curModel.transform.localPosition;
	                    FC.now_rotation = curModel.transform.localEulerAngles;
	                    Rigidbody rigid = curModel.AddComponent<Rigidbody>();
	                    rigid.useGravity = true;
	                } else
	                {
	                    Destroy(curModel);
	                }
	                itemSelected = false;
	                curModel = null; 
	            }
	        }
			else if(Input.GetMouseButton(0)){
				if(!Click_Mouse_down){
					Click_Box_Num = -1;
					Click_Mouse_down = true;
					Click_StartPosition = Input.mousePosition;
					for(int i = 0;i < itemCount; i++){
						if (buttonBox[i].Contains(Input.mousePosition))
						{
							Click_Box_Num = i;
					 	}
					}
				}
				else{
					if(Input.mousePosition.y > (Screen.height / 4) - 8 && Click_Box_Num != -1){
			            itemSelected = true;
			            curModel = (GameObject)Instantiate(models [Click_Box_Num]);
		                curModel.SetActive(true);
	  	                curModel.transform.localScale = itemScales[Click_Box_Num];
					}
					else{
						listMoving += Input.mousePosition.x - Click_StartPosition.x;
						Click_StartPosition = Input.mousePosition;
					}
				}
			}
			else{
				Click_Mouse_down = false;
			}
		}
    }

    Vector3 CalculatePlanePos()
    {
        Vector3 screen_pos = Input.mousePosition;
        Ray touch_ray = ar_camera.ScreenPointToRay(screen_pos);
        float t;
        t = -touch_ray.origin.y / touch_ray.direction.y;

        return touch_ray.origin + t * touch_ray.direction;
    }

    void OnGUI()
    {   
        if (contentManager.Mode == ContentManager.MODE.FURNITURE_MODE &&
            contentManager.Flag == 0)
        {
            //Inventory Layout - determined dynamically according to screen size
            int guiContainerLeftMargin = 10;
            int guiContainerTopMargin = Screen.height / 4 * 3;
            int guiContainerWidth = Screen.width - guiContainerLeftMargin * 2;
            int guiContainerHeight = Screen.height / 4 - 10;

            int itemBoxWidth = guiContainerWidth / 5 - 10;
            int itemBoxHeight = guiContainerHeight - 20;

            GUI.BeginGroup(new Rect(guiContainerLeftMargin, guiContainerTopMargin, guiContainerWidth, guiContainerHeight));
            
            int itemBoxLeftMargin = 10;
            int itemBoxTopMargin = 10;
			if(listMoving > 0) listMoving = 0;
			if(listMoving < Screen.width - (itemBoxLeftMargin * (itemCount) + itemBoxWidth * itemCount)){
			    if(Screen.width - (itemBoxLeftMargin * (itemCount) + itemBoxWidth * itemCount ) < 0)
					listMoving = Screen.width - (itemBoxLeftMargin * (itemCount) + itemBoxWidth * itemCount );
				else
					listMoving = 0;
			}
			Debug.Log (listMoving);
			for(int i = 0;i < itemCount; i++){
				buttonBox[i] = new Rect(itemBoxLeftMargin * (i+1) + itemBoxWidth * i + listMoving, itemBoxTopMargin, itemBoxWidth, itemBoxHeight);
			}

			for(int i = 0;i < itemCount; i++){
				GUI.Box (buttonBox[i],new GUIContent(images [i]), itemBoxStyle);
			}
            GUI.EndGroup();
			itemScales[0] = new Vector3(5.0f,5.0f,5.0f);
			itemScales[1] = new Vector3(20.0f,20.0f,20.0f);
//			itemScales[2] = new Vector3(5.0f,5.0f,5.0f);
//			itemScales[3] = new Vector3(20.0f,20.0f,20.0f);
//			itemScales[4] = new Vector3(5.0f,5.0f,5.0f);
//			itemScales[5] = new Vector3(20.0f,20.0f,20.0f);
//			itemScales[6] = new Vector3(5.0f,5.0f,5.0f);
//			itemScales[7] = new Vector3(20.0f,20.0f,20.0f);
//			itemScales[8] = new Vector3(5.0f,5.0f,5.0f);
//			itemScales[9] = new Vector3(20.0f,20.0f,20.0f);
        }
		else{
			listMoving = 0;
		}
    }

    private Texture2D plainColor2DTexture(int width, int height, Color color)
    {
        Color [] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i ++)
        {
            pix [i] = color;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply(); 
        return result;
    }

    private void DestroyInventoryItemInstances()
    {
        foreach (GameObject obj in objectInstanceList)
        {
            Destroy(obj);
        }

        objectInstanceList.Clear();
    }

}
