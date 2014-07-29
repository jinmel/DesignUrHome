using UnityEngine;
using System.Collections;

public class Button2_listener : MonoBehaviour {

	public GameObject DropIt;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		Singleton.GetInstance().Mode = 2;

		Vector3 Pos = GameObject.Find ("ARCamera").transform.position;
		Pos.y -= 130;

		int x = Random.Range (-50, 50);
		int z = Random.Range (-50, 50);

		Pos.x -= x;
		Pos.z -= z;

		Instantiate (DropIt, Pos, DropIt.transform.rotation);
	}
}
