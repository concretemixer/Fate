using UnityEngine;
using System.Collections;

namespace Fate
{

    public class IntroConversation : Conversation
    {
		public ConversationEntry[] dialog = new ConversationEntry[]
		{			
			new ConversationEntry() {"intro.fuel_guy.start",ConversationEntry.EntryType.Begin, 0, null,"",1,0},
			new ConversationEntry()	{"intro.fuel_guy.start",ConversationEntry.EntryType.Text, 1, null, "intro.fuel_guy_talk",2,2},
			new ConversationEntry() {"intro.fuel_guy.start",ConversationEntry.EntryType.Text, 2, "FuelGuy", "intro.fuel_guy_talk_ok",3,2},
			new ConversationEntry() {"intro.fuel_guy.start",ConversationEntry.EntryType.End, 3, null, "",-1,0}

		};


		// intro.fuel_guy.start
		// intro.biker
		// intro.girl.puckup
		// intro.girl.pay_cash
		// intro.girl.pay_card

		// intro.girl.pay_card_fail
		// intro.girl.pay_card_more
		// intro.girl.pay_card_ok



        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void StartDialog(string id)
        {
        }

        public override void SelectAnswer(string id, int idx)
        {
        }
    }
}
