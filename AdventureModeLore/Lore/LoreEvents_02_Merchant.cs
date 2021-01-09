using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Players;
using Objectives.Definitions;


namespace AdventureModeLore.Lore {
	public partial class LoreEvents : ILoadable {
		public const string ObjectiveTitle_FindOrb = "Find an Orb";


		public static NPCLoreStage LoreDefs02_Merchant { get; } = new NPCLoreStage(
			prereqObjectives: new string[] {
				LoreEvents.ObjectiveTitle_FindMerchant
			},
			prereqConditions: new Func<bool>[0],
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
							+ "\n"+"to resonate with the terrain. Maybe this will be of help?",
						condition: ( obj ) => {
							var orbsMod = ModLoader.GetMod( "Orbs" );

							return PlayerItemFinderHelpers.CountTotalOfEach(
								player: Main.LocalPlayer,
								itemTypes: new HashSet<int> {
									orbsMod.ItemType("RedOrbItem"),
									orbsMod.ItemType("BlueOrbItem"),
									orbsMod.ItemType("TealOrbItem"),
									orbsMod.ItemType("PurpleOrbItem"),
									orbsMod.ItemType("CyanOrbItem"),
									orbsMod.ItemType("GreenOrbItem"),
									orbsMod.ItemType("PinkOrbItem"),
									orbsMod.ItemType("YellowOrbItem"),
									orbsMod.ItemType("WhiteOrbItem")
								},
								includeBanks: true
							) > 0;
						}
					)
				)
			}
		);
	}
}
