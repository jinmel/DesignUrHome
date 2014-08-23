using UnityEngine;
using System.Collections;

public class ContentManager
{
    private static ContentManager instance = null;

    public enum MODE
    {
        DEFAULT_MODE,
        LIGHT_MODE,
        FURNITURE_MODE,
        CHARACTER_MODE,
        RENDER_ONOFF_MODE}
    ;

    public MODE Mode = MODE.DEFAULT_MODE;
    public int Flag = 0; // it seperate status of each mode.
    public string imageTargetName;  //present tracking ImageTarget name.
    public Rect UI_Domain;

    public static ContentManager getInstance()
    {
        if(instance == null)
            instance = new ContentManager();
        return instance;
    }
}
