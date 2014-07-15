using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour {
	// Use this for initialization
	void Start () {
		StartCoroutine("Rock");
	}
	
	// Update is called once per frame
	void Update () {

	}
	IEnumerator Rock(){
		float radius = 10.0f;
		float x=radius,z=0;
		int wise = 0;
		float rad = 0;
		while(true){
			yield return new WaitForSeconds(0.01f);
//			this.transform.position= new Vector3(0.0f,0.0f,0.0f);
			this.transform.position= new Vector3(x,0.0f,z);
			this.transform.eulerAngles = new Vector3(0.0f,-rad/Mathf.PI*180.0f,0.0f);
			rad += 0.1f;
			x = Mathf.Cos (rad) * radius;
			z = Mathf.Sin (rad) * radius;
			Debug.Log(x.ToString()+","+z.ToString());
		}
	}
}
