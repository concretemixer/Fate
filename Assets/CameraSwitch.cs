using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

    public Camera onCamera;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnHeroEnterZone(string name)
    {
        if (name == gameObject.name)
        {
            foreach (var go in GameObject.FindGameObjectsWithTag("MainCamera"))
                go.SetActive(false);

            onCamera.gameObject.SetActive(true);

            GameObject canvas = GameObject.Find("Canvas");
            if (canvas!=null)
                canvas.GetComponent<Canvas>().worldCamera = onCamera;
            
        }
    }
}
