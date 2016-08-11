using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Fate {
		
	public class Scenario : MonoBehaviour {

        public Conversation conversation;
		public  GameObject textPanel;
		public  GameObject responsePanel;

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
            foreach (var sw in GameObject.Find("CameraHelpers").GetComponentsInChildren<CameraSwitch>())
            {
                sw.OnHeroEnterZone(name);
            }
		}

        public virtual bool OnActionIntended(Interactable.Action action, GameObject obj)
        {
            return true;
        }

		public virtual void OnAction(Interactable.Action action, GameObject obj)
		{
		}

		void HideTextPanel()
		{
			textPanel.SetActive (false);
		}

		void HideResponsePanel()
		{
			responsePanel.SetActive (false);
		}

		protected void SayToSelf(string s)
		{
			if (textPanel.activeSelf) {
				CancelInvoke ();
			}
			textPanel.SetActive (true);
			textPanel.GetComponentInChildren<Text>().text = "<color=#ffffff>"+s+"</color>";
			Invoke ("HideTextPanel", 5);
		}

		protected void Say(string s)
		{
			if (textPanel.activeSelf) {
				CancelInvoke ();
			}
			textPanel.SetActive (true);
			textPanel.GetComponentInChildren<Text>().text = "<color=#ffff00>"+s+"</color>";
			Invoke ("HideTextPanel", 5);
		}

		protected void Response(string s)
		{
			if (responsePanel.activeSelf) {
				CancelInvoke ();
			}
			responsePanel.SetActive (true);
			responsePanel.GetComponentInChildren<Text>().text = "<color=#ffbb00>"+s+"</color>";
			Invoke ("HideResponsePanel", 5);
		}

		public virtual void OnConversationEvent(string id, string name) 
		{
		}

        public virtual void OnConversationEnd(string id)
        {
            HideResponsePanel();
            HideTextPanel();
        }

        public virtual void OnConversationText(string character, string key)
        {
            if (character == null)
            {
                Say(locale.GetText(key));
                HideResponsePanel();
            }
            else
            {
                Response(locale.GetText(key));
                HideTextPanel();
            }
        }

        public virtual void OnConversationThink(string character, string key)
        {
            if (character == null)
            {
                SayToSelf(locale.GetText(key));
                HideResponsePanel();
            }
        }

		public virtual void OnConversationSelectAnswer(string id, string[] answers) 
		{
		}

	}

}
