using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour{
	private static Singleton singleton = new Singleton();
	public int Mode = 0; // 0 = default
	public string target_name;	// present tracking target name. ex) ImageTarget_apartment2

	public static Singleton GetInstance(){
		return singleton;
	}
	void Start(){
		DontDestroyOnLoad(this);
	}
	void Update(){
	}
}
