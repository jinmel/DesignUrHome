using UnityEngine;
using System.Collections;

public class Button3_listener : MonoBehaviour {
  //view mode
  //////////////////////////////////////////////
  // mode 0 : default
  // mode 1 : bird-eyes view
  // mode 2 : target following view mode
  //////////////////////////////////////////////

  public GameObject main_model;
  public Camera AR_Camera;
  public GameObject _GamePad;
  public Camera _CAM;
  public GameObject _Light;

  private int mode_checker;
  private GameObject _Character;
  private Object _t_Light;
  private GameObject _BackgroundCam;

  // Use this for initialization
  void Start () {
    mode_checker = 0;
    _BackgroundCam = GameObject.Find ("BackgroundCamera(Clone)");
  }
	
  // Update is called once per frame
  void Update () {
  }
	
  public void OnMouseDown() {
    SceneManager.getInstance().Mode = 3;
    mode_checker = (mode_checker + 1) % 3;

    switch (mode_checker) {
    case 0:
      //destroy all human model

      //Destroy temp Light
      DestroyObject(_t_Light);
      _CAM.gameObject.SetActive(false);

      //Set Active Cam
      AR_Camera.gameObject.SetActive(true);

      break;
    case 1:
      //Create human model & not change camera
      //Create main model & attach model controller
      _GamePad.SetActive(true);
      break;
    case 2:
      //disable ARcamera & change camera view
      //when changeing view, camera pos & rotation => ARcamera pos to main model
      //AR_Camera.gameObject.SetActive(false);
      _GamePad.SetActive(false);

      //Main Light Copy
      _t_Light = Instantiate(_Light, _Light.transform.position, _Light.transform.rotation);
      AR_Camera.gameObject.SetActive(false);

      _CAM.transform.position = AR_Camera.transform.position;
      _CAM.transform.rotation = AR_Camera.transform.rotation;
      _CAM.gameObject.SetActive(true);

      break;
    }
  }
}
