using UnityEngine;
using System.Collections;

public class SplashManager : MonoBehaviour {

    private Texture splashImage;

	// Use this for initialization
	void Start () {
        //draw texture
        splashImage = Resources.Load<Texture>("splash");
        StartCoroutine(LoadNextScene(2.0F));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI(){
        GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),splashImage);
    }

    IEnumerator LoadNextScene(float delay){
        yield return new WaitForSeconds(delay);
        Application.LoadLevel("MainScene");

    }
}
