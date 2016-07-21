using UnityEngine;
using System.Collections;

namespace Fate
{

    public class Conversation : MonoBehaviour
    {
		public struct ConversationEntry {
			public enum EntryType {
				Start,
				Text,
				Choice,
				Event,
				End,
			}

			public string dialogId;
			public EntryType type;
			public int  id;
			public string character;
			public string data;
			public int next;						
			public float pause;						
		}

        public enum State
        {
            Idle,
            Speaking,
            Waiting
        }

        private string currentId = "";
        private int currentStep = 0;


        public Scenario scenario;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void StartDialog(string id)
        {
        }

        public virtual void SelectAnswer(string id, int idx)
        {
        }
    }
}
