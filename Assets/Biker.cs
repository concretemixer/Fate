using UnityEngine;
using System.Collections;

public class Biker : MonoBehaviour {

    float smokeTimer = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        smokeTimer -= Time.deltaTime;
        if (smokeTimer < 0)
        {
            GetComponent<Animator>().SetTrigger("Smoke_t");
            smokeTimer = Random.Range(10.0f, 20.0f);
        }
	}
}
