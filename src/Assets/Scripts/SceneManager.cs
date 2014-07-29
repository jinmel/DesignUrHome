using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
		private static SceneManager instance = new SceneManager ();
		public int Mode = 0; // 0 = default

		public static SceneManager getInstance ()
		{
				return instance;
		}

		void Start ()
		{
				//prevent destruction of singleton instance
				DontDestroyOnLoad (this);
		}

		void Update ()
		{
		}
}
