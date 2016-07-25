﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.IO;

namespace Fate {
	
	public class Hero : MonoBehaviour {

		Plane ground = new Plane(Vector3.up, Vector3.zero);

		GameObject selected = null;
		Interactable.Action intendedAction = Interactable.Action.None;

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
		// Use this for initialization


		void Start () {
            inventoryList.GetComponent<Dropdown>().ClearOptions();
            inventoryList.GetComponent<Dropdown>().AddOptions(
                new System.Collections.Generic.List<string>() { "none", "cash","credit_card" }
            );
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



			if (intendedAction != Interactable.Action.None) {
				float d = (GetComponent<NavMeshAgent> ().destination - transform.position).magnitude;
				//Debug.Log ("d =" + d);
				if (d < 0.25) {
					if (selected != null) {
						scenario.OnAction (intendedAction, selected);
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

                        Vector3 menuPos = selected.transform.position;
                        foreach (Transform child in selected.transform)
                        {
                            if (child.gameObject.tag == "MenuPoint")
                            {
                                menuPos = child.transform.position;
                            }
                        }

                        Vector3 screenPos = camera.WorldToScreenPoint(menuPos);
                        actionsPanel.GetComponent<RectTransform>().anchoredPosition = screenPos;

                        Interactable.Action[] acts = selected.GetComponent<Interactable>().Actions;
                        int i = 0;
                        foreach (Button b in actionsPanel.GetComponentsInChildren<Button>(true))
                        {
                            b.gameObject.SetActive(i < acts.Length);
                            if (i < acts.Length)
                                b.GetComponentInChildren<Text>().text = acts[i].ToString();
                            i++;
                        }
                        actionsPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, acts.Length * 40 + 20);
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
                    GetComponent<NavMeshAgent>().destination = child.transform.position;
                }
            }

            float d = (GetComponent<NavMeshAgent>().destination - transform.position).magnitude;
            GetComponent<NavMeshAgent>().speed = d > 10 ? 5 : 2;
            intendedAction = act;

        }

		public void OnActionSelected(int idx)
		{
			Interactable.Action[] acts = selected.GetComponent<Interactable> ().Actions;
            OnActionSelected( acts[idx]);		
		}

        public void OnInventorySelected()
        {
            if (inventoryList.GetComponent<Dropdown>().value == 0)
                tool = null;
            else
                tool = inventoryList.GetComponent<Dropdown>().options[inventoryList.GetComponent<Dropdown>().value].text;
        }
	}

}