using UnityEngine;
using System.Collections;

public class SplashManager : MonoBehaviour
{

    private Texture splashImage;
    private AsyncOperation async;
    private Texture inLoadBar;
    private Texture outLoadBar;
    

    // Use this for initialization
    void Start()
    {
        //draw texture
        splashImage = Resources.Load<Texture>("splashs");
        StartCoroutine(LoadNextScene());

        //Screen orientation
        Screen.orientation = ScreenOrientation.AutoRotation;

        inLoadBar = Resources.Load("loadingbar_in") as Texture;
        outLoadBar = Resources.Load("loadingbar_out") as Texture;
    }
    
    // Update is called once per frame
	void Update()
	{
		Screen.orientation = ScreenOrientation.LandscapeRight;
	}

    void OnGUI()
    {
        //calculate loading bar position
        float screenCenter = Screen.width / 2;
        float inLoadbarLeft = screenCenter - inLoadBar.width / 2;
        float outLoadbarLeft = screenCenter - outLoadBar.width / 2;
        float inLoadbarTop = Screen.height - 100;
        float outLoadbarTop = Screen.height - 100;

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), splashImage);
        GUI.DrawTexture(new Rect(outLoadbarLeft, outLoadbarTop, outLoadBar.width, outLoadBar.height), outLoadBar);
        GUI.DrawTexture(new Rect(inLoadbarLeft + 10, inLoadbarTop + 10, inLoadBar.width * async.progress, inLoadBar.height * 0.75F), inLoadBar);

    }

    IEnumerator LoadNextScene()
    {
        async = Application.LoadLevelAsync("MainScene");
        yield return async;
    }
}
