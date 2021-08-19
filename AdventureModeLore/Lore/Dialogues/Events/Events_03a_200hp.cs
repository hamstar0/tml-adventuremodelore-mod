using System;
using Terraria;
using Terraria.ID;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Dialogues.Events {
	public partial class DialogueLoreEventDefinitions {
		public const string ObjectiveTitle_TalkToGoblin = "Talk To A Goblin";


		public static DialogueLoreEvent LoreDefs03a_200hp => new DialogueLoreEvent(
			name: "Pre-Goblins Quests",
			prereqs: new Func<bool>[] {
				() => Main.LocalPlayer.statLifeMax >= 200
			},
			npcType: NPCID.Guide,
			subStages: new DialogueLoreEventStage[] {
				new DialogueLoreEventStage(
					dialogue: () => "I have to tell you something. There are natives on this island!"
						+" Not mere scattered survivors or profiteers, but a full blown army of goblins!"
						+" We must find a way to communicate with them directly."
						+" I fear our presence here might be taken the wrong way!",
					objective: new FlatObjective(
						title: DialogueLoreEventDefinitions.ObjectiveTitle_TalkToGoblin,
						description: "It would seem there are natives in this land, if you'd call them that. Try to"
							+ "\n"+"somehow open a line of communication with them.",
						condition: ( obj ) => NPC.AnyNPCs( NPCID.GoblinTinkerer )
					)
				)
			},
			isRepeatable: false
		);
	}
}
