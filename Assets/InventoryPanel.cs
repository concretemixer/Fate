using UnityEngine;
using System.Collections;

public class InventoryPanel : MonoBehaviour {

    GameObject selected = null;

    Vector3 itemPointScreen;
	// Use this for initialization
	void Start () {
        itemPointScreen = new Vector3(Screen.width * 0.06f, Screen.height * 0.92f, 0);
	}
	
	// Update is called once per frame
	void Update () {

        if (selected != null)
        {
            Vector3 itemPoint;
            Ray ray = Camera.main.ScreenPointToRay(itemPointScreen);
            itemPoint = ray.origin + ray.direction * 8;
            selected.transform.position = itemPoint;
            selected.transform.Rotate(0, Time.deltaTime*30, 0);
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
}
