using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using Objectives.Definitions;


namespace AdventureModeLore.Lore {
	public partial class LoreEvents : ILoadable {
		public const string ObjectiveTitle_InvestigateDungeon = "Investigate Dungeon";

		public const string ObjectiveTitle_Find10Mirrors = "Locate 10 Mounted Magic Mirrors";


		public static NPCLoreStage LoreDefs00_Guide { get; } = new NPCLoreStage(
			name: "Guide Quests",
			prereqs: new Func<bool>[0],
			npcType: NPCID.Guide,
			subStages: new NPCLoreSubStage[] {
				new NPCLoreSubStage(
					dialogue: () => "Before the attack, reports came in of a large brick structure on the island a bit "
							+"inland. Perhaps we should check it out?",
					objective: new FlatObjective(
						title: LoreEvents.ObjectiveTitle_InvestigateDungeon,
						description: "There appears to be a large, ominous structure with a suspicious old man"
								+"\n"+"wandering around it's entrance. Recommend an investigation.",
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
				new NPCLoreSubStage(
					dialogue: () => "If you're having trouble getting to a place, use your ropes, platforms, framing planks, "
							+"and track deployment kits. There should be some in the barrel on the raft. If you need more, "
							+"some of these can be crafted or bought, if you have the needed materials or money."
				),
				new NPCLoreSubStage(
					dialogue: () => "Speaking of getting around, this island is pretty big, and we're gonna need to get "
							+"around. In the raft's storage are magic wall mirrors that can be used to quickly travel "
							+"between each other. Those furnishing Kits also come with them. I estimate we'll need at least "
							+"[c/FFFFBB:10] of these to get up and running. Spread them around for best effect.",
					objective: new PercentObjective(
						title: LoreEvents.ObjectiveTitle_Find10Mirrors,
						description: "Create or locate 10 Mounted Magic Mirrors on your map.",
						condition: ( obj ) => (float)LoreEvents.CountDiscoveredMirrors() / 10f
					)
				)
			}
		);
	}
}
