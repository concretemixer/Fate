using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Fate {

	public class ScenarioIntro : Scenario {

		bool truckFueled = false;
		bool fuelPaid = false;
	


		public Camera startCamera;
		public Camera gameCamera;
		public Camera sideCamera;
        public Camera shopCamera;
		public Camera shopSecurityCamera;

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
            case State.Outro:
                if (lifetime > 4)
                {
                    SceneManager.LoadScene("EastGate");
                }
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
            if (name == "ZoneShopExit")
            {
                gameCamera.gameObject.SetActive(true);
                shopCamera.gameObject.SetActive(false);

                hero.GetComponent<NavMeshAgent>().enabled = false;
                hero.transform.position = GameObject.Find("ShopExitStart").transform.position;
                hero.GetComponent<NavMeshAgent>().enabled = true;
                hero.GetComponent<NavMeshAgent>().destination = GameObject.Find("ShopExitFinish").transform.position;     

            }
            if (name == "ZoneTruckKill")
            {
                GameObject.Find("DeathTruck").GetComponent<Animation>().Play();
            }
            if (name == "DeathTruck")
            {
                //hero.GetComponent<Animator>().SetBool("Death_b",true);
                hero.SetActive(false);
                GameObject.Find("BloodSpill").transform.position = hero.transform.position;
                GameObject.Find("BloodSpill").GetComponentInChildren<ParticleSystem>().Play();
            }
		}

		GameObject fuelGuy;

		public override void OnAction(Interactable.Action action, GameObject obj)
		{
			//Debug.Log ("doing " + action.ToString() + " on "+ obj.name);			
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
					else {
						SayToSelf (locale.GetText ("intro.truck.go"));
						startCamera.gameObject.SetActive(true);
						gameCamera.gameObject.SetActive (false);
						state = State.Outro;
						hero.SetActive (false);
						hero.GetComponent<Hero> ().Deselect ();

						truck.GetComponent<Animation> ().Play ("leave_gas_station");
                        lifetime = 0;
					}
				}
			}
			if (obj.name == "SecurityMonitor") {
				if (action == Interactable.Action.Look) {
					SayToSelf (locale.GetRandomText ("intro.sec_look", 2));
				}
				if (action == Interactable.Action.LookClose) {
					shopCamera.gameObject.SetActive(false);
					shopSecurityCamera.gameObject.SetActive (true);
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
            if (obj.name == "Dumpster")
            {
                if (action == Interactable.Action.Look)
                {
                    SayToSelf(locale.GetRandomText("intro.dumpster_look", 2));
                }
                if (action == Interactable.Action.Use)
                {
                    SayToSelf(locale.GetRandomText("intro.dumpster_use", 2));
                }
            }
            if (obj.name == "FuelStation")
            {
                if (action == Interactable.Action.Look)
                {
                    if (!truckFueled)
                        SayToSelf(locale.GetText("intro.shop_look"));
                    else if (!fuelPaid)
                        SayToSelf(locale.GetText("intro.shop_look_pay"));
                    else
                        SayToSelf(locale.GetText("intro.shop_look_leave"));
                }
                if (action == Interactable.Action.Enter)
                {
                    shopCamera.gameObject.SetActive(true);
                    gameCamera.gameObject.SetActive(false);

                    hero.GetComponent<NavMeshAgent>().enabled = false;
                    hero.transform.position = GameObject.Find("ShopEnterStart").transform.position;
                    hero.GetComponent<NavMeshAgent>().enabled = true;
                    hero.GetComponent<NavMeshAgent>().destination = GameObject.Find("ShopEnterFinish").transform.position;    

					if (fuelGuy != null) {
						fuelGuy.GetComponent<Animator> ().SetFloat ("Speed_f", 0.0f);
						fuelGuy.GetComponent<Animator> ().SetFloat ("Anim_Speed_f", 0.5f);
					}
                }
            }
            if (obj.name == "WallBox")
            {
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
            if (obj.name == "Diesel")
            {
                if (action == Interactable.Action.Look)
                {
                    SayToSelf(locale.GetRandomText("intro.diesel_look", 2));
                }
            }
            if (obj.name == "CounterGirl")
            {
                if (action == Interactable.Action.Look)
                {
                    SayToSelf(locale.GetRandomText("intro.girl_look", 3));
                }
                if (action == Interactable.Action.Talk)
                {
                    Say(locale.GetText("intro.girl_talk"));
                }

				if (action == Interactable.Action.Use) {
					if (!truckFueled)
						Response (locale.GetText ("intro.girl_answer"));
					else if (!fuelPaid) {
						Response (locale.GetText ("intro.girl_pay"));
						fuelPaid = true;
					}
					else
						Response (locale.GetText ("intro.girl_else"));
				}
            }
		}

		void FuelGuyGo()
		{
			Response (locale.GetText ("intro.fuel_guy_talk_ok"));
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


	}

}