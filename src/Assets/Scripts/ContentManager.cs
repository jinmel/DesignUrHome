using UnityEngine;
using System.Collections;

public class ContentManager : MonoBehaviour
{
		private static ContentManager instance = new ContentManager();
		public int Mode = 0; // 0 = default, button mode.
		public string imageTargetName;	//present tracking ImageTarget name.

		public static ContentManager getInstance ()
		{
				return instance;
		}

		void Start ()
		{
				//prevent destruction of singleton instance
				Debug.Log ("called");
				DontDestroyOnLoad (this);
		}

		void Update ()
		{
		}
}
