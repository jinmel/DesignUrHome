using UnityEngine;
using System.Collections;

public class ContentManager : MonoBehaviour
{
		private static ContentManager instance = null;
		public int Mode = 0; // 0 = default, button mode.
		public string ImageTarget_name;	//present tracking ImageTarget name.

		public static ContentManager getInstance ()
		{
				return instance;
		}

		void Start ()
		{
				//prevent destruction of singleton instance
				instance = this;	
				DontDestroyOnLoad (this);
		}

		void Update ()
		{
		}
}
