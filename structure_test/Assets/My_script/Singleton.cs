using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour{
	private static Singleton singleton = new Singleton();
	public int Mode = 0; // 0 = default

	public static Singleton GetInstance(){
		return singleton;
	}
	void Start(){
		DontDestroyOnLoad(this);
	}
	void Update(){
	}
}
