using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Fate {

	public class ScenarioIntro : Scenario {

		bool truckFueled = false;
		bool fuelPaid = false;

		public  GameObject textPanel;
		public  GameObject responsePanel;



		public Camera startCamera;
		public Camera gameCamera;
		public Camera sideCamera;
		public GameObject hero;
		public GameObject truck;
		// Use this for initialization
		float lifetime = 0;


		void Start () {
			hero.SetActive (false);
			foreach (var go in GameObject.FindGameObjectsWithTag ("Selection")) {
				go.GetComponent<MeshRenderer> ().enabled = false;
			}
		}
		
		// Update is called once per frame
		void Update () {
			lifetime += Time.deltaTime;

			switch (state) {
			case State.Intro:
				if (lifetime > 3.5) {
					if (!truck.GetComponent<Animation>().isPlaying)
						truck.GetComponent<Animation> ().Play ();
				}
				if (lifetime > 8) {
					startCamera.gameObject.SetActive(false);
					gameCamera.gameObject.SetActive (true);
					//sideCamera.gameObject.SetActive (true);
					state = State.Playing;
					hero.SetActive (true);
				}
				break;
			case State.Playing:
				break;
			}
		}

		public override void OnHeroEnterZone(string name) 
		{
			if (name == "ZoneSideCamera") {
				gameCamera.gameObject.SetActive (false);
				sideCamera.gameObject.SetActive (true);
			}
			if (name == "ZoneGameCamera") {
				gameCamera.gameObject.SetActive (true);
				sideCamera.gameObject.SetActive (false);
			}

		}

		GameObject fuelGuy;

		public override void OnAction(Interactable.Action action, GameObject obj)
		{
			Debug.Log ("doing " + action.ToString() + " on "+ obj.name);			
			if (obj.name == "Truck") {
				if (action == Interactable.Action.Look) {
					SayToSelf (locale.GetRandomText("intro.my_truck",3));
				}
				if (action == Interactable.Action.EnterTruck) {
					if (!truckFueled) {
						SayToSelf (locale.GetText ("intro.truck.fuel_first"));
					}
					else if (!fuelPaid) {
						SayToSelf (locale.GetText ("intro.truck.pay_first"));
					}
				}
			}
			if (obj.name == "FuelPumps") {
				if (action == Interactable.Action.Look) {
					SayToSelf (locale.GetRandomText ("intro.pump_look", 2));
				}
			}
			if (obj.name == "FuelGuy") {
				if (action == Interactable.Action.Look) {
					SayToSelf (locale.GetRandomText ("intro.fuel_guy_look",1));
				}
				if (action == Interactable.Action.Use) {
					SayToSelf (locale.GetRandomText ("intro.fuel_guy_use", 3));
				}
				if (action == Interactable.Action.Talk) {
					Say(locale.GetText ("intro.fuel_guy_talk"));
					fuelGuy = obj;
					Invoke ("FuelGuyGo", 0.5f);
				}
			}
			if (obj.name == "WallBox") {
				if (action == Interactable.Action.Look) {
					SayToSelf (locale.GetRandomText ("intro.box_look",2));
				}				
				if (action == Interactable.Action.Use) {
					foreach (var m in obj.transform.gameObject.GetComponentsInChildren<MeshRenderer>()) {
						if (m.gameObject.tag == "Selection") {
							m.enabled = false;
						}
					}					
					obj.GetComponentInChildren<ParticleSystem> ().Play ();
					hero.GetComponent<Animator> ().SetBool("Death_b", true);
					hero.GetComponent<NavMeshAgent> ().updateRotation = false;

					Vector3 dir = 
						hero.GetComponent<NavMeshAgent> ().destination - obj.transform.position;
					dir.Normalize ();
					hero.GetComponent<NavMeshAgent> ().destination = 
						hero.GetComponent<NavMeshAgent> ().destination + dir * 1.5f;

					state = State.Death;
				}
			}
		}

		void FuelGuyGo()
		{
			Response ("Ok!");
			foreach (var m in fuelGuy.transform.gameObject.GetComponentsInChildren<MeshRenderer>()) {
				if (m.gameObject.tag == "Selection") {
					m.enabled = false;
				}
			}
			fuelGuy.GetComponent<NavMeshObstacle> ().enabled = false;
			fuelGuy.GetComponent<NavMeshAgent> ().enabled = true;
			fuelGuy.GetComponent<NavMeshAgent> ().destination = GameObject.Find("FuelGuyPos").transform.position;
			fuelGuy.GetComponent<CapsuleCollider> ().enabled = false;
			fuelGuy.GetComponent<Animator> ().SetFloat ("Speed_f", 3.0f);
			fuelGuy.GetComponent<Animator> ().SetFloat ("Anim_Speed_f", 3.0f);

			truckFueled = true;
		}

		void HideTextPanel()
		{
			textPanel.SetActive (false);
		}

		void HideResponsePanel()
		{
			responsePanel.SetActive (false);
		}

		void SayToSelf(string s)
		{
			if (textPanel.activeSelf) {
				CancelInvoke ();
			}
			textPanel.SetActive (true);
			textPanel.GetComponentInChildren<Text>().text = "<color=#ffffff>"+s+"</color>";
			Invoke ("HideTextPanel", 5);
		}

		void Say(string s)
		{
			if (textPanel.activeSelf) {
				CancelInvoke ();
			}
			textPanel.SetActive (true);
			textPanel.GetComponentInChildren<Text>().text = "<color=#ffff00>"+s+"</color>";
			Invoke ("HideTextPanel", 5);
		}

		void Response(string s)
		{
			if (responsePanel.activeSelf) {
				CancelInvoke ();
			}
			responsePanel.SetActive (true);
			responsePanel.GetComponentInChildren<Text>().text = "<color=#ffbb00>"+s+"</color>";
			Invoke ("HideResponsePanel", 5);
		}

	}

}