using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using Objectives.Definitions;


namespace AdventureModeLore.Lore {
	public partial class LoreEvents : ILoadable {
		public const string ObjectiveTitle_FindMechanic = "Find Mechanic";

		public const string ObjectiveTitle_FindWitchDoctor = "Find Witch Doctor";


		public static NPCLoreStage LoreDefs05_FindMechanicAndWitchDoctor => new NPCLoreStage(
			name: "Goblin Quests",
			prereqs: new Func<bool>[] {
				() => NPC.AnyNPCs( NPCID.GoblinTinkerer )
			},
			npcType: NPCID.GoblinTinkerer,
			subStages: new NPCLoreSubStage[] {
				new NPCLoreSubStage(
					dialogue: () => "Sorry, I cannot be of much assistance in diplomacy with my former tribe. I doubt they"
							+" would have an open mind, anyhow.",
					objective: new FlatObjective(
						title: LoreEvents.ObjectiveTitle_FindMechanic,
						description: "Rumors exist of a plan to empower technology with the dungeon's spiritual"
								+ "\n"+"energies. This could be disasterous. Liberate the engineer.",
						condition: ( obj ) => NPC.AnyNPCs( NPCID.Mechanic )
					)
				),
				new NPCLoreSubStage(
					dialogue: () => "I know little about the undeath plague, but I do know of another inhabitant of these"
							+" lands who may: A lone witch doctor residing in the jungle. Unfortunately, he has gone into"
							+" hiding on on account of powerful monsters now residing in the jungle.",
					objective: new FlatObjective(
						title: LoreEvents.ObjectiveTitle_FindWitchDoctor,
						description: "A mysterious lizard-man sorcerer may know the plague's secret. Some"
								+ "\n"+"powerful monster in the jungle has put him into hiding, though.",
						condition: ( obj ) =>  NPC.AnyNPCs( NPCID.WitchDoctor )
					)
				)
			},
			isRepeatable: false
		);
	}
}
