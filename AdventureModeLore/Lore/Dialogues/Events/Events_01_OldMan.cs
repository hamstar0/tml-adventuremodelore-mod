﻿using System;
using Terraria;
using Terraria.ID;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Dialogues.Events {
	public partial class DialogueLoreEventDefinitions {
		public const string ObjectiveTitle_FindMerchant = "Find A Merchant";
		public const string ObjectiveTitle_FindJungle = "Find Jungle";


		public static DialogueLoreEvent GetEvent_Dialogue01_OldMan() => new DialogueLoreEvent(
			name: "Old Man Quests",
			prereqs: new Func<bool>[] {
				() => ObjectivesAPI.HasRecordedObjectiveByNameAsFinished( DialogueLoreEventDefinitions.ObjectiveTitle_InvestigateDungeon )
			},
			npcType: NPCID.OldMan,
			subStages: new DialogueLoreEventStage[] {
				new DialogueLoreEventStage(
					dialogue: () => "You're in no shape to concern with why I'm here, or what this place is."
							+ " No, I'm not the only inhabitant of this cursed land, but anyone with sense will be in hiding."
							+ " They might come to you if you have something to offer them and a safe place to stay.",
					objective: new FlatObjective(
						title: DialogueLoreEventDefinitions.ObjectiveTitle_FindMerchant,
						isImportant: false,
						description: "Other inhabitants exist in this land, some less enslaved than others. Build a"
								+ "\n"+"house for a merchant to settle in.",
						condition: ( obj ) => NPC.AnyNPCs( NPCID.Merchant )
					)
				),
				new DialogueLoreEventStage(
					dialogue: () => "The plague? I know nothing of this. All I know is I made a pact long ago to keep this "
							+ "place sealed. I don't even remember why. This dungeon has its secrets, but I want nothing to "
							+ "do with it!"
							+ "\n"+"You might instead try investigating the jungle. I hear it too has its secrets. And dangers.",
					objective: new FlatObjective(
						title: DialogueLoreEventDefinitions.ObjectiveTitle_FindJungle,
						description: "The old man says there's something suspicious in the jungle. Maybe take a look?",
						isImportant: true,
						condition: ( obj ) =>  Main.LocalPlayer.ZoneJungle
					)
				),
				new DialogueLoreEventStage(
					dialogue: () => "I guess to reach the jungle you'll need to get through that barrier over yonder. "
							+ "It was created to block access to the rest of this island. Unless you know the arts of "
							+ "creating barriers of your own, you're out of luck penetrating through others..."
				)
			},
			isRepeatable: false
		);
	}
}
