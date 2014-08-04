using UnityEngine;
using System.Collections;

public class InventoryController : MonoBehaviour
{
		public GUIStyle itemBoxStyle;
		public GUIStyle inventoryBoxStyle;
		// Use this for initialization
		void Start ()
		{
				Debug.Log ("created");
				Debug.Log ("width:" + Screen.width.ToString () + " height:" + (string)Screen.height.ToString ());
		}
	
		// Update is called once per frame
		void Update ()
		{

		}

		void OnGUI ()
		{	
				//Inventory Layout - determined dynamically according to screen size
				int guiContainerLeftMargin = 10;
				int guiContainerTopMargin = Screen.height / 4 * 3;
				int guiContainerWidth = Screen.width - guiContainerLeftMargin * 2;
				int guiContainerHeight = Screen.height / 4 - 10;

				int itemBoxWidth = guiContainerWidth / 5 - 10;
				int itemBoxHeight = guiContainerHeight - 20;

				GUI.BeginGroup (new Rect (guiContainerLeftMargin, guiContainerTopMargin, guiContainerWidth, guiContainerHeight));
				
				GUI.Box (new Rect (0, 0, guiContainerWidth, guiContainerHeight), "Inventory");

				int itemBoxLeftMargin = 10;
				int itemBoxTopMargin = 10;

				itemBoxStyle.normal.background = plainColor2DTexture (2, 2, Color.white);

				GUI.Box (new Rect (itemBoxLeftMargin, itemBoxTopMargin, itemBoxWidth, itemBoxHeight), "item1");
				GUI.Box (new Rect (itemBoxLeftMargin * 2 + itemBoxWidth, itemBoxTopMargin, itemBoxWidth, itemBoxHeight), "item2");
				GUI.Box (new Rect (itemBoxLeftMargin * 3 + itemBoxWidth * 2, itemBoxTopMargin, itemBoxWidth, itemBoxHeight), "item3");
				GUI.Box (new Rect (itemBoxLeftMargin * 4 + itemBoxWidth * 3, itemBoxTopMargin, itemBoxWidth, itemBoxHeight), "item4");
				GUI.Box (new Rect (itemBoxLeftMargin * 5 + itemBoxWidth * 4, itemBoxTopMargin, itemBoxWidth, itemBoxHeight), "item5");

				GUI.EndGroup ();
		}

		private Texture2D plainColor2DTexture (int width, int height, Color color)
		{
				Color [] pix = new Color[width * height];
				for (int i = 0; i < pix.Length; i ++) {
						pix [i] = color;
				}
				Texture2D result = new Texture2D (width, height);
				result.SetPixels (pix);
				result.Apply ();	
				return result;
		}
}
