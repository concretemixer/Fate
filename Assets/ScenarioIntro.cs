using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Fate {

	public class ScenarioIntro : Scenario {

		bool truckFueled = false;
        bool pinKnown = false;
		bool fuelPaid = false;
        bool bikerLeft = false;
        bool camerasDisabled = false;
        bool vendingBroken = false;

        int pinFailCount = 0;
	


		public Camera startCamera;
		public Camera gameCamera;
		public Camera sideCamera;
        public Camera shopCamera;
		public Camera shopSecurityCamera;
        public Camera shopPinCamera;

        public GameObject hero;

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
                        if (!GameObject.Find("TruckAnimRoot").GetComponent<Animation>().isPlaying)
                            GameObject.Find("TruckAnimRoot").GetComponent<Animation> ().Play ();
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

                if (truckFueled)
                {
                    GameObject.Find("Motorbike").GetComponent<Animation>().Play();
                    GameObject.Find("Biker").transform.localPosition = Vector3.zero;
                    GameObject.Find("Biker").GetComponent<Animator>().SetTrigger("Ride_t");
                    bikerLeft = true;
                }

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
        string pinCode;

        public override bool OnActionIntended(Interactable.Action action, GameObject obj)
        {
            if (obj.name == "FuelPumps")
            {
                if (action == Interactable.Action.Look)
                {
                    SayToSelf(locale.GetRandomText("intro.pump_look", 2));
                    return false;
                }
            }
            if (obj.name == "Truck")
            {
                if (action == Interactable.Action.Look)
                {
                    SayToSelf(locale.GetRandomText("intro.my_truck", 3));
                    return false;
                }
                if (action == Interactable.Action.EnterTruck)
                {
                    if (!truckFueled)
                    {
                        SayToSelf(locale.GetText("intro.truck.fuel_first"));
                        return false;
                    }
                    else if (!fuelPaid)
                    {
                        SayToSelf(locale.GetText("intro.truck.pay_first"));
                        return false;
                    }
                    
                }

            }
            if (obj.name == "FuelGuy")
            {
                if (action == Interactable.Action.Look)
                {
                    SayToSelf(locale.GetRandomText("intro.fuel_guy_look", 1));
                    return false;
                }
                if (action == Interactable.Action.Use)
                {
                    SayToSelf(locale.GetRandomText("intro.fuel_guy_use", 3));
                    return false;
                }
            }
            if (obj.name == "Biker")
            {
                if (action == Interactable.Action.Look)
                {
                    SayToSelf(locale.GetRandomText("intro.biker_look", 1));
                    return false;
                }
            }
            if (obj.name == "Dumpster")
            {
                if (action == Interactable.Action.Look)
                {
                    SayToSelf(locale.GetRandomText("intro.dumpster_look", 2));
                    return false;
                }
            }
            if (obj.name.StartsWith("SecurityCamera"))
            {
                if (action == Interactable.Action.Look)
                {
                    SayToSelf(locale.GetRandomText("intro.camera_look", 2));
                    return false;
                }
            }
            if (obj.name == "FireExtinguisher")
            {
                if (action == Interactable.Action.Look)
                {
                    SayToSelf(locale.GetRandomText("intro.extinguisher_look", 2));
                    return false;
                }
            }
            if (obj.name == "SecurityMonitor")
            {
                if (action == Interactable.Action.Look)
                {
                    SayToSelf(locale.GetRandomText("intro.sec_look", 2));
                    return false;
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
            }
            if (obj.name == "VendingMachine")
            {
                if (action == Interactable.Action.Look) {
                    if (vendingBroken)
                        SayToSelf(locale.GetText("intro.vending_look"));
                    else
                        SayToSelf(locale.GetText("intro.vending_bloken"));
                }
                if (action == Interactable.Action.Use)
                {
                    if (hero.GetComponent<Hero>().Tool == "credit_card")
                    {
                        SayToSelf(locale.GetText("intro.vending_cash"));
                        return false;
                    }
                    if (hero.GetComponent<Hero>().Tool == "crowbar" && !camerasDisabled)
                    {
                        SayToSelf(locale.GetText("intro.vending_cameras"));
                        return false;
                    }
                    
                }
            }

            if (obj.name == "Dumpster")
            {
                if (action == Interactable.Action.Use)
                {
                    if (string.IsNullOrEmpty(hero.GetComponent<Hero>().Tool))
                    {
                        if (pinFailCount == 0)
                        {
                            SayToSelf(locale.GetRandomText("intro.dumpster_use", 2));
                            return false;
                        }
                    }
                    else
                    {
                        SayToSelf(locale.GetRandomText("intro.dumpster_no_waste", 2));
                        return false;
                    }
                }
            }
            if (obj.name == "WallBox")
            {
                if (action == Interactable.Action.Look)
                {
                    if (camerasDisabled)
                        SayToSelf(locale.GetRandomText("intro.box_look_broken", 2));
                    else
                        SayToSelf(locale.GetRandomText("intro.box_look", 2));
                }   
            }
            if (action == Interactable.Action.Look)
                return false;

            return true;
        }

		public override void OnAction(Interactable.Action action, GameObject obj)
		{
			//Debug.Log ("doing " + action.ToString() + " on "+ obj.name);			
            if (obj.name.StartsWith("Pin_") && shopPinCamera.isActiveAndEnabled)
            {
                string digit = obj.name.Replace("Pin_","")+"... ";
                pinCode += digit;
                SayToSelf(pinCode);
                string pin = pinCode.Replace(".","").Replace(" ", "");

                if (pin.Length == 4)
                {
                    SayToSelf(pinCode + "[Enter]");

                    if (pinKnown && pin == "5803")
                    {                        
                        conversation.StartDialog("intro.girl.pin_good");
                        state = Scenario.State.Interlude;                                                                  
                        fuelPaid = true;
                    }
                    else
                    {
                        conversation.StartDialog("intro.girl.pin_forgot");
                        state = Scenario.State.Interlude;                                                                  
                        pinFailCount++;
                    }

                    shopCamera.gameObject.SetActive(true);
                    shopPinCamera.gameObject.SetActive(false);
                }
            }
			if (obj.name == "Truck") {
				
				if (action == Interactable.Action.EnterTruck) {
					if (truckFueled && fuelPaid) {
					
						SayToSelf (locale.GetText ("intro.truck.go"));
						startCamera.gameObject.SetActive(true);
						gameCamera.gameObject.SetActive (false);
						state = State.Outro;
						hero.SetActive (false);
						hero.GetComponent<Hero> ().Deselect ();

                        GameObject.Find("TruckAnimRoot").GetComponent<Animation> ().Play ("leave_gas_station2");
                        lifetime = 0;
					}
				}
			}
			if (obj.name == "SecurityMonitor") {

				if (action == Interactable.Action.LookClose) {
					shopCamera.gameObject.SetActive(false);
					shopSecurityCamera.gameObject.SetActive (true);
                //    shopPinCamera.gameObject.SetActive(true);
                  //  pinCode = "";
				}
			}

			if (obj.name == "FuelGuy") {

				if (action == Interactable.Action.Talk) {
				//	Say(locale.GetText ("intro.fuel_guy_talk"));
					fuelGuy = obj;
					//Invoke ("FuelGuyGo", 0.5f);

                    conversation.StartDialog("intro.fuel_guy.start");
					state = Scenario.State.Interlude;
				}
			}
			if (obj.name == "Biker") {
				if (action == Interactable.Action.Talk) {
					conversation.StartDialog("intro.biker.start");
					state = Scenario.State.Interlude;
				}
			}
            if (obj.name == "Dumpster")
            {

                if (action == Interactable.Action.Use)
                {
                    hero.GetComponent<Animator>().SetBool("Give_t", true);
                    hero.GetComponent<Hero>().TakeItem("crowbar");                    
                }
            }

            
            if (obj.name == "MonitorWall")
            {
                if (action == Interactable.Action.Exit)
                {
                    shopCamera.gameObject.SetActive(true);
                    shopSecurityCamera.gameObject.SetActive(false);

                    if (bikerLeft)
                        pinKnown = true;
                }
            }
            if (obj.name == "FuelStation")
            {
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

				if (action == Interactable.Action.Use) {

                    if (hero.GetComponent<Hero>().Tool == "drink")
                    {
                        GameObject drink = GameObject.Find("ItemTemplate_drink");
                        GameObject.Find("Hero_Item1").GetComponent<SkinnedMeshRenderer>().sharedMesh = drink.GetComponent<MeshFilter>().mesh;


                        hero.GetComponent<Animator>().SetBool("Pour_t", true);
                        foreach (var c in obj.GetComponentsInChildren<ParticleSystem>())
                        {
                            if (c.gameObject.name == "Smoke" || c.gameObject.name == "Spark2" || c.gameObject.name == "Spark3")
                                c.Play();
                        }
                            
                        if (!camerasDisabled)
                        {
                            obj.GetComponent<Interactable>().defaultAction = Interactable.Action.Look;
                            camerasDisabled = true;
                            GameObject.Find("SecurityMonitor").GetComponent<Interactable>().defaultAction = Interactable.Action.Look;
                            GameObject.Find("SecurityMonitorScreen").SetActive(false);
                            GameObject.Find("SecurityCamera").GetComponent<Animation>().Stop();
                            GameObject.Find("SecurityCamera2").GetComponent<Animation>().Play("camera_die");
                        }

                        hero.GetComponent<Hero>().RemoveItem("drink", true, 6);
                    }
                    else
                    {
                        foreach (var c in obj.GetComponentsInChildren<ParticleSystem>())
                        {
                            if (c.gameObject.name == "Spark1")
                                c.Play();
                        }
                        hero.GetComponent<Animator>().SetBool("Death_b", true);
                        hero.GetComponent<Animator>().SetTrigger("Give_t");
                        hero.GetComponent<NavMeshAgent>().updateRotation = false;
                        state = State.Death;
                    }
				}
			}
            if (obj.name == "VendingMachine")
            {
                if (action == Interactable.Action.Use)
                {
                    if (hero.GetComponent<Hero>().Tool == "cash")
                    {
                        hero.GetComponent<Animator>().SetBool("Give_t", true);
                        hero.GetComponent<Hero>().TakeItem("drink");
                    }
                    else if (hero.GetComponent<Hero>().Tool == "crowbar")
                    {
                        //obj.GetComponent<Animation>().Play();
                        hero.GetComponent<Animator>().SetBool("Open_t", true);
                        hero.GetComponent<Hero>().TakeItem("cash_big");
                        vendingBroken = true;
                        obj.GetComponent<Animation>().Play("break_vending");
                        //obj.transform.FindChild("Glass").GetComponent<MeshRenderer>().enabled = true;
                    }
                    else
                    {
                        obj.GetComponent<Animation>().Play();
                        hero.GetComponent<Animator>().SetBool("Kick_t", true);
                    }
                }
            }
            if (obj.name == "CounterGirl")
            {

                if (action == Interactable.Action.Talk)
                {
					conversation.StartDialog("intro.girl.pickup");
					state = Scenario.State.Interlude;
                }

				if (action == Interactable.Action.Use) {
                    if (!truckFueled)
                    {
                        conversation.StartDialog("intro.girl.not_fueled");
                        state = Scenario.State.Interlude;
                    }
                    else if (!fuelPaid)
                    {
                        string tool = hero.GetComponent<Hero>().Tool;
                        if (string.IsNullOrEmpty(tool))
                        {
                            conversation.StartDialog("intro.girl.fueled");
                            state = Scenario.State.Interlude;
                        }
                        else if (tool == "credit_card")
                        {
                            hero.GetComponent<Animator> ().SetBool("Give_t", true);
                            conversation.StartDialog("intro.girl.enter_pin");
                            state = Scenario.State.Interlude;
                        }
                        else if (tool == "cash")
                        {
                            hero.GetComponent<Animator> ().SetBool("Give_t", true);
                            conversation.StartDialog("intro.girl.no_cash");
                            state = Scenario.State.Interlude;
                        }
                        else if (tool == "cash_big")
                        {
                            hero.GetComponent<Animator>().SetBool("Give_t", true);
                            conversation.StartDialog("intro.girl.cash_ok");
                            state = Scenario.State.Interlude;
                            fuelPaid = true;
                        }
                        else
                        {
                            hero.GetComponent<Animator> ().SetBool("Give_t", true);
                            conversation.StartDialog("intro.girl.what");
                            state = Scenario.State.Interlude;
                        }

/*

*/
//						fuelPaid = true;
                    }
                    else
                    {
                        conversation.StartDialog("intro.girl.else");
                        state = Scenario.State.Interlude;                    
                    }
				}
            }
		}
			

		public override void OnConversationEnd(string id)
		{
			state = State.Playing;
			base.OnConversationEnd(id);
		}

		public override void OnConversationEvent(string id, string name) 
		{
			if (name == "intro.fuel_guy_leave") {
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
            if (name=="intro.enter_pin") {
                shopCamera.gameObject.SetActive(false);
                shopPinCamera.gameObject.SetActive(true);
                pinCode = "";                
            }
            if (name == "intro.girl_no_cash")
            {
                GameObject.Find("CounterGirl").GetComponent<Animator> ().SetBool("SayNo_t", true);
            }
            if (name == "intro.girl_yes_cash")
            {
                GameObject.Find("CounterGirl").GetComponent<Animator> ().SetBool("SayYes_t", true);
            }
		}

	}

}