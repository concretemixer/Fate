using UnityEngine;
using System.Collections;

namespace Fate {

public class Interactable : MonoBehaviour {

	public enum Action {
		None,
		Look,
		LookClose,
		Enter,
		EnterTruck,
		Exit,
		Use,
		Talk
	}

	public Action[] actions;

	public Action[] Actions {
		get {
			return actions;
		}
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

}
