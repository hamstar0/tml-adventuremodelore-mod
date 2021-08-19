using System;
using Terraria;
using Terraria.ID;
using Objectives;
using Objectives.Definitions;
using AdventureModeLore.Lore.Dialogue;


namespace AdventureModeLore.Lore.Sequenced.Events {
	public partial class DialogueLoreEvents {
		public const string ObjectiveTitle_FindMerchant = "Find A Merchant";
		public const string ObjectiveTitle_FindJungle = "Find Jungle";


		public static DialogueLoreEvent LoreDefs01_OldMan => new DialogueLoreEvent(
			name: "Old Man Quests",
			prereqs: new Func<bool>[] {
				() => ObjectivesAPI.HasRecordedObjectiveByNameAsFinished( DialogueLoreEvents.ObjectiveTitle_InvestigateDungeon )
			},
			npcType: NPCID.OldMan,
			subStages: new DialogueLoreEventStage[] {
				new DialogueLoreEventStage(
					dialogue: () => "You're in no shape to concern with why I'm here, or what this place is."
							+ " No, I'm not the only inhabitant of this cursed land, but anyone with sense will be in hiding."
							+ " They might come to you if you have something to offer them and a safe place to stay.",
					objective: new FlatObjective(
						title: DialogueLoreEvents.ObjectiveTitle_FindMerchant,
						description: "Other inhabitants exist in this land, some less enslaved than others. Build a"
								+ "\n"+"house for a merchant to settle in.",
						condition: ( obj ) => NPC.AnyNPCs( NPCID.Merchant )
					)
				),
				new DialogueLoreEventStage(
					dialogue: () => "The plague? I know nothing of this. All I know is I made a pact long ago to keep this "
							+ "place sealed. I don't even remember why. This dungeon has its secrets, but I want nothing to "
							+ "do with it!"
							+ "\nYou might instead try investigating the jungle. I hear it too has its secrets. And dangers.",
					objective: new FlatObjective(
						title: DialogueLoreEvents.ObjectiveTitle_FindJungle,
						description: "The old man says there's something suspicious in the jungle. Maybe take a look?",
						condition: ( obj ) =>  Main.LocalPlayer.ZoneJungle
					)
				)
			},
			isRepeatable: false
		);
	}
}
