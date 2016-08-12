using UnityEngine;
using System.Collections;

namespace Fate {
    public class ScenarioMainSquare : Scenario {

        public Camera squareCamera;

    	// Use this for initialization
    	void Start () {
            state = State.Playing;
            foreach (var go in GameObject.FindGameObjectsWithTag("MainCamera"))
                go.SetActive(false);
            squareCamera.gameObject.SetActive(true);
        }
    	
    	// Update is called once per frame
    	void Update () {
    	
    	}

        public override void OnHeroEnterZone(string name)
        {
            base.OnHeroEnterZone(name);
        }
    }
}
