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
    private bool inventoryItemSelected;


  
    // Use this for initialization
    void Start()
    {
        objectInstanceList = new List<GameObject>();
        inventoryItemSelected = false;

    }
    
    // Update is called once per frame
    void Update()
    {

        if (curModel){
            Debug.Log(curModel.name);
        }
        if (inventoryItemSelected)
        {
            //follow cursor 
            curModel.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        } else //release
        {
            inventoryItemSelected = false;
        }
     
    }

    Vector3 ScreenPointToRealPosition(Vector3 screenpos, int distance)
    {
        if (Camera.main)
        {
            Ray ray = Camera.current.ScreenPointToRay(screenpos);
            Debug.Log("Camera Position : " + Camera.current.transform.position + "Ray origin: " + ray.origin);
            return ray.origin + ray.direction * distance;
        } else
            return screenpos;
           
    }

    void OnGUI()
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

        Rect button1Box = new Rect(itemBoxLeftMargin, itemBoxTopMargin, itemBoxWidth, itemBoxHeight);
        Rect button2Box = new Rect(itemBoxLeftMargin * 2 + itemBoxWidth, itemBoxTopMargin, itemBoxWidth, itemBoxHeight);
        Rect button3Box = new Rect(itemBoxLeftMargin * 3 + itemBoxWidth * 2, itemBoxTopMargin, itemBoxWidth, itemBoxHeight);
        Rect button4Box = new Rect(itemBoxLeftMargin * 4 + itemBoxWidth * 3, itemBoxTopMargin, itemBoxWidth, itemBoxHeight);
        Rect button5Box = new Rect(itemBoxLeftMargin * 5 + itemBoxWidth * 4, itemBoxTopMargin, itemBoxWidth, itemBoxHeight);

        GUI.Box(button1Box, new GUIContent(images [0]), itemBoxStyle);
        GUI.Box(button2Box, "item2", itemBoxStyle);
        GUI.Box(button3Box, "item3", itemBoxStyle);
        GUI.Box(button4Box, "item4", itemBoxStyle);
        GUI.Box(button5Box, "item5", itemBoxStyle);

 
        GUI.EndGroup();

        Event curEvent = Event.current;
        Debug.Log(curEvent);

        if(curEvent.type == EventType.mouseDown && button1Box.Contains(Input.mousePosition)){
            inventoryItemSelected = true;
            curModel = (GameObject)Instantiate(models[0]);
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
