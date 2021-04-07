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
				() => NPC.savedGoblin
			},
			npcType: NPCID.GoblinTinkerer,
			subStages: new NPCLoreSubStage[] {
				new NPCLoreSubStage(
					//"Sorry, I cannot be of much assistance in diplomacy with my former tribe. I doubt they"
					//		+" would have an open mind, anyhow.
					dialogue: () => "The only thing I can tell you for your mission is that strange activities have been seen"
							+" at that big dungeon you've encountered. It's a veritable pressure cooker of necromantic"
							+" energies. Someone is building technology there and infusing it with such energy. They must"
							+" be a skilled engineer, but if this isn't stopped, it could be disasterous. If you can find"
							+" their engineer, maybe we can turn things around?",
					objective: new FlatObjective(
						title: LoreEvents.ObjectiveTitle_FindMechanic,
						description: "Rumors exist of a plan to empower technology with the dungeon's spiritual"
								+ "\n"+"energies. This could be disasterous. Liberate the engineer.",
						condition: ( obj ) => NPC.savedMech
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
						condition: ( obj ) => NPC.AnyNPCs( NPCID.WitchDoctor )
					)
				)
			},
			isRepeatable: false
		);
	}
}
