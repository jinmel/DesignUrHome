﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    public GUIStyle itemBoxStyle;
    public GUIStyle inventoryBoxStyle;

	//items for Inventory
	public int itemCount;
    public Texture2D[] images;
    public GameObject[] models;

    //privat variables
    private GameObject curModel;
    private List<GameObject> objectInstanceList; // keep the list of instance of inventory items created by user. 
    private bool itemSelected; //status variable for if user is holding the item. 
    private Camera ar_camera;
    private int making_furniture_count = 1;
	private int listMoving = 0;
    private ContentManager contentManager;
  
    // Use this for initialization
    void Start()
    {
        contentManager = ContentManager.getInstance();
        ar_camera = Camera.main;
        objectInstanceList = new List<GameObject>();
        itemSelected = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (itemSelected)
        {
            //follow cursor 
            if (Input.GetMouseButton(0))
            {
                Ray screenray = Camera.main.ScreenPointToRay(Input.mousePosition);
                curModel.transform.position = screenray.origin + 440 * screenray.direction;
                Debug.Log("cursor position:" + Input.mousePosition.ToString() + "item position:" + curModel.transform.position.x.ToString());
            } else
            { //release
                Vector3 curPos = curModel.transform.position;
                curModel.transform.position = CalculatePlanePos();
                Debug.Log("release item on: " + curModel.transform.position.ToString());
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

			Rect[] buttonBox = new Rect[itemCount];
			for(int i = 0;i < itemCount; i++){
				buttonBox[i] = new Rect(itemBoxLeftMargin * (i+1) + itemBoxWidth * i + listMoving, itemBoxTopMargin, itemBoxWidth, itemBoxHeight);
			}
//            Rect button1Box = new Rect(itemBoxLeftMargin, itemBoxTopMargin, itemBoxWidth, itemBoxHeight);
//            Rect button2Box = new Rect(itemBoxLeftMargin * 2 + itemBoxWidth, itemBoxTopMargin, itemBoxWidth, itemBoxHeight);
//            Rect button3Box = new Rect(itemBoxLeftMargin * 3 + itemBoxWidth * 2, itemBoxTopMargin, itemBoxWidth, itemBoxHeight);
//            Rect button4Box = new Rect(itemBoxLeftMargin * 4 + itemBoxWidth * 3, itemBoxTopMargin, itemBoxWidth, itemBoxHeight);
//            Rect button5Box = new Rect(itemBoxLeftMargin * 5 + itemBoxWidth * 4, itemBoxTopMargin, itemBoxWidth, itemBoxHeight);

            GUI.Box(buttonBox[0], new GUIContent(images [0]), itemBoxStyle);
			GUI.Box(buttonBox[1], new GUIContent(images [1]), itemBoxStyle);
            GUI.Box(buttonBox[2], "item3", itemBoxStyle);
            GUI.Box(buttonBox[3], "item4", itemBoxStyle);
            GUI.Box(buttonBox[4], "item5", itemBoxStyle);
            GUI.EndGroup();

            Event curEvent = Event.current;
			Vector3[] itemScales = new Vector3[itemCount];
			itemScales[0] = new Vector3(5.0f,5.0f,5.0f);
			itemScales[1] = new Vector3(20.0f,20.0f,20.0f);
			for(int i = 0;i < itemCount; i++){
	            //handle box 1 event
	            if (curEvent.type == EventType.mouseDown && buttonBox[i].Contains(Input.mousePosition))
	            {
	                itemSelected = true;
	                curModel = (GameObject)Instantiate(models [i]);
	                curModel.SetActive(true);
	                curModel.transform.localScale = itemScales[i];
				}
			}
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
