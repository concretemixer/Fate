using UnityEngine;
using System.Collections;

namespace Fate {

public class Interactable : MonoBehaviour {

	public enum Action {
		None = 0,
		Look = 1,
		LookClose = 2,
		Enter = 3,
		EnterTruck = 4,
		Exit = 5,
		Use = 6,
		Talk = 7,
        Take = 8,
        UseItem = 9
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
