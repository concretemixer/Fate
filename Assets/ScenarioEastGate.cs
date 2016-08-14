using UnityEngine;
using System.Collections;

namespace Fate {
	public class ScenarioEastGate : Scenario {

        public Camera sideCamera;
        public Camera barCamera;

        public Light mainLight;
        public Light barLight;

        public GameObject hero;

        bool night = false;

		// Use this for initialization
		void Start () {
			state = State.Playing;
		}
		
		// Update is called once per frame
		void Update () {
            if (Input.GetKeyDown(KeyCode.N))
            {
                night = !night;
                foreach (var go in GameObject.FindGameObjectsWithTag("Night"))
                {
                    Light light = go.GetComponent<Light>();
                    if (light != null)
                    {
                        if (light.type == LightType.Point)
                            light.enabled = night;
                        if (light.type == LightType.Directional)
                            light.enabled = !night;
                    }
                    MeshRenderer mr= go.GetComponent<MeshRenderer>();
                    if (mr != null)
                        mr.enabled = night;

                    if (go.GetComponent<NavMeshObstacle>() != null)
                    {
                        Vector3 p = go.transform.position;
                        p.y = night ? -10 : 0;
                        go.transform.position = p;
                    }
                }
                RenderSettings.ambientIntensity = night ? 0.8f : 1.5f;
            }		
		}

		public override void OnHeroEnterZone(string name) 
		{
			if (name == "ZoneLeaveBar") {
                LeaveBar(true);
			}
            base.OnHeroEnterZone(name);
		}

        private void LeaveBar(bool leave)
        {
            sideCamera.gameObject.SetActive (leave);
            barCamera.gameObject.SetActive (!leave);

            barLight.enabled = !leave;
            mainLight.enabled = !night && leave;

            if (leave)
            {
                hero.GetComponent<NavMeshAgent>().enabled = false;
                hero.transform.position = GameObject.Find("BarExitStart").transform.position;
                hero.GetComponent<NavMeshAgent>().enabled = true;
                hero.GetComponent<NavMeshAgent>().destination = GameObject.Find("BarExitFinish").transform.position;            }
            else
            {
                hero.GetComponent<NavMeshAgent>().enabled = false;
                hero.transform.position = GameObject.Find("BarEnterStart").transform.position;
                hero.GetComponent<NavMeshAgent>().enabled = true;
                hero.GetComponent<NavMeshAgent>().destination = GameObject.Find("BarEnterFinish").transform.position;    
            }
        }

        public override void OnAction(Interactable.Action action, GameObject obj)
        {
            //Debug.Log ("doing " + action.ToString() + " on "+ obj.name);          
            if (obj.name == "Bar")
            {
                if (action==Interactable.Action.Enter)
                    LeaveBar(false);
            }
        }
	}
}
