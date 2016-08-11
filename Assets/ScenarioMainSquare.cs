using UnityEngine;
using System.Collections;

namespace Fate {
    public class ScenarioMainSquare : Scenario {

        public Camera squareCamera;
        public Camera railsCamera;
        public Camera cinemaCamera;

    	// Use this for initialization
    	void Start () {
            state = State.Playing;
            squareCamera.gameObject.SetActive(true);
            railsCamera.gameObject.SetActive(false);
            cinemaCamera.gameObject.SetActive(false);
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
