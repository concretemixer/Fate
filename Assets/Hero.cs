using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.IO;

namespace Fate {
	
	public class Hero : MonoBehaviour {

		Plane ground = new Plane(Vector3.up, Vector3.zero);

		GameObject selected = null;
		Interactable.Action intendedAction = Interactable.Action.None;
        Quaternion intendedRotation;

        float destroyTimer=0;
        string tool = "";

        public string Tool
        {
            get
            {
                return tool;
            }
            private set
            {
                tool = value;
            }
        }

		public Scenario scenario;
		public  GameObject actionsPanel;
        public  GameObject inventoryList;

        System.Collections.Generic.List<string> inventory =  new System.Collections.Generic.List<string>() { "none", "cash","credit_card" };
		// Use this for initialization


		void Start() {
            UpdateInventory();
		}

        void UpdateInventory()
        {
            inventoryList.GetComponent<Dropdown>().ClearOptions();
            inventoryList.GetComponent<Dropdown>().AddOptions( inventory );
            foreach(var s in inventory) {
                if (GameObject.Find("ItemTemplate_" + s)==null)
                {
                    Object go = Resources.Load("Items/" + s); 
                    if (go != null)
                    {
                        GameObject g = GameObject.Instantiate(go, new Vector3(1000, 1000, 1000), Quaternion.identity) as GameObject;
                        g.name = "ItemTemplate_" + s;
                    }               
                }
            }
        }

		int shotNum = 0;

		public void Deselect()
		{
			foreach (var go in GameObject.FindGameObjectsWithTag ("Selection")) {
				go.GetComponent<MeshRenderer> ().enabled = false;
			}			
		}

		Vector3 touchGround;
		// Update is called once per frame
		void Update () {

            destroyTimer -= Time.deltaTime;
            if (destroyTimer<0)
            {
                if (GameObject.Find("Hero_Item1")!=null)
                    GameObject.Find("Hero_Item1").GetComponent<SkinnedMeshRenderer>().sharedMesh = null;
                if (GameObject.Find("Hero_Item2")!=null)
                    GameObject.Find("Hero_Item2").GetComponent<SkinnedMeshRenderer>().sharedMesh = null;
            }


			if (intendedAction != Interactable.Action.None) 
            {
                Vector3 d = GetComponent<NavMeshAgent> ().destination - transform.position;
                d.y = 0;
				//Debug.Log ("d =" + d);
                if (d.magnitude < 0.25f) 
                {




                    if (selected != null)
                    {
                        transform.rotation = intendedRotation;
                        scenario.OnAction(intendedAction, selected);
                    }
                    intendedAction = Interactable.Action.None;                  
				}
			}

			if (Input.GetKeyDown(KeyCode.S))
			{

				string filename = @"d:\--\shot" + shotNum.ToString() + ".png";
				while (File.Exists(filename))
				{
					shotNum++;
					filename = @"d:\--\shot" + shotNum.ToString() + ".png";                    
				}

				Application.CaptureScreenshot(filename);
				shotNum++;
			}


			if (Input.GetMouseButtonDown(0) && scenario.state==Scenario.State.Playing)
			{
				if(EventSystem.current.IsPointerOverGameObject())
					return;

				intendedAction = Interactable.Action.None;

				Camera camera = Camera.main;

				Ray ray = camera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				bool hitOk = false;
				if (Physics.Raycast (ray, out hit)) {
					//Debug.Log (hit.transform.gameObject.name);
					if (hit.transform.gameObject.tag == "Interactable") {
						hitOk = true;
						foreach (var go in GameObject.FindGameObjectsWithTag ("Selection")) {
							go.GetComponent<MeshRenderer> ().enabled = false;
						}					
						foreach (var m in hit.transform.gameObject.GetComponentsInChildren<MeshRenderer>()) {
							if (m.gameObject.tag == "Selection") {
								m.enabled = true;								
							}
						}

                        selected = hit.transform.gameObject;

						foreach (Transform child in hit.transform) {
							if (child.gameObject.tag == "Respawn") {
								//GetComponent<NavMeshAgent> ().destination = child.transform.position;
								GetComponent<NavMeshAgent> ().destination = transform.position;
                                GetComponent<NavMeshAgent>().autoBraking = true;
							}
						}

						float d = (GetComponent<NavMeshAgent> ().destination - transform.position).magnitude;
						GetComponent<NavMeshAgent> ().speed = d > 10 ? 5 : 2;
					} 
					else if (hit.transform.gameObject.tag == "CameraHelper") {
					}
					else {
						selected = null;
						hitOk = true;
					}
				} 
				if (!hitOk){
					float rayDistance;
					if (ground.Raycast (ray, out rayDistance)) {
						touchGround = ray.origin + ray.direction * rayDistance;
						GetComponent<NavMeshAgent> ().destination = touchGround;
                        GetComponent<NavMeshAgent>().autoBraking = true;
						hitOk = true;

						foreach (var go in GameObject.FindGameObjectsWithTag ("Selection")) {
							go.GetComponent<MeshRenderer> ().enabled = false;					
						}

					}
					selected = null;
				}

				if (hitOk) {
					float d = (GetComponent<NavMeshAgent> ().destination - transform.position).magnitude;
					GetComponent<NavMeshAgent> ().speed = d > 10 ? 5 : 2;
				}

				if (selected != null) {

                    if (selected.GetComponent<Interactable>().defaultAction != Interactable.Action.None)
                    {
                        actionsPanel.SetActive(false);
                        OnActionSelected(selected.GetComponent<Interactable>().defaultAction);
                    }
                    else
                    {
                        actionsPanel.SetActive(true);
                        actionsPanel.GetComponent<ActionsPanel>().ShowOn(selected);
                    }
				} else {
					actionsPanel.SetActive (false);
				}

			}

			float speed = GetComponent<NavMeshAgent> ().velocity.magnitude;
			GetComponent<Animator> ().SetFloat ("Speed_f", speed);
			GetComponent<Animator> ().SetFloat ("Anim_Speed_f", speed > 0.5f ? speed : 0.5f);


		}

