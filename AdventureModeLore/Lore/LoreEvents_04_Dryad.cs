using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.World;
using Objectives.Definitions;


namespace AdventureModeLore.Lore {
	public partial class LoreEvents : ILoadable {
		//public const string ObjectiveTitle_DefeatEvil = "Defeat The Evil Biome's Guardian";
		public static string ObjectiveTitle_DefeatEvil => "Defeat The "+(WorldGen.crimson?"Crimson's":"Corruption")+" Guardian";

		public const string ObjectiveTitle_ReachUnderworld = "Reach Underworld";


		public static NPCLoreStage LoreDefs04_DefeatEvil => new NPCLoreStage(
			name: "Dryad Quests",
			prereqs: new Func<bool>[] {
				() => NPC.AnyNPCs( NPCID.Dryad )
			},
			npcType: NPCID.Dryad,
			subStages: new NPCLoreSubStage[] {
				new NPCLoreSubStage(
					dialogue: () => "I see you are here to stop the undeath plague. Might I suggest you first start with the "
							+(WorldGen.crimson?"crimson":"corruption")+" areas that have begun appearing in this land."
							+" If these aren't stopped soon, evil essence will spread far and wide, and the plague along "
							+"with it.",
					objective: new FlatObjective(
						title: LoreEvents.ObjectiveTitle_DefeatEvil,
						description: "There's evil growing in the "+(WorldGen.crimson?"crimson":"corruption")+". It will "
								+ "need to be stopped, or else the plague"
								+ "\n"+"will spread.",
						condition: ( obj ) => NPC.downedBoss2
					)
				),
				new NPCLoreSubStage(
					dialogue: () => "There are many contributing factors, but the main source of your so-called plague "
							+"is within the furthest depths of the world itself. It will be an endeavor just to make it "
							+"there. May the blessings of nature be with you!",
					objective: new FlatObjective(
						title: LoreEvents.ObjectiveTitle_ReachUnderworld,
						description: "It would seem the source of the plague is deep underground. You must find it.",
						condition: ( obj ) => Main.LocalPlayer.position.Y >= (WorldHelpers.UnderworldLayerTopTileY * 16)
					)
				)
			},
			isRepeatable: false
		);
	}
}
