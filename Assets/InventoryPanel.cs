using UnityEngine;
using System.Collections;

public class InventoryPanel : MonoBehaviour {

    GameObject selected = null;
    GameObject taken = null;
    float takeTimer = -1;
    float takeDelay = -1;

    float baseScale = 1;

    const float inTime = 0.3f;
    const float showTime = 1f;
    const float outTime = 0.3f;

    Vector3 itemPointScreen;
    public GameObject itemAnchor;
    public GameObject takeAnchor;
	// Use this for initialization
	void Start () {
        itemPointScreen = new Vector3(Screen.width * 0.06f, Screen.height * 0.92f, 0);
	}
	
	// Update is called once per frame
	void Update () {


        if (selected != null)
        {
            Vector3[] corners = new Vector3[4];
            itemAnchor.GetComponent<RectTransform>().GetWorldCorners(corners);
            Vector3 itemPoint;

            itemPoint.x = (corners[0].x + corners[1].x + corners[2].x + corners[3].x) / 4;
            itemPoint.y = (corners[0].y + corners[1].y + corners[2].y + corners[3].y) / 4;
            itemPoint.z = (corners[0].z + corners[1].z + corners[2].z + corners[3].z) / 4;

            selected.transform.position = itemPoint;
            selected.transform.Rotate(0, Time.deltaTime*30, 0);
        }
        if (taken != null)
        {
            Vector3[] corners = new Vector3[4];
            takeAnchor.GetComponent<RectTransform>().GetWorldCorners(corners);
            Vector3 itemPoint;

            itemPoint.x = (corners[0].x + corners[1].x + corners[2].x + corners[3].x) / 4;
            itemPoint.y = (corners[0].y + corners[1].y + corners[2].y + corners[3].y) / 4;
            itemPoint.z = (corners[0].z + corners[1].z + corners[2].z + corners[3].z) / 4;

            taken.transform.Rotate(0, Time.deltaTime*60, 0);            

            if (takeDelay >= 0)
            {
                takeDelay -= Time.deltaTime;
                if (takeDelay < 0)
                    taken.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                takeTimer -= Time.deltaTime;
                if (takeTimer < 0)
                {
                    taken.transform.localScale = new Vector3(baseScale,baseScale,baseScale);
                    taken.GetComponent<MeshRenderer>().enabled = false;
                    taken = null;
                }
                else {
                    if (takeTimer > outTime + showTime)
                    {
                        float scale = baseScale * Mathf.Lerp(2, 1, (takeTimer - (outTime + showTime)) / inTime);
                        taken.transform.localScale = new Vector3(scale, scale, scale);
                    }
                    else if (takeTimer < outTime)
                    {
                        float scale = baseScale * Mathf.Lerp(0, 2, takeTimer / outTime);
                        taken.transform.localScale = new Vector3(scale, scale, scale);

                        takeAnchor.GetComponentInParent<Canvas>().GetComponent<RectTransform>().GetWorldCorners(corners);
                        itemPoint = Vector3.Lerp(corners[2], itemPoint, takeTimer / outTime);
                    }
                    taken.transform.position = itemPoint;
                }

               
            }

           
        }
	}

    public void OnSelectItem(string item)
    {
        if (string.IsNullOrEmpty(item))
        {
            if (selected != null)
                selected.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            if (selected != null)
                selected.GetComponent<MeshRenderer>().enabled = false;
            GameObject go = GameObject.Find("ItemTemplate_" + item);         
            selected = go;
            selected.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void OnTakeItem(string item, float delay)
    {
        if (taken != null)
            taken.GetComponent<MeshRenderer>().enabled = false;
        GameObject go = GameObject.Find("ItemTemplate_" + item);         
        taken = go;
        taken.GetComponent<MeshRenderer>().enabled = delay <= 0; 
        baseScale = taken.transform.localScale.x;
        takeTimer = inTime+showTime+outTime;
        takeDelay = delay;
    }
}
