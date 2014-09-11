using UnityEngine;
using System.Collections;

public class FurnitureMaker : MonoBehaviour
{

    public GameObject FurnitureMovingPad;
    private string selected_furniture;
    private ContentManager contentManager;

    // Use this for initialization
    int count = 0;

    public void Start()
    {
        selected_furniture = null;
        contentManager = ContentManager.getInstance();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (contentManager.Mode == ContentManager.MODE.FURNITURE_MODE)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject targets = GameObject.Find("Targets"); 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    if (hit.transform.name.Contains("furniture"))
                    {
						if(contentManager.Flag == 0){
	                        FurnitureMovingPad.SetActive(true);
	                        FurnitureMovingPad.GetComponent<FurnitureController>().selected_furniture = hit.transform.name;
	                        selected_furniture = hit.transform.name;
	                        GameObject.Find(selected_furniture).GetComponent<FurnitureCollider>().isMoving = true;
	                        setFurnitureLayer(GameObject.Find(selected_furniture), "FurnitureSelectedLayer");
	                        contentManager.Flag = 1;
						}
						else{
							contentManager.Flag = 0;
						}
                    }
                }
            }
        }
        if ((contentManager.Flag == 0 || 
            contentManager.Mode != ContentManager.MODE.FURNITURE_MODE) && 
            selected_furniture != null)
        {
            GameObject.Find(selected_furniture).GetComponent<FurnitureCollider>().isMoving = false;
            setFurnitureLayer(GameObject.Find(selected_furniture), "Default");
            selected_furniture = null;
        }
    }

    void setFurnitureLayer(GameObject n_furniture, string layer)
    {
        foreach (Transform child in n_furniture.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer(layer);
        }
    }
}
