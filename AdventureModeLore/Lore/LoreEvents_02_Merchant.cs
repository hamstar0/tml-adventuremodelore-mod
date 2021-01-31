using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Players;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore {
	public partial class LoreEvents : ILoadable {
		public const string ObjectiveTitle_FindOrb = "Find 1 of Each Orb Type";


		public static NPCLoreStage LoreDefs02_Merchant { get; } = new NPCLoreStage(
			name: "Merchant Quests",
			prereqs: new Func<bool>[] {
				() => ObjectivesAPI.HasRecordedObjectiveByNameAsFinished( LoreEvents.ObjectiveTitle_FindMerchant )
			},
			npcType: NPCID.Merchant,
			subStages: new NPCLoreSubStage[] {
				new NPCLoreSubStage(
					dialogue: () => "I go where the money is. If you're looking for some, you'll need to find treasures."
						+" This land itself is enchanted, and most areas can be accessed by using those special"
						+" magic orbs found here and there. They'll often be accompanying said other treasures."
						+" Strange, huh?",
					objective: new FlatObjective(
						title: LoreEvents.ObjectiveTitle_FindOrb,
						description: "It seems the land itself is enchanted. Special orbs can be found that appear"
							+ "\n"+"to resonate with the terrain. Maybe these will be of help?",
						condition: ( obj ) => {
							var orbsMod = ModLoader.GetMod( "Orbs" );

							//

							bool hasOrb( string itemName ) {
								return PlayerItemFinderHelpers.CountTotalOfEach(
									player: Main.LocalPlayer,
									itemTypes: new HashSet<int> { orbsMod.ItemType(itemName) },
									includeBanks: true
								) > 0;
							}

							//

							return hasOrb("RedOrbItem")
								&& hasOrb("BlueOrbItem")
								&& hasOrb("BrownOrbItem")
								&& hasOrb("PurpleOrbItem")
								&& hasOrb("CyanOrbItem")
								&& hasOrb("GreenOrbItem")
								&& hasOrb("PinkOrbItem")
								&& hasOrb("YellowOrbItem")
								&& hasOrb("WhiteOrbItem");
						}
					)
				)
			},
			isRepeatable: false
		);
	}
}
