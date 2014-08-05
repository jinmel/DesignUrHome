using UnityEngine;
using System.Collections;

public class ContentManager 
{
		private static ContentManager instance = new ContentManager();
		public int Mode = 0; // 0 = default, button mode.
		public int Flag = 0; // it seperate status of each mode.
		public string imageTargetName;	//present tracking ImageTarget name.
		public Rect UI_Domain;

		public static ContentManager getInstance ()
		{
				return instance;
		}
}
