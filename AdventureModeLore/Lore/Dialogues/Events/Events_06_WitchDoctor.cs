using System;
using Terraria;
using Terraria.ID;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Dialogues.Events {
	public partial class DialogueLoreEventDefinitions {
		public const string ObjectiveTitle_SummonWoF = "Sacrifice Voodoo Doll";


		public static DialogueLoreEvent GetEvent_Dialogue06_SummonWoF() => new DialogueLoreEvent(
			name: "Witch Doctor Quests",
			prereqs: new Func<bool>[] {
				() => NPC.AnyNPCs( NPCID.WitchDoctor )
			},
			npcType: NPCID.WitchDoctor,
			subStages: new DialogueLoreEventStage[] {
				new DialogueLoreEventStage(
					dialogue: () => "What you call a plague is the disruption of the souls of the dead in their journey back"
							+" into the land. Their anger can be felt by the living and dead alike, causing all of the"
							+" troubles you now know too well. Long ago, a ritual was performed to steal the power of dead"
							+" for use by greedy men. It resulted in a war that reached out even into the stars, and nearly"
							+" brought our world to an end."
				),
				new DialogueLoreEventStage(
					dialogue: () => "Today, few live who even remember of these events. The end of the war did not truly come"
							+" until the stolen souls were finally returned to the world. However, these souls had since"
							+" become troubled, and the ritual performed for this task was executed improperly!"
				),
				new DialogueLoreEventStage(
					dialogue: () => "I'm afraid to say, but it may already be too late. Long have these tormented spirits"
							+" dwelled in a most pitiful incarceration in the depths. Now their containment is failing, and"
							+" once unleashed, their wrath will surely plunge all the land into chaos! If you wish to do"
							+" something about this, you may not like what you will hear next."
				),
				new DialogueLoreEventStage(
					dialogue: () => "You must find a living descendant of the ones responsible for placing the fateful seal"
							+" upon the dead, and sacrifice their soul to these spirits. I will show you the ritual. Alas, a"
							+" person of whom I speak resides among you as a trusted companion. One who has been with you"
							+" from the beginning. It is your choice to inform them of this. May this land have mercy on"
							+" their soul.",
					objective: new FlatObjective(
						title: DialogueLoreEventDefinitions.ObjectiveTitle_SummonWoF,
						description: "The witch doctor describes a ritual to destroy the spiritual energy"
							+ "\n"+"confluence; the source of the plague. It involves a voodoo sacrifice"
							+ "\n"+"of one of its makers near its source: The underworld.",
						condition: ( obj ) => NPC.AnyNPCs( NPCID.WallofFlesh )
					)
				),
			},
			isRepeatable: false
		);
	}
}
