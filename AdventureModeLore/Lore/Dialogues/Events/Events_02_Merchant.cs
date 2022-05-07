using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Dialogues.Events {
	public partial class DialogueLoreEventDefinitions {
		public const string ObjectiveTitle_FindOrb = "Find 1 of Each Orb Type";


		public static DialogueLoreEvent GetEvent_Dialogue02_Merchant() {
			var orbsMod = ModLoader.GetMod( "Orbs" );
			var orbs = new HashSet<int> {
				orbsMod.ItemType( "RedOrbItem" ),
				orbsMod.ItemType( "BlueOrbItem" ),
				orbsMod.ItemType( "BrownOrbItem" ),
				orbsMod.ItemType( "PurpleOrbItem" ),
				orbsMod.ItemType( "CyanOrbItem" ),
				orbsMod.ItemType( "GreenOrbItem" ),
				orbsMod.ItemType( "PinkOrbItem" ),
				orbsMod.ItemType( "YellowOrbItem" ),
				orbsMod.ItemType( "WhiteOrbItem" )
			};

			//

			return new DialogueLoreEvent(
				name: "Merchant Quests",
				prereqs: new Func<bool>[] {
					() => ObjectivesAPI.HasRecordedObjectiveByNameAsFinished(
						DialogueLoreEventDefinitions.ObjectiveTitle_FindMerchant
					)
				},
				npcType: NPCID.Merchant,
				subStages: new DialogueLoreEventStage[] {
					new DialogueLoreEventStage(
						dialogue: () => "I go where the money is. If you're looking for some, you'll need to find"
							+" treasures. This land itself is enchanted, and most areas can be accessed by using"
							+" those special magic orbs found here and there. They'll often be accompanying said"
							+" other treasures. Strange, huh?"
							+"\nSee if you can find 1 of each type. I'm eager to see how many there are!",
						objective: new FlatObjective(
							title: DialogueLoreEventDefinitions.ObjectiveTitle_FindOrb,
							description: "It seems the land itself is enchanted. Special orbs can be found that"
								+"\nappear to resonate with very land itself. Maybe these will be of help?",
							isImportant: false,
							condition: ( obj ) => {
								IEnumerable<int> eachOrbType = Main.LocalPlayer
										.inventory
										.Where( i => i?.active == true && orbs.Contains(i.type) )
										.Select( i => i.type );
								int totalUniqueOrbTypes = new HashSet<int>( eachOrbType )
									.Count();

								return totalUniqueOrbTypes >= orbs.Count;
							}
						)
					)
				},
				isRepeatable: false
			);
		}
	}
}
