using UnityEngine;
using System.Collections;

enum ItemNumber
{
    None = 0,
    Bed}
;

public class InventoryController : MonoBehaviour
{
    public GUIStyle itemBoxStyle;
    public GUIStyle inventoryBoxStyle;

    //items for Inventory
    public Texture2D bedImage;
    public GameObject bedModel;

    //status variables
    private ItemNumber curItem;
        
        
    // Use this for initialization
    void Start()
    {
        Debug.Log("created");
        Debug.Log("width:" + Screen.width.ToString() + " height:" + (string)Screen.height.ToString());
        //instantiate bed object
        Instantiate(bedModel);
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 mousepos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            switch (curItem)
            {
            //item 1 - bed
                case ItemNumber.Bed:
                    bedModel.transform.position = mousepos;
                    bedModel.SetActive(true);
                    break;
                default:
                    break;
            }

        } else
        {
            switch (curItem)
            {
                case ItemNumber.Bed:
                    bedModel.SetActive(false);
                    break;
                default:
                    break;
                
            }
        }
                
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

        if (GUI.Button(new Rect(itemBoxLeftMargin, itemBoxTopMargin, itemBoxWidth, itemBoxHeight), new GUIContent(bedImage), itemBoxStyle))
        {
            Debug.Log("Bed clicked");
            curItem = ItemNumber.Bed;
        }
        GUI.Button(new Rect(itemBoxLeftMargin * 2 + itemBoxWidth, itemBoxTopMargin, itemBoxWidth, itemBoxHeight), "item2");
        GUI.Button(new Rect(itemBoxLeftMargin * 3 + itemBoxWidth * 2, itemBoxTopMargin, itemBoxWidth, itemBoxHeight), "item3");
        GUI.Button(new Rect(itemBoxLeftMargin * 4 + itemBoxWidth * 3, itemBoxTopMargin, itemBoxWidth, itemBoxHeight), "item4");
        GUI.Button(new Rect(itemBoxLeftMargin * 5 + itemBoxWidth * 4, itemBoxTopMargin, itemBoxWidth, itemBoxHeight), "item5");

        GUI.EndGroup();
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
}
