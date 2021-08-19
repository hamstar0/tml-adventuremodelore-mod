using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using Objectives.Definitions;
using AdventureModeLore.Lore.Dialogues;


namespace AdventureModeLore.Lore.Dialogues.Events {
	public partial class DialogueLoreEvents {
		public const string ObjectiveTitle_InvestigateDungeon = "Investigate Dungeon";

		public const string ObjectiveTitle_Find10Mirrors = "Locate 10 Mounted Magic Mirrors";


		public static DialogueLoreEvent LoreDefs00_Guide => new DialogueLoreEvent(
			name: "Guide Quests",
			prereqs: new Func<bool>[0],
			npcType: NPCID.Guide,
			subStages: new DialogueLoreEventStage[] {
				new DialogueLoreEventStage(
					dialogue: () => "Before the attack, reports came in of a large brick structure on the island a bit "
							+"inland. Perhaps we should check it out?",
					objective: new FlatObjective(
						title: DialogueLoreEvents.ObjectiveTitle_InvestigateDungeon,
						description: "There's a large, ominous structure with a strange old man wandering around"
								+"\n"+"it's entrance. Ask the old man for information about the island.",
						condition: ( obj ) => {
							return Main.player.Any( plr => {
								if( plr?.active != true ) {
									return false;
								}

								NPC oldMan = Main.npc.FirstOrDefault( n => n.type == NPCID.OldMan );
								if( oldMan?.active != true ) {
									return false;
								}

								return ( plr.position - oldMan.position ).LengthSquared() < ( 256f * 256f );
							} );
						}
					)
				),
				new DialogueLoreEventStage(
					dialogue: () => "If you're having trouble getting somewhere, use your ropes, platforms, framing planks, "
							+"and track deployment kits. There should be some in the raft's barrel. If you need more, "
							+"some of these can be crafted or bought, if you have the needed materials or money."
				),
				new DialogueLoreEventStage(
					dialogue: () => "Speaking of, this island is pretty big. In the raft's storage are magic wall mirrors "
							+"that allow fast travel between each other. Furnishing Kits also come with them. We should get "
							+"at least [c/FFFFBB:10] of these spread around the island for our operations.",
					objective: new PercentObjective(
						title: DialogueLoreEvents.ObjectiveTitle_Find10Mirrors,
						description: "Create or locate 10 Mounted Magic Mirrors on your map.",
						condition: ( obj ) => (float)DialogueLoreEvents.CountDiscoveredMirrors() / 10f
					)
				)
			},
			isRepeatable: false
		);
	}
}