		void OnTriggerEnter(Collider other) {
			scenario.OnHeroEnterZone (other.gameObject.name);
		}


        public void OnActionSelected(Interactable.Action act)
        {            
            Debug.Log("intended " + act.ToString() + " on " + selected.name);

            actionsPanel.SetActive(false);

            if (!scenario.OnActionIntended(act, selected))
                return;

            GetComponent<NavMeshAgent>().destination = transform.position;

            foreach (Transform child in selected.transform)
            {
                if (child.gameObject.tag == "Respawn")
                {
                    GetComponent<NavMeshAgent>().destination = child.transform.position;// -child.transform.forward * child.transform.localScale.x;
                    intendedRotation = child.transform.rotation;
                }
                if (child.gameObject.tag == "Selection")
                {
                    child.GetComponent<MeshRenderer>().enabled = false;
                }

            }

            float d = (GetComponent<NavMeshAgent>().destination - transform.position).magnitude;
            GetComponent<NavMeshAgent>().speed = d > 10 ? 5 : 2;
            intendedAction = act;

        }            	

        public void OnInventorySelected()
        {
            if (inventoryList.GetComponent<Dropdown>().value == 0)
                tool = null;
            else
                tool = inventoryList.GetComponent<Dropdown>().options[inventoryList.GetComponent<Dropdown>().value].text;
        }

        public void TakeItem(string item) 
        {
            inventory.Add(item);
            UpdateInventory();
        }

        public void RemoveItem(string item, bool destroy, float destroyTimer)
        {
            if (destroy)
            {
                inventory.Remove(item);
            }
            this.destroyTimer = destroyTimer;
            UpdateInventory();
        }
	}

}