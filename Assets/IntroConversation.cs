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
			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.Random, 1, null, "2|3|4|5",-1,0.5f),
			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.Text, 2, null, "intro.biker_talk.0",6,1.5f),
			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.Text, 3, null, "intro.biker_talk.1",6,1.5f),
			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.Text, 4, null, "intro.biker_talk.2",6,1.5f),
			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.Text, 5, null, "intro.biker_talk.3",6,1.5f),
			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.Text, 6, "Biker", "intro.biker_fuck_off",7,1.0f),
			new ConversationEntry("intro.biker.start",ConversationEntry.EntryType.End, 7, null, "",-1,0),

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

                new ConversationEntry("intro.girl.what",ConversationEntry.EntryType.Start, 0, null,"",1,0),
                new ConversationEntry("intro.girl.what",ConversationEntry.EntryType.Text, 1, "CounterGirl", "intro.girl_what",2,2.0f),
                new ConversationEntry("intro.girl.what",ConversationEntry.EntryType.End, 2, null, "",-1,0),

                new ConversationEntry("intro.girl.no_cash",ConversationEntry.EntryType.Start, 0, null,"",1,1.5f),
                new ConversationEntry("intro.girl.no_cash",ConversationEntry.EntryType.Event, 1, null, "intro.girl_no_cash",2,0),
                new ConversationEntry("intro.girl.no_cash",ConversationEntry.EntryType.Text, 2, "CounterGirl", "intro.girl_no_cash",3,2.0f),
                new ConversationEntry("intro.girl.no_cash",ConversationEntry.EntryType.End, 3, null, "",-1,0),

                new ConversationEntry("intro.girl.cash_ok",ConversationEntry.EntryType.Start, 0, null,"",1,1.5f),
                new ConversationEntry("intro.girl.cash_ok",ConversationEntry.EntryType.Text, 1, "CounterGirl", "intro.girl_cash_ok",2,2.0f),
                new ConversationEntry("intro.girl.cash_ok",ConversationEntry.EntryType.End, 2, null, "",-1,0),

                new ConversationEntry("intro.girl.enter_pin",ConversationEntry.EntryType.Start, 0, null,"",1,1.5f),
                new ConversationEntry("intro.girl.enter_pin",ConversationEntry.EntryType.Text, 1, "CounterGirl", "intro.girl_pin",2,1.0f),
                new ConversationEntry("intro.girl.enter_pin",ConversationEntry.EntryType.Event, 2, null, "intro.enter_pin",3,0.5f),
                new ConversationEntry("intro.girl.enter_pin",ConversationEntry.EntryType.End, 3, null, "",-1,0),

                new ConversationEntry("intro.girl.pin_forgot",ConversationEntry.EntryType.Start, 0, null,"",1,0.5f),
                new ConversationEntry("intro.girl.pin_forgot",ConversationEntry.EntryType.Event, 1, null, "intro.girl_no_cash",2,0),
                new ConversationEntry("intro.girl.pin_forgot",ConversationEntry.EntryType.Text, 2, "CounterGirl", "intro.girl_pin_wrong_1",3,2.0f),
                new ConversationEntry("intro.girl.pin_forgot",ConversationEntry.EntryType.Text, 3, null, "intro.girl_pin_wrong_2",4,2.0f),
                new ConversationEntry("intro.girl.pin_forgot",ConversationEntry.EntryType.End, 4, null, "",-1,0),

                new ConversationEntry("intro.girl.pin_good",ConversationEntry.EntryType.Start, 0, null,"",1,0.5f),
                new ConversationEntry("intro.girl.pin_good",ConversationEntry.EntryType.Event, 1, null, "intro.girl_yes_cash",2,0),
                new ConversationEntry("intro.girl.pin_good",ConversationEntry.EntryType.Text, 2, "CounterGirl", "intro.girl_pin_good_1",3,2.0f),
                new ConversationEntry("intro.girl.pin_good",ConversationEntry.EntryType.End, 3, null, "",-1,0),


                new ConversationEntry("intro.girl.else",ConversationEntry.EntryType.Start, 0, null,"",1,0.5f),
                new ConversationEntry("intro.girl.else",ConversationEntry.EntryType.Text, 1, "CounterGirl", "intro.girl_else_1",2,2.0f),
                new ConversationEntry("intro.girl.else",ConversationEntry.EntryType.Text, 2, null, "intro.girl_else_2",3,2.0f),
                new ConversationEntry("intro.girl.else",ConversationEntry.EntryType.End, 3, null, "",-1,0),

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
