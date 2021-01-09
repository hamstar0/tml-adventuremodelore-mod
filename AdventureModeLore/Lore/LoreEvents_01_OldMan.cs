using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore {
	public partial class LoreEvents : ILoadable {
		public const string ObjectiveTitle_FindMerchant = "Find A Merchant";
		public const string ObjectiveTitle_FindJungle = "Find Jungle";


		public static NPCLoreStage LoreDefs01_OldMan { get; } = new NPCLoreStage(
			prereqs: new Func<bool>[] {
				() => ObjectivesAPI.HasRecordedObjectiveByNameAsFinished( LoreEvents.ObjectiveTitle_InvestigateDungeon )
			},
			npcType: NPCID.OldMan,
			subStages: new NPCLoreSubStage[] {
				new NPCLoreSubStage(
					dialogue: () => "You're in no shape to concern with why I'm here, or what this place is."
							+ " No, I'm not the only inhabitant of this cursed land, but anyone with sense will be in hiding."
							+ " They might come to you if you have something to offer them and a safe place to stay.",
					objective: new FlatObjective(
						title: LoreEvents.ObjectiveTitle_FindMerchant,
						description: "Other inhabitants exist in this land, some less enslaved than others. Build a"
								+ "\n"+"house for a merchant to settle in.",
						condition: ( obj ) => NPC.AnyNPCs( NPCID.Merchant )
					)
				),
				new NPCLoreSubStage(
					dialogue: () => "The plague? I know nothing of this. All I know is I made a pact long ago to keep this "
							+ "place sealed. I don't even remember why. This dungeon has its secrets, but I want nothing to "
							+ "do with it!"
							+ "\nYou might instead try investigating the jungle. I hear it too has its secrets. And dangers.",
					objective: new FlatObjective(
						title: LoreEvents.ObjectiveTitle_FindJungle,
						description: "The old man says there's something suspicious in the jungle. Maybe take a look?",
						condition: ( obj ) =>  Main.LocalPlayer.ZoneJungle
					)
				)
			}
		);
	}
}
