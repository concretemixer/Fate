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
		Talk,
        Take
	}

	public Action[] actions;
    public Action defaultAction = Action.None;

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
