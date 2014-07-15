using UnityEngine;
using System.Collections;

public class Straight_man : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine("Straight_Rock");
	}
	
	// Update is called once per frame
	void Update () {

	}
	const float speedz = 0.005f;
	float dx = 0.0f;
	float dz = speedz;
	float x = 0.0f;
	float z = 1.0f;
	IEnumerator Straight_Rock(){
		while(true){
			yield return new WaitForSeconds(0.001f);
			x += dx;
			z += dz;
			this.transform.position= new Vector3(x,0.01f,z);
			float angle = Mathf.Atan2 (dx,dz) * Mathf.Rad2Deg;
			this.transform.eulerAngles = new Vector3(0.0f,angle,0.0f);
		}
	}
	float rad = 0.0f;
	void OnCollisionEnter(Collision collision) {
//		Vector3 from = collision.transform.position;
//		Vector3 to = this.transform.position;
		StopCoroutine("Straight_Rock");
		x -= dx*2;
		z -= dz*2;
		this.transform.position= new Vector3(x,0.01f,z);
		float angle = Mathf.Atan2 (dx,dz) * Mathf.Rad2Deg;
		this.transform.eulerAngles = new Vector3(0.0f,angle,0.0f);

		rad += (Mathf.PI/ 36.0f);
		dx = Mathf.Sin (rad) * speedz;
		dz = Mathf.Cos (rad) * speedz;
		Debug.Log("Collision "+rad.ToString());
		StartCoroutine("Straight_Rock");
	}
}
