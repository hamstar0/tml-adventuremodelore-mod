﻿using System;
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
					dialogue: () => "Goblins sure are a wild lot. Rather than rise up like us civilized folk, they'd"
						+" rather brand their greatest minds heretics and lock them away. I hear they're kept buried"
						+" deep in the underground desert where no one wants to go. Prove them wrong. Try crafting"
						+" Seismic Charges by combining your orbs with my bombs to make your way through.",
					objective: new FlatObjective(
						title: DialogueLoreEventDefinitions.ObjectiveTitle_RescueGoblin,
						description: "The goblins have imprisoned one of their own for heresy. Search the underground"
							+"\n"+"desert for their whereabouts. Seismic Charges may help.",
						condition: ( obj ) => NPC.savedGoblin
					)
				)
			},
			isRepeatable: false
		);
	}
}
