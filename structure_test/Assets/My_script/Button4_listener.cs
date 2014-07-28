using UnityEngine;
using System.Collections;

public class Button4_listener : MonoBehaviour {

	private bool model_render_check;
	public GameObject Apartment2;
	public GameObject structure1;
	// Use this for initialization
	void Start () {
		model_render_check = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {

		if (model_render_check == true) {
			model_render_check = false;
			Apartment2.SetActive(false);
			structure1.SetActive(false);
				} else {
			model_render_check = true;
			Apartment2.SetActive(true);
			structure1.SetActive(true);
				}
	}
}
