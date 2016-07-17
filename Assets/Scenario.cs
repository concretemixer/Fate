using UnityEngine;
using System.Collections;

namespace Fate {
		
	public class Scenario : MonoBehaviour {

		public enum State {			
			Intro,
			Playing,
			Interlude,
			Death,
			Outro,
		}

		State _state = State.Intro;

		public State state {
			get {
				return _state;
			}
			protected set {
				_state = value;
			}
		}

		protected Locale locale = new Locale ();
		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public virtual void OnHeroEnterZone(string name) 
		{
		}

		public virtual void OnAction(Interactable.Action action, GameObject obj)
		{
		}
	}

}
