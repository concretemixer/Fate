using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Fate
{

    public class Conversation : MonoBehaviour
    {
		public class ConversationEntry {
			public enum EntryType {
				Start,
				Text,
				Choice,
				Event,
				End,
			}

            public ConversationEntry(
                string dialogId,
                EntryType type,
                int id,
                string character,
                string data,
                int next,
                float pause)
            {
                this.dialogId = dialogId;
                this.type = type;
                this.id = id;
                this.character = character;
                this.data = data;
                this.next = next;
                this.pause = pause;
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

        public ConversationEntry[] dialogs;

        protected State state = State.Idle;
        private string currentDlgId = "";
        private int currentId = 0;
        private int nextId = 0;
        private float timeToNext = 0;

        public Scenario scenario;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        protected void Update()
        {
            if (state == State.Speaking)
            {
                timeToNext -= Time.deltaTime;
                if (timeToNext <= 0)
                {
                    ConversationEntry e = GetEntryById(currentDlgId, nextId);
                    if (e != null)
                    {


                        switch (e.type)
                        {
                            case ConversationEntry.EntryType.Start:
                                break;
                            case ConversationEntry.EntryType.Text:
                                scenario.OnConversationText(e.character, e.data);
                                break;
                            case ConversationEntry.EntryType.Choice:
                                {
                                    List<string> keys = new List<string>();
                                    foreach (var _id in e.data.Split(new char[] { '|' }))
                                    {
                                        int id = int.Parse(_id);
                                        ConversationEntry e2 = GetEntryById(currentDlgId, id);
                                        keys.Add(e2.data);
                                    }
                                    state = State.Waiting;
                                    scenario.OnConversationSelectAnswer(currentDlgId, keys.ToArray());
                                }
                                break;
                            case ConversationEntry.EntryType.Event:
                                scenario.OnConversationEvent(currentDlgId, e.data);
                                break;
                            case ConversationEntry.EntryType.End:
                                scenario.OnConversationEnd(currentDlgId);
                                state = State.Idle;
                                break;
                        }

                        if (e.type == ConversationEntry.EntryType.Choice)
                        {
                            state = State.Waiting;
                        }
                        else
                        {
                            nextId = e.next;
                            timeToNext = e.pause;
                            currentId = e.id;
                        }
                    }
                }
            }

        }

        private ConversationEntry GetEntryById(string dialogId, int id)
        {
            foreach (var e in dialogs)
            {
                if (id == e.id && dialogId == e.dialogId)
                    return e;
            }
            return null;
        }

        public virtual void StartDialog(string id)
        {
            if (state != State.Idle)
                return;

            foreach (var e in dialogs)
            {
                if (e.type == ConversationEntry.EntryType.Start && id == e.dialogId)
                {
                    currentDlgId = id;
                    currentId = e.id;
                    nextId = e.next;
                    state = State.Speaking;
                    timeToNext = e.pause;

                    return;
                }                    
            }            
        }

        public virtual void SelectAnswer(int idx)
        {
            if (state == State.Waiting)
            {
                ConversationEntry e = GetEntryById(currentDlgId, nextId);
                List<string> keys = new List<string>();

                int i = 0;
                foreach (var _id in e.data.Split(new char[] { '|' }))
                {
                    int id = int.Parse(_id);
                    if (i == idx)
                    {
                        nextId = id;
                        timeToNext = 0;
                        state = State.Speaking;

                        break;
                    }
                }
            }
        }
    }
}
