using UnityEngine;
using System.Collections;

public class SetSunPos : MonoBehaviour
{

    public GameObject SunLight;                 //Control sun light direction.
    public Camera MainCamera;
    private Vector3 StructureCenterPos = new Vector3(0.0f, 0.0f, 0.0f);         //Tracking Target Center
    private Vector3 CenterToTouch;
    private bool MouseDownCheck = false;
    private float RevolutionRadius = -1.0f; //공전 반지름
    private float RevolutionVelocity = 1.0f;        //각 속도
    private float PresentAngle;                 //현재 각도
    private float AlphaAngle;
    private GameObject ImgTarget;
    public GUIStyle textStyle;
    private ContentManager contentManager;

    // Use this for initialization
    void Start()
    {
        contentManager = ContentManager.getInstance();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (contentManager.Mode == ContentManager.MODE.LIGHT_MODE)
        {

            if (Input.GetMouseButtonDown(0))
            {
                MouseDownCheck = true;
                ImgTarget = GameObject.Find((contentManager.imageTargetName));
                this.transform.parent = ImgTarget.transform.GetChild(0);

                PresentAngle = 0.0f;
            } else if (Input.GetMouseButtonUp(0))
            {
                MouseDownCheck = false;
            }
                        
            //Follow finger
            if (MouseDownCheck == true)
            {
                Vector3 TouchPos = CalculatePlanePos(Input.mousePosition);
                StructureCenterPos = ImgTarget.transform.position;
                CenterToTouch = TouchPos - StructureCenterPos;
                RevolutionRadius = CenterToTouch.magnitude;

                AlphaAngle = Vector3.Angle(Vector3.right, CenterToTouch);
                if (Vector3.Cross(CenterToTouch, Vector3.right).y < 0)
                    AlphaAngle = 360.0f - AlphaAngle;
                //Debug.Log (RevolutionRadius);
                AlphaAngle = Mathf.PI * AlphaAngle / 180.0f;

                this.transform.position = TouchPos;
                //Change Light Direction
                RotateSunLight();

                //Change Light Color
                ChangeSunColor();
            } else
            {
                //Auto movement\
                this.transform.position = CalculateSunPos();
                RotateSunLight();

                //Change Light Color
                ChangeSunColor();
            }

        }
    }

    private Vector3 CalculateSunPos()
    {
        Vector3 SunPos = new Vector3();
        PresentAngle += RevolutionVelocity % 360;

        float R_Angle = Mathf.PI * PresentAngle / 180.0f;

        SunPos.y = RevolutionRadius * Mathf.Sin(R_Angle);
        SunPos.x = RevolutionRadius * Mathf.Cos(R_Angle) * Mathf.Cos(AlphaAngle);
        SunPos.z = RevolutionRadius * Mathf.Cos(R_Angle) * Mathf.Sin(AlphaAngle);

        SunPos += StructureCenterPos;

        return SunPos;
    }

    private Vector3 CalculatePlanePos(Vector3 MousePos)
    {
        Ray MouseRay = MainCamera.ScreenPointToRay(MousePos);
        float t = -MouseRay.origin.y / MouseRay.direction.y;
        return MouseRay.origin + t * MouseRay.direction;
    }

    private void RotateSunLight()
    {
        Vector3 TargetDir = StructureCenterPos - this.transform.position;
        // local z-axis <= TargetDir
        SunLight.transform.forward = TargetDir.normalized;
    }

    private void ChangeSunColor()
    {
        const float Ybasis = 0.1f;
        const float Ymax = 0.4f;
        float tAdder = (0.8f - Ybasis - Ymax) / RevolutionRadius;
                
        float yellow, red;
        if (this.transform.position.y < 0)
        {
            red = 0.2f;
            yellow = Ybasis;
        } else
        {
            red = 0.2f + this.transform.position.y * (0.8f / RevolutionRadius);
            yellow = Ybasis + this.transform.position.y * tAdder;
        }
                                
        SunLight.light.color = new Color(red, yellow, 0.0f);
        Debug.Log(SunLight.light.color);
    }

    void OnGUI()
    {
        //GUI.Label (new Rect (800, 5, 30, 30), this.transform.parent.gameObjeparent.ToString(), this.textStyle);
    }
}
