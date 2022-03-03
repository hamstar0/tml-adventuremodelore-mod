using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Objectives.Definitions;
using FindableManaCrystals.Items;


namespace AdventureModeLore.Lore.Dialogues.Events {
	public partial class DialogueLoreEventDefinitions {
		public const string ObjectiveTitle_EndPlague = "Eliminate Plague Source";
		
		public const string ObjectiveTitle_FindMagicalPhenomena = "Discover Magical Phenomena";
		
		public const string ObjectiveTitle_InvestigateDungeon = "Investigate Dungeon";

		public const string ObjectiveTitle_Find10Mirrors = "Locate 10 Mounted Magic Mirrors";


		////

		public static DialogueLoreEvent GetEvent_Dialogue00_Guide() => new DialogueLoreEvent(
			name: "Guide Quests",
			prereqs: new Func<bool>[0],
			npcType: NPCID.Guide,
			subStages: new DialogueLoreEventStage[] {
				new DialogueLoreEventStage(
					dialogue: () => "We made it! We finally have a chance to put an end to the accursed plague! With "
						+"your special... magical gift, we must learn about the magical properties of this island, "
						+"which seem to be the key to this threat.",
					objectives: new Objective[] {
						new FlatObjective(
							title: DialogueLoreEventDefinitions.ObjectiveTitle_EndPlague,
							description: "Eliminate the source of the undeath plague.",
							condition: ( obj ) => Main.hardMode
						),
						new FlatObjective(
							title: DialogueLoreEventDefinitions.ObjectiveTitle_FindMagicalPhenomena,
							description: "Discover a hidden sample of magical phenomena on this island.",
							condition: ( obj ) => {
								IEnumerable<Item> manaShards = Main.LocalPlayer.inventory
									.Where( i =>
										i?.active == true
										&& i.type == ModContent.ItemType<ManaCrystalShardItem>()
									);

								return manaShards.Count() >= 1;
							}
						)
					}
				),
				new DialogueLoreEventStage(
					dialogue: () => "Before the attack, reports came in of a large brick structure on the island a bit "
						+"inland. Perhaps our first stop should be to check it out?",
					objective: new FlatObjective(
						title: DialogueLoreEventDefinitions.ObjectiveTitle_InvestigateDungeon,
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

								return (plr.position - oldMan.position).LengthSquared() < (256f * 256f);
							} );
						}
					)
				),
				new DialogueLoreEventStage(
					dialogue: () => "If you're having trouble getting somewhere, use your ropes, platforms, framing "
						+"planks, and track deployment kits. There should be some in the raft's barrel. If you need "
						+"more, you'll need to craft or buy them, if and when you can."
				),
				/*new DialogueLoreEventStage(
					dialogue: () => "Speaking of, this island is pretty big. In the raft's storage are magic wall mirrors "
							+"that allow fast travel between each other. Furnishing Kits also come with them. We should get "
							+"at least [c/FFFFBB:10] of these spread around the island for our operations.",
					objective: new PercentObjective(
						title: DialogueLoreEventDefinitions.ObjectiveTitle_Find10Mirrors,
						description: "Create or locate 10 Mounted Magic Mirrors on your map.",
						condition: ( obj ) => (float)DialogueLoreEventDefinitions.CountDiscoveredMirrors() / 10f
					)
				)*/
			},
			isRepeatable: false
		);
	}
}
