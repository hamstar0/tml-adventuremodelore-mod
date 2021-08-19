using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;
using ModLibsGeneral.Libraries.World;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Sequenced {
	public partial class SequencedLoreEventManager : ILoadable {
		//public const string ObjectiveTitle_DefeatEvil = "Defeat The Evil Biome's Guardian";
		public static string ObjectiveTitle_DefeatEvil => "Defeat The "+(WorldGen.crimson?"Crimson's":"Corruption")+" Guardian";

		public const string ObjectiveTitle_ReachUnderworld = "Reach Underworld";


		public static SequencedLoreEventStage LoreDefs04_DefeatEvil => new SequencedLoreEventStage(
			name: "Dryad Quests",
			prereqs: new Func<bool>[] {
				() => NPC.AnyNPCs( NPCID.Dryad )
			},
			npcType: NPCID.Dryad,
			subStages: new SequencedLoreEventSubStage[] {
				new SequencedLoreEventSubStage(
					dialogue: () => "I see you are here to stop the undeath plague. Might I suggest you first start with the "
							+(WorldGen.crimson?"crimson":"corruption")+" areas that have begun appearing in this land."
							+" If these aren't stopped soon, evil essence will spread far and wide, and the plague along "
							+"with it.",
					objective: new FlatObjective(
						title: SequencedLoreEventManager.ObjectiveTitle_DefeatEvil,
						description: "There's evil growing in the "+(WorldGen.crimson?"crimson":"corruption")+". It will "
								+ "need to be stopped, or else the plague"
								+ "\n"+"will spread.",
						condition: ( obj ) => NPC.downedBoss2
					)
				),
				new SequencedLoreEventSubStage(
					dialogue: () => "There are many contributing factors, but the main source of your so-called plague "
							+"is within the furthest depths of the world itself. It will be an endeavor just to make it "
							+"there. May the blessings of nature be with you!",
					objective: new FlatObjective(
						title: SequencedLoreEventManager.ObjectiveTitle_ReachUnderworld,
						description: "It would seem the source of the plague is deep underground. You must find it.",
						condition: ( obj ) => Main.LocalPlayer.position.Y >= (WorldLocationLibraries.UnderworldLayerTopTileY * 16)
					)
				)
			},
			isRepeatable: false
		);
	}
}
