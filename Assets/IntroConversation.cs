using UnityEngine;
using System.Collections;

namespace Fate
{

    public class IntroConversation : Conversation
    {
		public ConversationEntry[] _dialogs = new ConversationEntry[]
		{			
			new ConversationEntry("intro.fuel_guy.start",ConversationEntry.EntryType.Start, 0, null,"",1,0),
			new ConversationEntry("intro.fuel_guy.start",ConversationEntry.EntryType.Text, 1, null, "intro.fuel_guy_talk",2,1.5f),
			new ConversationEntry("intro.fuel_guy.start",ConversationEntry.EntryType.Text, 2, "FuelGuy", "intro.fuel_guy_talk_ok",3,1.0f),
			new ConversationEntry("intro.fuel_guy.start",ConversationEntry.EntryType.Event, 3, null, "intro.fuel_guy_leave",4,0.5f),
			new ConversationEntry("intro.fuel_guy.start",ConversationEntry.EntryType.End, 4, null, "",-1,0),

			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.Start, 0, null,"",1,0),
			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.Random, 1, null, "2|3|4",-1,0.5f),
			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.Text, 2, null, "intro.biker_talk.0",5,1.5f),
			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.Text, 3, null, "intro.biker_talk.1",5,1.5f),
			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.Text, 4, null, "intro.biker_talk.2",5,1.5f),
			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.Text, 5, "Biker", "intro.biker_fuck_off",6,1.0f),
			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.End, 6, null, "",-1,0),

			new ConversationEntry("intro.girl.pickup",ConversationEntry.EntryType.Start, 0, null,"",1,0),
			new ConversationEntry("intro.girl.pickup",ConversationEntry.EntryType.Text, 1, null, "intro.girl_pickup",2,1.5f),

			new ConversationEntry("intro.girl.pickup",ConversationEntry.EntryType.Random, 2, null, "5|3|4",-1,0.5f),

			new ConversationEntry("intro.girl.pickup",ConversationEntry.EntryType.Text, 3, "CounterGirl", "intro.girl_pickup.answer.0",6,2.0f),
			new ConversationEntry("intro.girl.pickup",ConversationEntry.EntryType.Text, 4, "CounterGirl", "intro.girl_pickup.answer.1",6,2.0f),
			new ConversationEntry("intro.girl.pickup",ConversationEntry.EntryType.Text, 5, "CounterGirl", "intro.girl_pickup.answer.2",6,2.0f),

			new ConversationEntry("intro.girl.pickup",ConversationEntry.EntryType.Random, 6, null, "7|8|9",-1,0.5f),

			new ConversationEntry("intro.girl.pickup",ConversationEntry.EntryType.Text, 7, null, "intro.girl_pickup.0",10,2.0f),
			new ConversationEntry("intro.girl.pickup",ConversationEntry.EntryType.Text, 8, null, "intro.girl_pickup.1",10,2.0f),
			new ConversationEntry("intro.girl.pickup",ConversationEntry.EntryType.Text, 9, null, "intro.girl_pickup.2",10,2.0f),

			new ConversationEntry("intro.girl.pickup",ConversationEntry.EntryType.End, 10, null, "",-1,0),

			new ConversationEntry("intro.girl.not_fueled",ConversationEntry.EntryType.Start, 0, null,"",1,0),
			new ConversationEntry("intro.girl.not_fueled",ConversationEntry.EntryType.Text, 1, "CounterGirl", "intro.girl_answer",2,2.0f),
			new ConversationEntry("intro.girl.not_fueled",ConversationEntry.EntryType.End, 2, null, "",-1,0),

			new ConversationEntry("intro.girl.fueled",ConversationEntry.EntryType.Start, 0, null,"",1,0),
			new ConversationEntry("intro.girl.fueled",ConversationEntry.EntryType.Text, 1, "CounterGirl", "intro.girl_pay",2,2.0f),
			new ConversationEntry("intro.girl.fueled",ConversationEntry.EntryType.End, 2, null, "",-1,0),


		};


        public IntroConversation()
        {
            dialogs = _dialogs;
        }

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
            base.Update();
        }
      
    }
}
