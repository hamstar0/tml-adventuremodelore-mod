using System;
using Terraria;
using Terraria.ID;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Dialogues.Events {
	public partial class DialogueLoreEventDefinitions {
		public const string ObjectiveTitle_RescueGoblin = "Rescue Goblin Scientist";


		public static DialogueLoreEvent GetEvent_Dialogue03c_RescueGoblin() => new DialogueLoreEvent(
			name: "Goblin Rescue",
			prereqs: new Func<bool>[] {
				() => NPC.downedGoblins
			},
			npcType: NPCID.Demolitionist,
			subStages: new DialogueLoreEventStage[] {
				new DialogueLoreEventStage(
					dialogue: () => "Goblins sure are a wild lot. Rather than advance like us civilized folk, they'd"
						+" rather have savage wars and brand those of their own smart enough to know better as heretics. I hear"
						+" they like to keep such dissidants buried deep in the underground desert where no one wants to go."
						+" Prove them wrong. Try crafting Seismic Charges by combining your orbs with my bombs to make your way"
						+" through, and liberate any of these hapless prisoners you find.",
					objective: new FlatObjective(
						title: DialogueLoreEventDefinitions.ObjectiveTitle_RescueGoblin,
						description: "The goblins keep dissidants imprisoned deep in the underground desert. Craft"
							+"\n"+"Seismic Charges to help liberate any you can find.",
						isImportant: true,
						condition: ( obj ) => NPC.savedGoblin
					)
				)
			},
			isRepeatable: false
		);
	}
}
