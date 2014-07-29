using UnityEngine;
using System.Collections;

public class Button4_listener : MonoBehaviour {

	private bool model_render_check;
	public GameObject _Image_Target;

	// Use this for initialization
	void Start () {
		model_render_check = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		Singleton.GetInstance().Mode = 4;

		if (model_render_check == true) {
			model_render_check = false;
			_Image_Target.SetActive(false);
				} else {
			model_render_check = true;
			_Image_Target.SetActive(true);
				}
	}
}
