using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_WeakRef_Trickster() {
			if( TheTrickster.TheTricksterAPI.GetTricksterDefeatLocations().Count <= 0 ) {
				return false;
			}

			int tricksterType = ModContent.NPCType<TheTrickster.NPCs.TricksterNPC>();

			if( NPC.AnyNPCs(tricksterType) ) {	// Frees the timer when no tricksters exist
				Timers.SetTimer( "AML_TricksterLore", 60 * 5, false, () => false );
			}

			int timerTicks = Timers.GetTimerTickDuration( "AML_TricksterLore" );

			return timerTicks == 2;	// TODO: Flawed
		}


		////////////////

		private static GeneralLoreEvent GetEvent_Radio_Trickster() {
			bool PreReq() {
				if( ModLoader.GetMod("PKEMeter") == null ) {
					return false;
				}
				if( ModLoader.GetMod("TheTrickster") == null ) {
					return false;
				}

				return GeneralLoreEventDefinitions.Event_Radio_WeakRef_Trickster();
			}

			//

			string msgId = "AML_Radio_Trickster";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"That thing you just met must be what was describes in those expedition logs. What a crazy monstrosity"
				+" it is! What are its motives? It certainly has a strange power..."
				+"\n \n"
				+"In any case, that thing is just unusual enough that it may be worth investigating. It seems"
				+" unlikely to return to an area where it's already been encountered, apparently. These will become"
				+" marked on your map, from now on."
				+"\n \n"
				+"I shouldn't need to say this, but be careful! There's no telling what kind of mischief it's capable"
				+" of! Expect ambushes."
			);

			return new GeneralLoreEvent(
				name: "Radio - Trickster",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						MessagesAPI.AddMessagesCategoriesInitializeEvent( () => {
							MessagesAPI.AddMessage(
								title: "About the Trickster",
								description: msg,
								modOfOrigin: AMLMod.Instance,
								alertPlayer: MessagesAPI.IsUnread(msgId),
								isImportant: true,
								parentMessage: MessagesAPI.EventsCategoryMsg,
								id: msgId
							);
						} );
						return false;
					} );
				},
				isRepeatable: false
			);
		}
	}
}
