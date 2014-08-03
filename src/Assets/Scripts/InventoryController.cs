using UnityEngine;
using System.Collections;

public class InventoryController : MonoBehaviour
{
		public GUIStyle inventoryBoxStyle;
		public GUIStyle inventoryContainerStyle;
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
				//begin inventory gui group
				int guiBoxLeftMargin = 20;
				int guiBoxTopMargin = Screen.height/4 * 3	;
				int guiBoxWidth = Screen.width - guiBoxLeftMargin;
				int guiBoxHeight = Screen.height / 4 - guiBoxTopMargin;

				GUI.BeginGroup (new Rect (10, Screen.height / 4 * 3, guiBoxWidth, guiBoxHeight));
				

				GUI.Box (new Rect (0, 0, guiBoxWidth, guiBoxHeight), "Inventory");
				GUI.backgroundColor = Color.yellow;
				GUI.Box (new Rect (10, 10, guiBoxWidth / 5, guiBoxHeight - 10), "fuck");

				GUI.EndGroup ();
		}
	
}
