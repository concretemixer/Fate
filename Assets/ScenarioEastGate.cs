using UnityEngine;
using System.Collections;

namespace Fate {
	public class ScenarioEastGate : Scenario {

		public Camera introCamera;
		public Camera gameCamera;
		public Camera copCamera;


		// Use this for initialization
		void Start () {
			state = State.Playing;
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public override void OnHeroEnterZone(string name) 
		{
			if (name == "ZoneApproachCops") {
				gameCamera.gameObject.SetActive (false);
				copCamera.gameObject.SetActive (true);
			}
			if (name == "ZoneLeaveCops") {
				gameCamera.gameObject.SetActive (true);
				copCamera.gameObject.SetActive (false);
			}
		}
	}
}
