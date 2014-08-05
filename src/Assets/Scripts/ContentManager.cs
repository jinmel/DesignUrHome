using UnityEngine;
using System.Collections;

public class ContentManager
{
		private static ContentManager instance = new ContentManager ();
		public int Mode = 0; // 0 = default, button mode.
		public int Flag = 0; // it seperate status of each mode.
		public string imageTargetName;	//present tracking ImageTarget name.
		public Rect UI_Domain;

		//Definition
		public const int DEFAULT_MODE = 0;
		public const int LIGHT_MODE = 1;
		public const int FURNITURE_MODE = 2;
		public const int CHARACTER_MODE = 3;
		public const int RENDER_ONOFF_MODE = 4;

		public static ContentManager getInstance ()
		{
				return instance;
		}
}
